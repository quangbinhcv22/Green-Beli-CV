using QuangBinh.UIFramework.Actions;

namespace QuangBinh.UIFramework.Member
{
    [System.Serializable]
    public partial class UIMemberEvent : UIEvent
    {
        public UIMember.Classification memberType;

        public override string GetEventName()
        {
            return UIEvent.GetEventName(action, memberType);
        }
    }

    public partial class UIMemberEvent
    {
        public static string GetEventNameUIMemberEnter(UIMember.Classification memberType)
        {
            return UIEvent.GetEventName(Action.Enter, memberType);
        }

        public static string GetEventNameUIMemberExit(UIMember.Classification memberType)
        {
            return UIEvent.GetEventName(Action.Exit, memberType);
        }
    }
}