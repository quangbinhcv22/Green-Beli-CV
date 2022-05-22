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
    public class Web3GfruitToken : Web3Token
    {
        private string abiGamingReward;

        public Web3GfruitToken(Web3Controller web3Controller, string contract, string account, string abi) : base(
            web3Controller, contract, account, abi)
        {
            Name = "GFRUIT";
            
            abiGamingReward = web3Controller.GetAbi("GBGamingReward");
        }

        public override async void Deposit(int number)
        {
#if UNITY_WEBGL
            UIRequest.ShowDelayPanel.SendRequest();

            GLogger.LogLog($"Deposit: {number} => {Account}");

            var approve = await CheckAllowance();

            GLogger.LogLog($"Approve: {approve}");

            var depositQuantity = Web3Convert.IntToEther(number);

            if (approve.CompareTo(depositQuantity) <= 0)
            {
                Approve();

                GLogger.LogLog($"Approve smaller: {approve} < {number}");

                UIRequest.HideDelayPanel.SendRequest();
                return;
            }

            GLogger.LogLog("Before get token");

            var gfruitInWallet = await GetToken();

            GLogger.LogLog("wallet value: " + gfruitInWallet);
            if (gfruitInWallet < number)
            {
                EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, new ToastData()
                {
                    toastLevel = ToastData.ToastLevel.Danger,
                    content = $"Gfruit in your wallet is not enough: <color=green>{gfruitInWallet}</color>"
                });
                GLogger.LogLog($"Gfruit in your wallet is not enough: {gfruitInWallet}");

                UIRequest.HideDelayPanel.SendRequest();
                return;
            }

            // smart contract method to call
            string method = "deposited";

            // array of arguments for contract
            object[] obj =
            {
                Web3Convert.IntToEther(number).ToString()
            };
            string args = JsonConvert.SerializeObject(obj);

            // display response in game
            try
            {
                string response = await Web3GL.SendContract(method, abiGamingReward, BlockChainConfig.rewardContract, args, "0");
                GLogger.LogLog("Deposit response: " + response);
                EventManager.StartListening(EventName.Inventory.Change, OnMoneyChangeDeposit);
            }
            catch (Exception e)
            {
                UIRequest.HideDelayPanel.SendRequest();

                GLogger.LogLog("Something is wrong: " + e);

                _web3Controller.ShowToastPanel("Deposit GFRUIT Token Failed");
            }
            finally
            {
                GLogger.LogLog("Finish Deposit");
            }
#else
            await UniTask.Yield();
#endif
        }

        private void OnMoneyChangeDeposit()
        {
            EventManager.EmitEvent(EventName.ScreenEvent.RequestCloseCurrentPopup);
            UIRequest.HideDelayPanel.SendRequest();

            _web3Controller.ShowToastPanel("Deposit success!", ToastData.ToastLevel.Safe);

            EventManager.StopListening(EventName.Inventory.Change, OnMoneyChangeDeposit);
        }

        public override async void WithDraw(int quantity)
        {
#if UNITY_WEBGL
            UIRequest.ShowDelayPanel.SendRequest();

            string message;
            string signature;
            try
            {
                message = $"Withdraw {quantity}";

                signature = await Web3GL.Sign(message);
            }
            catch
            {
                UIRequest.HideDelayPanel.SendRequest();

                _web3Controller.ShowToastPanel("Not sign message");
                return;
            }

            var url = string.Format(BlockChainConfig.requestClaimToken, BlockChainConfig.serverUrl, Account, quantity);

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

            var dataResponse =
                JsonConvert.DeserializeObject<DataResponse<RewardResponse>>(webRequest.downloadHandler.text);

            if (dataResponse.error_code != 0)
            {
                GLogger.LogLog("Something is wrong: " + dataResponse.error_code);
                _web3Controller.ShowToastPanel("Withdrawal Failed");
                UIRequest.HideDelayPanel.SendRequest();
                return;
            }

            var data = dataResponse.data;

            // smart contract method to call
            string method = "claimReward";
            // array of arguments for contract
            object[] obj = {data};
            string args = JsonConvert.SerializeObject(obj);
            try
            {
                // connects to user's browser wallet to call a transaction
                string response = await Web3GL.SendContract(method, abiGamingReward, BlockChainConfig.rewardContract, args, "0");
                GLogger.LogLog("Withdraw: " + response);
                EventManager.StartListening(EventName.Server.WithdrawSuccess, OnWithdrawSuccess);
            }
            catch (Exception e)
            {
                GLogger.LogLog("Something is wrong: " + e);
                _web3Controller.ShowToastPanel("Withdrawal Failed");
                UIRequest.HideDelayPanel.SendRequest();
            }
            finally
            {
                GLogger.LogLog("Finish WithDraw");
            }
#else
            await UniTask.Yield();
#endif
        }

        private void OnWithdrawSuccess()
        {
            EventManager.EmitEvent(EventName.ScreenEvent.RequestCloseCurrentPopup);
            UIRequest.HideDelayPanel.SendRequest();

            _web3Controller.ShowToastPanel("Withdraw success!", ToastData.ToastLevel.Safe);

            EventManager.StopListening(EventName.Server.WithdrawSuccess, OnWithdrawSuccess);
        }
    }
}