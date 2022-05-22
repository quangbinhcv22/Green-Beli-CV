using System;
using GEvent;
using GNetwork;
using Network.Messages;
using Network.Messages.StartRound;
using Newtonsoft.Json;
using UIFlow;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(StartRoundServerService), menuName = "NetworkAPI/StartRound")]
    public class StartRoundServerService : ScriptableObject, IServerAPI
    {
        private static StartRoundServerService Instance => NetworkApiManager.GetAPI<StartRoundServerService>();

        public static MessageResponse<StartRoundResponse> Response => Instance._response;
        [NonSerialized] private MessageResponse<StartRoundResponse> _response;


        [SerializeField] private StartRoundScreenHandler screenHandler;


        public string APIName => EventName.Server.StartRound;

        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<StartRoundResponse>>(message);
            if (_response.IsError) return;

            screenHandler.OnSuccess();
        }
    }

    [Serializable]
    public class StartRoundScreenHandler
    {
        public UIRequest screenRequest;

        public void OnSuccess()
        {
            screenRequest.SendRequest();
        }
    }
}