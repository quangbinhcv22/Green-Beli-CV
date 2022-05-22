using GEvent;
using QuangBinh.UIFramework.Screen;
using TigerForge;

namespace GRBESystem.UI
{
    public class GrbeScreenDataChangeService
    {
        private ScreenID _screenID;

        public GrbeScreenDataChangeService(ScreenID screenID)
        {
            SetScreenID(screenID);
        }
        
        private void SetScreenID(ScreenID screenID)
        {
            _screenID = screenID;
        }

        public void EmitEventDataChanged<T>(T newData)
        {
            EventManager.EmitEventData(EventName.ScreenEvent.GetScreenDataEventName(_screenID), data: newData);
        }
    }
}