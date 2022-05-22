using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GRBESystem.Widgets.EventSystemDisable
{
    public class EventSystemDisabler : MonoBehaviour
    {
        private EventSystem _eventSystem;

        private void Awake()
        {
            _eventSystem = EventSystem.current;
        }

        private void OnEnable()
        {
            _eventSystem.enabled = false;
        }

        private void OnDisable()
        {
            try
            {
                _eventSystem.enabled = true;
            }
            catch (Exception)
            {
                //end game
            }
        }
    }
}