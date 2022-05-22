using System;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(WithDrawTokenSuccessSererService), menuName = "ScriptableObject/Service/Server/WithDrawTokenSuccess")]
    public class WithDrawTokenSuccessSererService : ScriptableObject, IDeserializeResponseMessage<TokenHasChangedResponse>, ITokenHasChangedService
    {
        [NonSerialized] private MessageResponse<TokenHasChangedResponse> _response;
        public MessageResponse<TokenHasChangedResponse> Response => _response;

        
        public MessageResponse<TokenHasChangedResponse> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<TokenHasChangedResponse>>(message);
            
            if (_response.IsError is false)
                OnWithDrawSuccess();
            
            return _response;
        }

        private void OnWithDrawSuccess()
        {
            WithdrawCheckService.WithdrawCheckRequest();
        }

        public int GetNewGFruit()
        {
            return _response.data.gfrToken;
        }
    }
}