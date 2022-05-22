using System;
using System.Collections.Generic;
using Network.Messages;
using Network.Messages.StartPhase;
using Newtonsoft.Json;
using UIFlow;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(StartPhaseServerService),
        menuName = "ScriptableObject/Service/Server/StartPhase")]
    public class StartPhaseServerService : UnityEngine.ScriptableObject, IDeserializeResponseMessage<StartPhaseResponse>
    {
        [NonSerialized] private MessageResponse<StartPhaseResponse> _response;
        public MessageResponse<StartPhaseResponse> Response => _response;


        [SerializeField] private StartPhaseScreenHandler screenHandler;


        public MessageResponse<StartPhaseResponse> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<StartPhaseResponse>>(message);

            if (_response.IsError is false) screenHandler.OnSuccess();

            return _response;
        }
    }

    [Serializable]
    public class StartPhaseScreenHandler
    {
        [SerializeField] private List<UIRequest> onSuccess;

        public void OnSuccess()
        {
            onSuccess.SendRequest();
        }
    }
}