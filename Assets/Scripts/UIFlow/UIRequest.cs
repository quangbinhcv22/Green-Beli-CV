using System;
using GEvent;
using TigerForge;

namespace UIFlow
{
    [Serializable]
    public class UIRequest
    {
        public static readonly UIRequest ShowDelayPanel = new UIRequest {action = UIAction.Open, id = UIId.DelayResponsePanel};
        public static readonly UIRequest HideDelayPanel = new UIRequest {action = UIAction.Close, id = UIId.DelayResponsePanel};

        public UIAction action;
        public UIId id;
        public bool haveAnimation;
        public object data;

        public void SendRequest() => EventManager.EmitEventData(EventName.UI.RequestScreen(), data: this);
    }
}