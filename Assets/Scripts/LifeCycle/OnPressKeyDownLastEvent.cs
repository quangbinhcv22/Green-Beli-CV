using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace GLifeCycle
{
    public class OnPressKeyDownLastEvent : MonoBehaviour
    {
        public KeyCode keyCode;
        public UnityEvent onPress;

        private void OnEnable() => PressKeyDownLastEventEventManager.AddEvent(this);
        private void OnDisable() => PressKeyDownLastEventEventManager.RemoveEvent(this);
    }

    public class PressKeyDownLastEventEventManager : MonoBehaviour
    {
        private static PressKeyDownLastEventEventManager _instance;
        private static readonly Dictionary<KeyCode, List<OnPressKeyDownLastEvent>> Events;

        static PressKeyDownLastEventEventManager()
        {
            _instance = new GameObject(nameof(PressKeyDownLastEventEventManager),
                typeof(PressKeyDownLastEventEventManager)).GetComponent<PressKeyDownLastEventEventManager>();
            Events = new Dictionary<KeyCode, List<OnPressKeyDownLastEvent>>();
        }

        public static void AddEvent(OnPressKeyDownLastEvent onPressKeyDown)
        {
            var keyCode = onPressKeyDown.keyCode;

            if (Events.ContainsKey(keyCode)) Events[keyCode].Add(onPressKeyDown);
            else Events.Add(keyCode, new List<OnPressKeyDownLastEvent> {onPressKeyDown});
        }

        public static void RemoveEvent(OnPressKeyDownLastEvent onPressKeyDown)
        {
            var keyCode = onPressKeyDown.keyCode;

            if (Events.ContainsKey(keyCode) is false) return;
            
            Events[keyCode].Remove(onPressKeyDown);
        }

        // private void Update()
        // {
        //     foreach (var @event in Events)
        //     {
        //         if (Input.GetKeyDown(@event.Key))
        //         {
        //             if (@event.Value.Any() is false) continue;
        //             
        //             var lastEvent = @event.Value.Last();
        //             lastEvent.onPress?.Invoke();
        //         }
        //     }
        // }
    }
}