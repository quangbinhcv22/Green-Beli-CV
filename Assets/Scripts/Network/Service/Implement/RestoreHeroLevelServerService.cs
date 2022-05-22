using System;
using GEvent;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(RestoreHeroLevelServerService), menuName = "ScriptableObject/Service/Server/RestoreHeroLevel")]
    public class RestoreHeroLevelServerService : ScriptableObject, IDeserializeResponseMessage<string>
    {
        [NonSerialized] private MessageResponse<string> _response;
        public MessageResponse<string> Response => _response;
        
        
        public void SendRequest(RestoreHeroLevelRequest request)
        {
            Message.Instance().SetId(EventName.Server.RestoreHeroLevel).SetRequest(request).Send();
        }
        
        public MessageResponse<string> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<string>>(message);
            return _response;
        }
    }

    [Serializable]
    public class RestoreHeroLevelRequest
    {
        public int restoredLevel;
        public long heroId;
    }
}