using System;
using GEvent;
using Network.Messages;
using UnityEngine;

namespace GNetwork
{
    [CreateAssetMenu(menuName = "NetworkAPI/InactiveTree", fileName = nameof(InactiveTreeServerService))]
    public class InactiveTreeServerService : ScriptableObject, IServerAPI
    {
        private static InactiveTreeServerService Instance => NetworkApiManager.GetAPI<InactiveTreeServerService>();

        [NonSerialized] private MessageResponse<string> _response;

        public static MessageResponse<string> Response => Instance._response;


        public string APIName => EventName.Server.InactiveTree;

        public static void SendRequest(InactiveTreeRequest data)
        {
            Message.Instance().SetId(EventName.Server.InactiveTree).SetRequest(data).Send();
        }

        public void OnResponse(string message)
        {
            _response = JsonUtility.FromJson<MessageResponse<string>>(message);
        }
    }

    [Serializable]
    public class InactiveTreeRequest
    {
        public string isInactive;
    }
}