using Cysharp.Threading.Tasks;
using GEvent;
using Log;
using Newtonsoft.Json;
using TigerForge;
using UnityEngine;
using UnityEngine.Networking;

namespace Network.Service.Implement
{
    public static class WithdrawCheckService
    {
        private const string WithdrawCheckString = "https://api-dev.greenbeli.io/player/can-claim-gfr-token?address={0}";
        private static WithdrawCheckData _response;


        public static WithdrawCheckData ResponseData => _response;

        public static bool IsDataError => _response is null || _response.errorCode > (int) default;
        
        public static async void WithdrawCheckRequest()
        {
            OnResponse(await WithdrawCheckResponse());
        }

        private static void OnResponse(string message)
        {
            Debug.Log(message);
            _response = JsonConvert.DeserializeObject<WithdrawCheckData>(message);
            EventManager.EmitEvent(EventName.Server.CheckCanWithdraw);
        }
        
        private static async UniTask<string> WithdrawCheckResponse()
        {
            var requestLink = string.Format(WithdrawCheckString, NetworkService.playerInfo.address);

            GLogger.LogLog($"Send: <color=#00B0F0>{requestLink}</color>");
            return (await UnityWebRequest.Get(requestLink).SendWebRequest().ToUniTask()).downloadHandler.text;
        }
    }
    
    
    [System.Serializable]
    public class WithdrawCheckData
    {
        [JsonProperty("error_code")]
        public int errorCode;
        [JsonProperty("result")]
        public bool canWithdraw;
    }
}
