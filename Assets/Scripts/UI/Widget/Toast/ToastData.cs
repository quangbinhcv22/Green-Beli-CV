using GEvent;
using TigerForge;

namespace UI.Widget.Toast
{
    [System.Serializable]
    public struct ToastData
    {
        public string content;
        public ToastLevel toastLevel;

        public void Show()
        {
            EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, this);
        }

        public enum ToastLevel
        {
            Safe = 10,
            Neutral = 20,
            Danger = 30,
        }
    }
}