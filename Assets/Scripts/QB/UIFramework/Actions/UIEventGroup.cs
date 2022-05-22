using System;
using System.Collections.Generic;
using System.Linq;
using QuangBinh.UIFramework.Member;
using TigerForge;
using UnityEngine.Events;

namespace QuangBinh.UIFramework.Actions
{
    [Obsolete]
    [System.Serializable]
    public struct UIEventGroup
    {
        public List<UIMemberEvent> uiMemberEvents;
        public List<Screen.ScreenEvent> screenEvents;

        public void StartListening(UnityAction callBack)
        {
            var allEventName = new List<string>();
            allEventName.AddRange(collection: uiMemberEvents.Select(uiMemberEvent => uiMemberEvent.GetEventName()));
            allEventName.AddRange(collection: screenEvents.Select(screenEvent => screenEvent.GetEventName()));

            foreach (var eventName in allEventName)
            {
                EventManager.StartListening(eventName, callBack);
            }
        }
    }
}