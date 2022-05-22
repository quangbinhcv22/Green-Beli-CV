using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(TokenHasChangedServerService), menuName = "ScriptableObject/Service/Server/TokenHasChanged")]
    public class TokenHasChangedServerService : ScriptableObject, IDeserializeResponseMessage<TokenHasChangedResponse>
    {
        private MessageResponse<TokenHasChangedResponse> _messageResponse;
        public MessageResponse<TokenHasChangedResponse> MessageResponse => _messageResponse;


        public MessageResponse<TokenHasChangedResponse> DeserializeResponseMessage(string message)
        {
            _messageResponse = JsonConvert.DeserializeObject<MessageResponse<TokenHasChangedResponse>>(message);
            return MessageResponse;
        }
    }

    [System.Serializable]
    public struct TokenHasChangedResponse
    {
        public int gfrToken;
    }
}