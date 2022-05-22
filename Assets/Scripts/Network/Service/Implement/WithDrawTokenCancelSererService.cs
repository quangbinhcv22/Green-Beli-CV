using System;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(WithDrawTokenCancelSererService), menuName = "ScriptableObject/Service/Server/WithDrawTokenCancel")]
    public class WithDrawTokenCancelSererService : ScriptableObject, IDeserializeResponseMessage<TokenHasChangedResponse>, ITokenHasChangedService
    {
        [NonSerialized] private MessageResponse<TokenHasChangedResponse> _response;
        public MessageResponse<TokenHasChangedResponse> Response => _response;

        
        public MessageResponse<TokenHasChangedResponse> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<TokenHasChangedResponse>>(message);
            return _response;
        }

        public int GetNewGFruit()
        {
            return _response.data.gfrToken;
        }
    }
}