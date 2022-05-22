using System;
using GEvent;
using GNetwork;
using Network.Messages;
using SandBox.DataInformation;
using SandBox.Tree.InfomartionPopup;
using UIFlow;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(menuName = "NetworkAPI/ServerMaintain", fileName = nameof(ServerMaintainServerService))]

    public class ServerMaintainServerService : ScriptableObject, IServerAPI
    {
        private static ServerMaintainServerService Instance => NetworkApiManager.GetAPI<ServerMaintainServerService>();

        [NonSerialized] private MessageResponse<ServerInfoResponse> _response;

        public static MessageResponse<ServerInfoResponse> Response => Instance._response;
        [SerializeField] private UIRequest maintainScreenRequest;

        public string APIName => EventName.Server.Info;

        public void OnResponse(string message)
        {
            _response = JsonUtility.FromJson<MessageResponse<ServerInfoResponse>>(message);

            maintainScreenRequest.SendRequest();
        }
    }

    [Serializable]
    public class ServerInfoResponse
    {
        public string content;
    }
}