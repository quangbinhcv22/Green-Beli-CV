using System;
using GEvent;
using Network.Messages;
using Newtonsoft.Json;
using Service.Server;
using TigerForge;
using UIFlow;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = "ClaimRewardByDateServerService", menuName = "ScriptableObject/Service/Server/ClaimRewardByDate")]
    public class ClaimRewardByDateServerService : ScriptableObject, IDeserializeResponseMessage<int>
    {
        [NonSerialized] private MessageResponse<int> _response;
        public MessageResponse<int> Response => _response;
        
        
        public void SendRequest(string formattedDate)
        {
            UIRequest.ShowDelayPanel.SendRequest();
            Message.Instance().SetId(EventName.Server.ClaimRewardByDate).SetRequest(new FormattedDataRequest() { date = formattedDate }).Send();
        }

        public MessageResponse<int> DeserializeResponseMessage(string message)
        {
            UIRequest.HideDelayPanel.SendRequest();

            _response = JsonConvert.DeserializeObject<MessageResponse<int>>(message);
            return _response;
        }
    }
}