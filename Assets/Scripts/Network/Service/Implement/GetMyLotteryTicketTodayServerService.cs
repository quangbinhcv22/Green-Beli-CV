using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(GetMyLotteryTicketTodayServerService), menuName = "ScriptableObject/Service/Server/GetLotteryTicketByDate")]
    public class GetMyLotteryTicketTodayServerService : ScriptableObject, IDeserializeResponseMessage<List<GetMyLotteryTicketTodayResponse>>
    {
        [NonSerialized] private MessageResponse<List<GetMyLotteryTicketTodayResponse>> _response;
        public MessageResponse<List<GetMyLotteryTicketTodayResponse>> Response => _response;
        
        
        public void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.GetMyCurrentLotteryTicket).SetRequest(null).SetResponse(null).Send();
        }   

        public MessageResponse<List<GetMyLotteryTicketTodayResponse>> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<List<GetMyLotteryTicketTodayResponse>>>(message);
            return _response;
        }

        public List<long> GetLuckyNumbers()
        {
            return _response.IsError ? new List<long>() : _response.data.Select(ticket => ticket.luckyNumber).ToList();
        }
    }
    
    [Serializable]
    public class GetMyLotteryTicketTodayResponse
    {
        public string owner;
        public long luckyNumber;
        public string updatedTime;
        public int winOrder;
    }
}