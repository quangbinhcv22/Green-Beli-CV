using System;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using Network.Messages;
using Newtonsoft.Json;
using SandBox.DataInformation;
using SandBox.Tree.InfomartionPopup;
using UIFlow;
using UnityEngine;
using UnityEngine.Serialization;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(PlayPvpServerService), menuName = "NetworkAPI/PlayPvp")]
    public class PlayPvpServerService : ScriptableObject, IServerAPI
    {
        private static PlayPvpServerService Instance => NetworkApiManager.GetAPI<PlayPvpServerService>();
        
        [NonSerialized] private MessageResponse<string> _response;
        public static MessageResponse<string> Response => Instance._response;


        [SerializeField] private JoinPvpScreenHandler screenHandler;


        public static void SendRequest(int numberTicket = default)
        {
            UIRequest.ShowDelayPanel.SendRequest();

            var request = new PvpTicketRequest() {numberTicket = numberTicket};
            Message.Instance().SetId(EventName.Server.PlayPvp).SetRequest(request).Send();
        }

        public string APIName => EventName.Server.PlayPvp;

        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<string>>(message);

            if (_response.IsError)
            {
                screenHandler.OnError(_response.error);
            }
            else
            {
                screenHandler.OnSuccess();
            }

            UIRequest.HideDelayPanel.SendRequest();
        }
    }

    [System.Serializable]
    public struct PvpTicketRequest
    {
        public int numberTicket;
    }

    [Serializable]
    public class JoinPvpScreenHandler
    {
        private static bool IsNotSelectHeroError(string error) => error.Contains("Choose heroes");

        [SerializeField] [Space] private List<UIRequest> onSuccessUIRequests;

        [SerializeField] [Space] private UIRequest openInfoPopupRequest;
        [SerializeField]  private UIRequest onNotSelectHeroPopupRequest;
        //[SerializeField] private InfoPopupPreset notSelectHeroPopupInfo;

        public void OnSuccess()
        {
            onSuccessUIRequests.SendRequest();
        }
        
        public void OnError(string error)
        {
            InfoPopupData2 popupData;

            if (IsNotSelectHeroError(error))
            {
                onNotSelectHeroPopupRequest.SendRequest();
                return;
            }
            popupData = InfoPopupData2.Empty;

            popupData.content = error;
            openInfoPopupRequest.data = popupData;

            openInfoPopupRequest.SendRequest();
        }
    }
}