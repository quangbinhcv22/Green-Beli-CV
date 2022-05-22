using GEvent;
using TigerForge;
using UnityEngine;

namespace UIFlow.InGame
{
    [DefaultExecutionOrder(100)]
    public class RequestUIEventListener : MonoBehaviour
    {
        [SerializeField] private UIFrame2 frame;

        private void Awake()
        {
            EventManager.StartListening(EventName.UI.RequestScreen(), OnRequestScreen);
        }

        private void OnRequestScreen()
        {
            var uiRequest = EventManager.GetData(EventName.UI.RequestScreen());
            if (uiRequest is null) return;

            if(uiRequest is UIRequest request) frame.Request(request);
        }
    }
}