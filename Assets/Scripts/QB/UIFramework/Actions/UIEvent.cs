namespace QuangBinh.UIFramework.Actions
{
    [System.Serializable]
    public abstract class UIEvent
    {
        protected static string GetEventName(Action action, object owner)
        {
            return $"{action.ToString()}{owner}";
        }

        public abstract string GetEventName();
        public Action action;

        public enum Action
        {
            Enter = 0,
            Exit = 1,
        }
    }
}