using System;
using QuangBinh.UIFramework.Screen;

namespace GEvent
{
    public static partial class EventName
    {
        public static class UI
        {
            public static string Select<T>() => $"Select{typeof(T)}";
            public static string RequestScreen() => $"{nameof(ScreenRequest)}"; //data: ScreenRequest
            public static string ActiveUIsChanged() => "ActiveUIChanged";


            public const string StartLoadSession = "LoadedAllQueue"; //data: UnityEvent (on loaded all)
            public const string AddLoadingQueue = "AddLoadingQueue"; //data: string
        }
    }

    [Serializable]
    public class ScreenRequest
    {
        public ScreenAction action;
        public ScreenID screenID;
        public object data;

        public ScreenRequest SetAction(ScreenAction action)
        {
            this.action = action;
            return this;
        }

        public ScreenRequest SetScreenID(ScreenID screenID)
        {
            this.screenID = screenID;
            return this;
        }

        public ScreenRequest SetData(object data)
        {
            this.data = data;
            return this;
        }
    }

    public enum ScreenAction
    {
        Unset = 0,
        Open = 1,
        Switch = 2,
        Close = 3,
        New = 4,
    }
}