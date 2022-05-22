using GEvent;
using GNetwork;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(GetLotteryDetailServerService), menuName = "NetworkAPI/GetLotteryDetail")]
    public class GetLotteryDetailServerService : ScriptableObject, IServerAPI
    {
        private static GetLotteryDetailServerService Instance =>
            NetworkApiManager.GetAPI<GetLotteryDetailServerService>();
        public static MessageResponse<LotteryDetailResponse> Response => Instance._response;
        
        [System.NonSerialized] private MessageResponse<LotteryDetailResponse> _response;
        
        
        public static void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.GetLotteryDetail).Send();
        }
        
        public string APIName => EventName.Server.GetLotteryDetail;
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<LotteryDetailResponse>>(message);
        }
    }

    [System.Serializable]
    public class LotteryDetailResponse
    {
        public long totalSoldTicketOfDay;
        [JsonProperty("poolRewardGFRLotteryOfDay")]
        public long poolRewardGfrLotteryOfDay;
        
        public long totalSoldTicketOfWeek;
        [JsonProperty("poolRewardGFRLotteryOfWeek")]
        public long poolRewardGfrLotteryOfWeek;
        
        [JsonProperty("poolRewardGFRJackpot")]
        public long poolRewardGfrJackpot;
    }
}
