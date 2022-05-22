using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;
using Utils;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(GetTransactionHistoryServerService),
        menuName = "ScriptableObject/Service/Server/GetTransactionHistory")]
    public class GetTransactionHistoryServerService : ScriptableObject,
        IDeserializeResponseMessage<List<TransactionHistoryResponse>>
    {
        [NonSerialized] private MessageResponse<List<TransactionHistoryResponse>> _response;
        public MessageResponse<List<TransactionHistoryResponse>> Response => _response;

        public void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.GetTransactionHistory).SetResponse(null).SetRequest(null)
                .Send();
        }

        public MessageResponse<List<TransactionHistoryResponse>> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<List<TransactionHistoryResponse>>>(message);

            if (_response.IsError is false)
                _response.data = _response.data.OrderByDescending(history =>
                    history.createdTime.ToDateTime(DateTimeUtils.GreenBeliFullDateFormat)).ToList();

            return _response;
        }
    }

    [System.Serializable]
    public struct TransactionHistoryResponse
    {
        public string createdTime;
        public string id;
        public string type;
        public string quantity;
        public int status;
    }
}