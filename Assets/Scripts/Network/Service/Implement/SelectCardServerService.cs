using System;
using GEvent;
using GNetwork;
using Network.Messages;
using Network.Messages.SelectCard;
using Newtonsoft.Json;
using UIFlow;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(SelectCardServerService), menuName = "NetworkAPI/SelectCard")]
    public class SelectCardServerService : ScriptableObject, IServerAPI
    {
        private static SelectCardServerService Instance => NetworkApiManager.GetAPI<SelectCardServerService>();

        public static MessageResponse<string> Response => Instance._response;
        [NonSerialized] private MessageResponse<string> _response;


        [SerializeField] private SelectCardScreenHandler screenHandler;


        public static void SendRequest(int cardIndex)
        {
            UIRequest.ShowDelayPanel.SendRequest();

            var request = new SelectCardRequest() {cardNumber = cardIndex};
            Message.Instance().SetId(EventName.Server.SelectCard).SetRequest(request).Send();
        }

        public string APIName => EventName.Server.SelectCard;

        public void OnResponse(string message)
        {
            UIRequest.HideDelayPanel.SendRequest();

            _response = JsonConvert.DeserializeObject<MessageResponse<string>>(message);

            if (_response.IsError) return;

            screenHandler.OnSuccess();
        }
    }

    [Serializable]
    public class SelectCardScreenHandler
    {
        [SerializeField] private UIRequest screenRequest;
        
        public void OnSuccess()
        {
            screenRequest.SendRequest();
        }
    }
}