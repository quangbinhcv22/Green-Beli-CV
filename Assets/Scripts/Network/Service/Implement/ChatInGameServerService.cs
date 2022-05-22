using GEvent;
using GRBESystem.Definitions;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = "ChatInGameServerService", menuName = "ScriptableObject/Service/Server/ChatInGame")]
    public class ChatInGameServerService : ScriptableObject, IDeserializeResponseMessage<ChatInGameResponse>
    {
        [NonReorderable] private MessageResponse<ChatInGameResponse> _response;
        public MessageResponse<ChatInGameResponse> Response => _response;

        public void SendRequest(ChatInGameRequest request)
        {
            Message.Instance().SetId(EventName.Server.ChatInGame).SetRequest(request).Send();
        }

        public MessageResponse<ChatInGameResponse> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<ChatInGameResponse>>(message);
            return _response;
        }
    }

    [System.Serializable]
    public class ChatInGameRequest
    {
        public string text;
        public string player;
    }

    [System.Serializable]
    public class ChatInGameResponse
    {
        public string text;
        public string player;

        public Owner Player()
        {
            var loginResponse = NetworkService.Instance.services.login.MessageResponse;
            if (loginResponse.IsError) return Owner.Opponent;

            var isSelf = player == loginResponse.data.id;
            return isSelf ? Owner.Self : Owner.Opponent;
        }
    }
}