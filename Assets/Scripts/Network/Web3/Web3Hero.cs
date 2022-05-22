using System;
using Cysharp.Threading.Tasks;
using GEvent;
using Log;
using Newtonsoft.Json;
using TigerForge;
using UI.Widget.Toast;
using UIFlow;
using UnityEngine.Networking;

namespace Network.Web3
{
    public class Web3Hero
    {
        private Web3Controller _web3Controller;

        private string account;

        private string abiTreeNFT;
        private string abiBreeding;
        private string abiFusion;

        public Web3Hero(Web3Controller web3Controller, string account)
        {
            this._web3Controller = web3Controller;

            this.account = account;

            abiTreeNFT = _web3Controller.GetAbi("TreeNFT");
            abiBreeding = _web3Controller.GetAbi("Breeding");
            abiFusion = _web3Controller.GetAbi("Fusion");
        }
        private async UniTask<bool> CheckApproveTreeNFT()
        {
            string contract = BlockChainConfig.treeNFTContract;

            // smart contract method to call
            string method = "isApprovedForAll";
            // array of arguments for contract
            object[] obj = {account, BlockChainConfig.transporterContract};
            string args = JsonConvert.SerializeObject(obj);

            // display response in game
            try
            {
                // connects to user's browser wallet to call a transaction
                string response = await EVM.Call(BlockChainConfig.chain, BlockChainConfig.network,
                    contract, abiTreeNFT, method, args, BlockChainConfig.rpc);

                GLogger.LogLog($"Allowance: {response}");

                var result = Boolean.TryParse(response, out var isApproveAll);
                if (result == false)
                {
                    //throw new Exception($"error {method}: " + response);
                    _web3Controller.ShowToastPanel("Got Error Allowance");
                    return false;
                }

                return isApproveAll;
            }
            catch
            {
                _web3Controller.ShowToastPanel("Got Error Allowance");
                return false;
            }
        }

        private async UniTaskVoid ApproveTreeNFT()
        {
#if UNITY_WEBGL
            string contract = BlockChainConfig.treeNFTContract;
            // smart contract method to call
            string method = "setApprovalForAll";
            // array of arguments for contract
            object[] obj =
            {
                BlockChainConfig.transporterContract, true
            };
            string args = JsonConvert.SerializeObject(obj);

            // display response in game
            try
            {
                string response = await Web3GL.SendContract(method, abiTreeNFT, contract, args, "0");
                GLogger.LogLog(response);
            }
            catch (Exception e)
            {
                GLogger.LogLog("Something is wrong: " + e);
                _web3Controller.ShowToastPanel("Got Error Approve");
            }
#else
            await UniTask.Yield();
#endif
        }

        public async void BreedHero(long hero1, long hero2)
        {
#if UNITY_WEBGL
            var grbeBreedingPrice = 200;

            var approve = await _web3Controller.GrbeToken.CheckAllowance();

            GLogger.LogLog($"Approve: {approve}");

            var depositQuantity = Web3Convert.IntToEther(grbeBreedingPrice);

            if (approve.CompareTo(depositQuantity) <= 0)
            {
                _web3Controller.GrbeToken.Approve();

                GLogger.LogLog($"Approve smaller: {approve} < {grbeBreedingPrice}");

                UIRequest.HideDelayPanel.SendRequest();
                return;
            }

            UIRequest.ShowDelayPanel.SendRequest();

            string message;
            string signature;
            try
            {
                message = $"Breed {hero1} + {hero2} = ???";

                signature = await Web3GL.Sign(message);
            }
            catch
            {
                UIRequest.HideDelayPanel.SendRequest();

                _web3Controller.ShowToastPanel("Not sign message");
                return;
            }

            var url = string.Format(BlockChainConfig.requestBreeding, BlockChainConfig.serverUrl, hero1, hero2,
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

            var breedingResponse =
                JsonConvert.DeserializeObject<DataResponse<BreedingResponse>>(webRequest.downloadHandler.text);
            if (breedingResponse.error_code != 0)
            {
                UIRequest.HideDelayPanel.SendRequest();

                _web3Controller.ShowToastPanel($"Got an error: {breedingResponse.error_code}");

                return;
            }

            // smart contract method to call
            const string method = "breeding";
            // array of arguments for contract
            object[] obj = {breedingResponse.data};
            var args = JsonConvert.SerializeObject(obj);
            try
            {
                // connects to user's browser wallet to call a transaction
                string response = await Web3GL.SendContract(method, abiBreeding, BlockChainConfig.breedingContract, args, "0");
                GLogger.LogLog("Breeding: " + response);

                EventManager.StartListening(EventName.Server.BreedingSuccess, OnBreedSuccess);
            }
            catch (Exception e)
            {
                UIRequest.HideDelayPanel.SendRequest();

                GLogger.LogLog("Something is wrong: " + e);

                _web3Controller.ShowToastPanel("Breeding Failed");
            }
            finally
            {
                GLogger.LogLog("Finish Breeding");
            }
#else
            await UniTask.Yield();
#endif
        }

        private void OnBreedSuccess()
        {
            UIRequest.HideDelayPanel.SendRequest();

            EventManager.StopListening(EventName.Server.BreedingSuccess, OnBreedSuccess);
        }

        public async void FusionHero(long mainHero, long supportHero)
        {
#if UNITY_WEBGL
            var checkAllowNFT = await CheckApproveTreeNFT();

            GLogger.LogLog($"checkAllowNFT: {checkAllowNFT}");

            if (checkAllowNFT == false)
            {
                ApproveTreeNFT().Forget();

                UIRequest.HideDelayPanel.SendRequest();
                return;
            }

            var grbeFusionPrice = 200;

            var approve = await _web3Controller.GrbeToken.CheckAllowance();

            GLogger.LogLog($"Approve: {approve}");

            var depositQuantity = Web3Convert.IntToEther(grbeFusionPrice);

            if (approve.CompareTo(depositQuantity) <= 0)
            {
                _web3Controller.GrbeToken.Approve();

                GLogger.LogLog($"Approve smaller: {approve} < {grbeFusionPrice}");

                UIRequest.HideDelayPanel.SendRequest();
                return;
            }

            UIRequest.ShowDelayPanel.SendRequest();

            string message;
            string signature;
            try
            {
                message = $"Fusion Main[{mainHero}] + Support[{supportHero}]";

                signature = await Web3GL.Sign(message);
            }
            catch
            {
                UIRequest.HideDelayPanel.SendRequest();

                _web3Controller.ShowToastPanel("Not sign message");
                return;
            }

            var url = string.Format(BlockChainConfig.requestFusion, BlockChainConfig.serverUrl, mainHero, supportHero,
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

            var fusionResponse =
                JsonConvert.DeserializeObject<DataResponse<FusionResponse>>(webRequest.downloadHandler.text);
            if (fusionResponse.error_code != 0)
            {
                UIRequest.HideDelayPanel.SendRequest();

                _web3Controller.ShowToastPanel($"Got an error: {fusionResponse.error_code}");

                return;
            }

            // smart contract method to call
            const string method = "fusion";
            // array of arguments for contract
            object[] obj = {fusionResponse.data};
            var args = JsonConvert.SerializeObject(obj);
            try
            {
                // connects to user's browser wallet to call a transaction
                string response = await Web3GL.SendContract(method, abiFusion, BlockChainConfig.fusionContract, args, "0");
                GLogger.LogLog("Fusing: " + response);

                EventManager.StartListening(EventName.Server.FusionSuccess, OnFusionSuccess);
            }
            catch (Exception e)
            {
                UIRequest.HideDelayPanel.SendRequest();

                GLogger.LogLog("Something is wrong: " + e);

                _web3Controller.ShowToastPanel("Fusion Failed");
            }
            finally
            {
                GLogger.LogLog("Finish Fusion");
            }
#else
            await UniTask.Yield();
#endif
        }

        private void OnFusionSuccess()
        {
            UIRequest.HideDelayPanel.SendRequest();

            EventManager.StopListening(EventName.Server.FusionSuccess, OnFusionSuccess);
        }
    }
}