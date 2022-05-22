using GEvent;
using QuangBinh.UIFramework.Screen;
using TigerForge;
using UnityEngine.Events;

namespace GRBESystem.UI
{
    public class GrbeScreenDataReceiveService
    {
        private string _screenDataEventName;

        public GrbeScreenDataReceiveService(ScreenID screenID)
        {
            SetEventName(screenID);
        }

        public GrbeScreenDataReceiveService(ScreenID screenID, UnityAction callback) : this(screenID)
        {
            AddListener(callback);
        }


        private void SetEventName(ScreenID screenID)
        {
            _screenDataEventName = EventName.ScreenEvent.GetScreenDataEventName(screenID);
        }

        private void AddListener(UnityAction callback)
        {
            EventManager.StartListening(_screenDataEventName, callback);
        }

        
        public T GetScreenData<T>()
        {
            return EventManager.GetData<T>(_screenDataEventName);
        }
    }
}