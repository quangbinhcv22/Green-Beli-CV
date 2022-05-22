#if UNITY_EDITOR
using System.Collections.Generic;
using GNetwork;
using UnityEngine;

namespace Network.Controller
{
    public class FakeMessageReceived : MonoBehaviour
    {
        [System.Serializable]
        private struct FakeAction
        {
            public KeyCode keyCode;
            public string messageReceived;
        }

        [SerializeField] private List<FakeAction> fakeActions;

        private WebSocketController _webSocketController;

        private void Start()
        {
            _webSocketController = WebSocketController.Instance;
        }

        void Update()
        {
            foreach (var fakeAction in fakeActions)
            {
                if (Input.GetKeyDown(fakeAction.keyCode))
                {
                    NetworkApiManager.OnResponse(fakeAction.messageReceived);
                }
            }
        }
    }
}
#endif