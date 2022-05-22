using System;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using Network.Messages;
using UnityEngine;

namespace GNetwork
{
    [CreateAssetMenu(menuName = "NetworkAPI/ActiveTree", fileName = nameof(ActiveTreeServerService))]
    public class ActiveTreeServerService : ScriptableObject, IServerAPI
    {
        private static ActiveTreeServerService Instance => NetworkApiManager.GetAPI<ActiveTreeServerService>();

        [NonSerialized] private MessageResponse<string> _response;
        
        public static MessageResponse<string> Response => Instance._response;
        
        
        public static void SendRequest(ActiveTreeRequest data)
        {
            Message.Instance().SetId(EventName.Server.ActiveTree).SetRequest(data).Send();
        }

        public string APIName => EventName.Server.ActiveTree;

        public void OnResponse(string message)
        {
            _response = JsonUtility.FromJson<MessageResponse<string>>(message);
        }
    }

    [Serializable]
    public class ActiveTreeRequest
    {
        public string isActive;
    }
}
