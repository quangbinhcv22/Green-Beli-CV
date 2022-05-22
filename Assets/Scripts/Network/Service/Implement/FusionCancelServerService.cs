using System;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(FusionCancelServerService), menuName = "ScriptableObject/Service/Server/FusionCancel")]
    public class FusionCancelServerService : ScriptableObject, IDeserializeResponseMessage<FusionCancelResponse>, ITokenHasChangedService
    {
        [NonSerialized] private MessageResponse<FusionCancelResponse> _response;
        public MessageResponse<FusionCancelResponse> Response => _response;
        
        
        public MessageResponse<FusionCancelResponse> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<FusionCancelResponse>>(message);
            return _response;
        }

        public int GetNewGFruit()
        {
            return _response.data.gfrToken;
        }
    }

    [System.Serializable]
    public class FusionCancelResponse
    {
        public int gfrToken;
        public long mainHeroId;
        public long supportHeroId;
    }
}