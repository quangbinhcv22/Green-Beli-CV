using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using GNetwork;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;
using Utils;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(GetLotteryResultByDateServerService), menuName = "NetworkAPI/GetLotteryResult")]
    public class GetLotteryResultByDateServerService : ScriptableObject, IServerAPI
    {
        private static GetLotteryResultByDateServerService Instance =>
            NetworkApiManager.GetAPI<GetLotteryResultByDateServerService>();
        public static MessageResponse<GetLotteryResultByDateResponse> Response => Instance._response;

        [NonSerialized] private MessageResponse<GetLotteryResultByDateResponse> _response;

        
        public static void SendRequest(GetLotteryResultByDateRequest byDateRequest)
        {
            Message.Instance().SetId(EventName.Server.GetLotteryResult).SetRequest(byDateRequest).Send();
        }

        public string APIName => EventName.Server.GetLotteryResult;
        
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<GetLotteryResultByDateResponse>>(message);

            if (_response.IsError is false)
                _response.data.winTickets = _response.data.winTickets.OrderBy(winTicket => winTicket.winOrder).ToList();
        }
    }

    [Serializable]
    public class GetLotteryResultByDateRequest
    {
        public string time;
        public string type = nameof(LotteryType.DAILY);
    }

    [Serializable]
    public enum LotteryType
    {
        DAILY = 0,
        JACKPOT = 1,
    }

    [Serializable]
    public class GetLotteryResultByDateResponse
    {
        public string time;
        public int numberTicket;
        public List<WinTicketResponse> winTickets;

        public DateTime GetDate => time.ToDateTime(DateTimeUtils.GreenBeliFullDateFormat);
    }
    
    [Serializable]
    public class WinTicketResponse
    {
        public string owner;
        public long luckyNumber;
        public int winOrder;

        public bool isJackpot;
    }
}