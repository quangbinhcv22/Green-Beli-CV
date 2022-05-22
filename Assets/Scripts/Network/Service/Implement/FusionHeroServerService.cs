using System;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(FusionHeroServerService), menuName = "ScriptableObject/Service/Server/FusionHero")]
    public class FusionHeroServerService : ScriptableObject, IDeserializeResponseMessage<FusionHeroResponse>, ITokenHasChangedService
    {
        [NonSerialized] private MessageResponse<FusionHeroResponse> _response;
        public MessageResponse<FusionHeroResponse> Response => _response;
        
        
        public MessageResponse<FusionHeroResponse> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<FusionHeroResponse>>(message);
            return _response;
        }

        public int GetNewGFruit()
        {
            return _response.data.gfrToken;
        }
    }

    [System.Serializable]
    public class FusionHeroResponse
    {
        public int gfrToken;
        public long mainHeroId;
        public long supportHeroId;
    }
}