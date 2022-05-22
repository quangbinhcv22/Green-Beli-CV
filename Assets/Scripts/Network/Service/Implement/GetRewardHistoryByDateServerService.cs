using System;
using System.Collections.Generic;
using GEvent;
using Network.Messages;
using Newtonsoft.Json;
using Service.Server;
using UIFlow;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(GetRewardHistoryByDateServerService), menuName = "ScriptableObject/Service/Server/GetRewardHistoryByDate")]
    public class GetRewardHistoryByDateServerService : ScriptableObject, IDeserializeResponseMessage<List<RewardHistorySourceResponse>>
    {
        [NonSerialized] private MessageResponse<List<RewardHistorySourceResponse>> _response;
        public MessageResponse<List<RewardHistorySourceResponse>> Response => _response;


        public void SendRequest(string formattedDate)
        {
            Message.Instance().SetId(EventName.Server.GetRewardHistoryByDate).SetRequest(new FormattedDataRequest() { date = formattedDate }).Send();
        }

        public MessageResponse<List<RewardHistorySourceResponse>> DeserializeResponseMessage(string message)
        {
            UIRequest.HideDelayPanel.SendRequest();
            _response = JsonConvert.DeserializeObject<MessageResponse<List<RewardHistorySourceResponse>>>(message);

            return _response;
        }
    }

    [System.Serializable]
    public struct RewardHistorySourceResponse
    {
        public string date;
        public double amount;
        public bool isClaimed;
        public string type;
    }
}