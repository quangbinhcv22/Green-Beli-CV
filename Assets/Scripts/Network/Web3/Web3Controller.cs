using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GEvent;
using Log;
using Network.Service;
using Newtonsoft.Json;
using Pattern;
using TigerForge;
using UI.Widget.Toast;
using UIFlow;
using UnityEngine;
using UnityEngine.Networking;

namespace Network.Web3
{
    public class Web3Controller : Singleton<Web3Controller>
    {
        private Web3GfruitToken _gfruitToken;

        public Web3GfruitToken GfruitToken
        {
            get
            {
                return _gfruitToken ??= new Web3GfruitToken(
                    this, BlockChainConfig.gfruitTokenContract,
                    NetworkService.playerInfo.address, GetAbi("GFRUIT"));
            }
        }

        private Web3GrbeToken _grbeToken;

        public Web3GrbeToken GrbeToken
        {
            get
            {
                return _grbeToken ??= new Web3GrbeToken(this, BlockChainConfig.grbeTokenContract,
                    NetworkService.playerInfo.address, GetAbi("GRBE"));
            }
        }
        
        private Web3GmetaToken _gmetaToken;

        public Web3GmetaToken GmetaToken
        {
            get
            {
                return _gmetaToken ??= new Web3GmetaToken(this, BlockChainConfig.gmetaTokenContract,
                    NetworkService.playerInfo.address, GetAbi("GMETA"));
            }
        }
        
        private Web3BusdToken _busdToken;

        public Web3BusdToken BusdToken
        {
            get
            {
                return _busdToken ??= new Web3BusdToken(this, BlockChainConfig.busdTokenContract,
                    NetworkService.playerInfo.address, GetAbi("BUSD"));
            }
        }

        private Web3Hero _hero;

        public Web3Hero GreenHero
        {
            get
            {
                return _hero ??= new Web3Hero(this, NetworkService.playerInfo.address);
            }
        }

        private Web3Tree _tree;

        public Web3Tree RealTree
        {
            get
            {
                return _tree ??= new Web3Tree(this, NetworkService.playerInfo.address);
            }
        }

        public void AssembleIntoBox(int fragmentType)
        {
        }

        public void PackIntoBox(int fragmentType, int quantity)
        {
        }

        public void UnboxIntoInventory(long boxID)
        {
        }

        public void UnpackIntoInventory(long packID)
        {
        }

        private string abiReward = string.Empty;

        string GetAbiReward()
        {
            if (abiReward.Equals(String.Empty))
                abiReward = Resources.Load<TextAsset>("Abi/GBGamingReward").text;
            return abiReward;
        }

        public string GetAbi(string path)
        {
            return Resources.Load<TextAsset>($"Abi/{path}").text;
        }

        public void ShowToastPanel(string content, ToastData.ToastLevel level = ToastData.ToastLevel.Danger)
        {
            EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, new ToastData()
            {
                toastLevel = level,
                content = content
            });
        }

        ///////////////////// NOT USE /////////////////////////
        public async void GetTotalClaim(string account)
        {
            // smart contract method to call
            string method = "totalClaimed";
            // abi in json format
            string abi = GetAbiReward();
            // array of arguments for contract
            string[] obj = {account};
            string args = JsonConvert.SerializeObject(obj);

            // display response in game
            try
            {
                // connects to user's browser wallet to call a transaction
                string response = await EVM.Call(BlockChainConfig.chain, BlockChainConfig.network,
                    BlockChainConfig.rewardContract, abi, method, args, BlockChainConfig.rpc);
                // display response in game
                GLogger.LogLog(response);
            }
            catch (Exception e)
            {
                GLogger.LogLog("Something is wrong: " + e);
                ShowToastPanel("Get total claim failed");
            }
        }

        public async Task<WithDrawTokenInfo> GetWithdrawTokenInfo(string address)
        {
            var request = string.Format(BlockChainConfig.requestGetWithdrawInfo, BlockChainConfig.serverUrl, address);

            UnityWebRequest webRequest = UnityWebRequest.Get(request);

            await webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                ShowToastPanel(webRequest.error);
                return new WithDrawTokenInfo()
                {
                    fee = 1,
                };
            }

            GLogger.LogLog(webRequest.downloadHandler.text);

            return JsonConvert.DeserializeObject<DataResponse<WithDrawTokenInfo>>(webRequest.downloadHandler.text).data;
        }
    }
}