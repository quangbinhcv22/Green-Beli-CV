using System;
using GEvent;
using Log;
using Newtonsoft.Json;
using TigerForge;
using UIFlow;
using UnityEngine.Networking;

namespace Network.Web3
{
    public class Web3Tree
    {
        private Web3Controller _web3Controller;

        private string account;

        private string abiTreeNFT;
        
        public Web3Tree(Web3Controller web3Controller, string account)
        {
            this._web3Controller = web3Controller;

            this.account = account;

            abiTreeNFT = _web3Controller.GetAbi("PlantTreeNFT");
        }

        public async void OpenTree(int quantity)
        {
#if UNITY_WEBGL
            UIRequest.ShowDelayPanel.SendRequest();
            
            var approveGmeta = await _web3Controller.GmetaToken.CheckAllowance();
            
            var approveBusd = await _web3Controller.BusdToken.CheckAllowance();

            GLogger.LogLog($"Approve Gmeta: {approveGmeta}\n Approve Busd: {approveBusd}");

            if (approveGmeta <= 0)
            {
                _web3Controller.GmetaToken.Approve();

                UIRequest.HideDelayPanel.SendRequest();
                return;
            }
            if (approveBusd <= 0)
            {
                _web3Controller.BusdToken.Approve();

                UIRequest.HideDelayPanel.SendRequest();
                return;
            }
            
            string message;
            string signature;
            try
            {
                message = $"Open {quantity} + tree(s)";
            
                signature = await Web3GL.Sign(message);
            }
            catch
            {
                UIRequest.HideDelayPanel.SendRequest();
            
                _web3Controller.ShowToastPanel("Not sign message");
                return;
            }

            var url = string.Format(BlockChainConfig.requestOpenTree, BlockChainConfig.serverUrl, quantity,
                account);

            UnityWebRequest webRequest = UnityWebRequest.Get(url);

            webRequest.SetRequestHeader("message", message);
            webRequest.SetRequestHeader("signature", signature);

            await webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                UIRequest.HideDelayPanel.SendRequest();

                _web3Controller.ShowToastPanel(webRequest.error);
                return;
            }
            
            GLogger.LogLog(webRequest.downloadHandler.text);

            var openTreeResponse =
                JsonConvert.DeserializeObject<DataResponse<OpenTreeResponse>>(webRequest.downloadHandler.text);
            if (openTreeResponse.error_code != 0)
            {
                UIRequest.HideDelayPanel.SendRequest();

                _web3Controller.ShowToastPanel($"Got an error: {openTreeResponse.error_code}");

                return;
            }
            
            // smart contract method to call
            const string method = "unbox";
            // array of arguments for contract
            object[] obj = {openTreeResponse.data};
            var args = JsonConvert.SerializeObject(obj);
            try
            {
                GLogger.LogLog("Open Tree Response args: " + args);
                // connects to user's browser wallet to call a transaction
                string response = await Web3GL.SendContract(method, abiTreeNFT, BlockChainConfig.plantTreeNFT, args, "0");
                GLogger.LogLog("Open Tree Response: " + response);

                EventManager.StartListening(EventName.Server.HaveNewTree, OnOpenTreeSuccess);
            }
            catch (Exception e)
            {
                UIRequest.HideDelayPanel.SendRequest();

                GLogger.LogLog("Something is wrong: " + e);

                _web3Controller.ShowToastPanel("Open Tree Failed");
                EventManager.EmitEvent(EventName.Server.GetListTree);
            }
            finally
            {
                GLogger.LogLog("Finish Open Tree");
                UIRequest.HideDelayPanel.SendRequest();
            }
#else
            await UniTask.Yield();
#endif
        }
        
        private void OnOpenTreeSuccess()
        {
            UIRequest.HideDelayPanel.SendRequest();

            EventManager.StopListening(EventName.Server.HaveNewTree, OnOpenTreeSuccess);
        }
    }
    
    
}