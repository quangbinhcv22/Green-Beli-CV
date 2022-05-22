using System;
using GEvent;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(SkipGameService), menuName = "ScriptableObject/Service/Server/SkipGameService")]
    public class SkipGameService : ScriptableObject
    {
        [NonSerialized] private MessageResponse<string> _response;
        public MessageResponse<string> Response => _response;

        
        public void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.SkipGame).SetRequest(null).SetResponse(null).Send();
        }
        
        public MessageResponse<string> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<string>>(message);
            return _response;
        }
    }
}
