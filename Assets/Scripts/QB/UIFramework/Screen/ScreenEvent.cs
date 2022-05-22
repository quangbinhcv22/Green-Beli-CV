using QuangBinh.UIFramework.Actions;

namespace QuangBinh.UIFramework.Screen
{
    [System.Serializable]
    public partial class ScreenEvent : UIEvent
    {
        public ScreenID screenID;

        public override string GetEventName()
        {
            return UIEvent.GetEventName(action, screenID);
        }
    }

    public partial class ScreenEvent
    {
        public static string GetEventNameScreenEnter(ScreenID screenId)
        {
            return UIEvent.GetEventName(Action.Enter, screenId);
        }

        public static string GetEventNameScreenExit(ScreenID screenId)
        {
            return UIEvent.GetEventName(Action.Exit, screenId);
        }
    }
}