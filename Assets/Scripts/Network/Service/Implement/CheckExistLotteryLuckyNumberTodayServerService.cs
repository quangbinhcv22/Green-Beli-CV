using System;
using System.Collections.Generic;
using GEvent;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(CheckExistLotteryLuckyNumberTodayServerService), menuName = "ScriptableObject/Service/Server/CheckExistLotteryLuckyNumberToday")]
    public class CheckExistLotteryLuckyNumberTodayServerService : ScriptableObject, IDeserializeResponseMessage<List<long>>
    {
        [NonSerialized] private MessageResponse<List<long>> _response;
        public MessageResponse<List<long>> Response => _response;
        
        
        public void SendRequest(CheckExistLotteryLuckyNumberTodayRequest request)
        {
            Message.Instance().SetId(EventName.Server.CheckExistCurrentLotteryLuckyNumber).SetRequest(request).Send();
        }

        public MessageResponse<List<long>> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<List<long>>>(message);
            return _response;
        }
    }

    [Serializable]
    public class CheckExistLotteryLuckyNumberTodayRequest
    {
        public List<long> heroIds;
    }
}