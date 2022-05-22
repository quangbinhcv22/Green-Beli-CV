using System;
using GEvent;
using GNetwork;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(WinLotteryServerService), menuName = "NetworkAPI/WinLottery")]
    public class WinLotteryServerService : ScriptableObject, IServerAPI
    {
        private static WinLotteryServerService Instance => NetworkApiManager.GetAPI<WinLotteryServerService>();
        public static MessageResponse<WinLotteryResponse> Response => Instance._response;
        [NonSerialized] private MessageResponse<WinLotteryResponse> _response;

        
        public string APIName => EventName.Server.WinLottery;
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<WinLotteryResponse>>(message);
            NetworkService.Instance.services.getRewardHistoryAll.SendRequest();
        }
    }

    [Serializable]
    public class WinLotteryResponse
    {
        public long luckyNumber;
        public string type;
        [JsonProperty("rewardGFRToken")]
        public long rewardGfrToken;
        
        public int winOrder;
    }
}