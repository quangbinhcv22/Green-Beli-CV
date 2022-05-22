using System;
using GEvent;
using Network.Messages;
using UnityEngine;

namespace GNetwork
{
    [CreateAssetMenu(menuName = "NetworkAPI/RestoreTree", fileName = nameof(RestoreDyingTreeServerService))]
    public class RestoreDyingTreeServerService : ScriptableObject, IServerAPI
    {
        private static RestoreDyingTreeServerService Instance =>
            NetworkApiManager.GetAPI<RestoreDyingTreeServerService>();

        [NonSerialized] private MessageResponse<string> _response;

        public static MessageResponse<string> Response => Instance._response;

        public string APIName => EventName.Server.RestoreDyingTree;

        public static void SendRequest(RestoreDyingTreeRequest data)
        {
            Message.Instance().SetId(EventName.Server.RestoreDyingTree).SetRequest(data).Send();
        }

        public void OnResponse(string message)
        {
            _response = JsonUtility.FromJson<MessageResponse<string>>(message);
        }
    }

    [Serializable]
    public class RestoreDyingTreeRequest
    {
        public int treeId;
    }
}