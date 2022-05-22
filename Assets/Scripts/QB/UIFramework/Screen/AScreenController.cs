using System;
using GEvent;
using QuangBinh.UIFramework.Member;
using QuangBinh.UIFramework.Screen;
using TigerForge;
using UnityEngine;

namespace QB.UIFramework.Screen
{
    [DefaultExecutionOrder(1000)]
    public abstract class AScreenController : UIMember
    {
        [Space] public ScreenID screenId;


        protected abstract void OtherActionOnEnable();
        protected abstract void OtherActionOnDisable();
        protected abstract void OtherActionOnEmitEvent();
        protected abstract void HandleDataOpenScreenRequest(object data);

        protected virtual bool GetConditionEmitEventEnableDisable()
        {
            return true;
        }


        private void OnEnable()
        {
            if (GetConditionEmitEventEnableDisable() == false) return;

            EventManager.EmitEvent(UIMemberEvent.GetEventNameUIMemberEnter(classification));
            EventManager.EmitEvent(ScreenEvent.GetEventNameScreenEnter(screenId));

            OtherActionOnEmitEvent();
            OtherActionOnEnable();
        }

        private void OnDisable()
        {
            try
            {
                if (GetConditionEmitEventEnableDisable() == false) return;

                EventManager.EmitEvent(UIMemberEvent.GetEventNameUIMemberExit(classification));
                EventManager.EmitEvent(ScreenEvent.GetEventNameScreenExit(screenId));

                OtherActionOnDisable();
            }
            catch (NullReferenceException)
            {
                //end game
            }
        }


        public void OpenWithData(object data = null)
        {
            EventManager.EmitEventData(EventName.ScreenEvent.ScreenOpeningID, data: screenId);

            SetActive(true);
            HandleDataOpenScreenRequest(data);
        }

        public void Open()
        {
            OpenWithData();
        }

        public virtual void SetData(object data)
        {
        }

        public void Close()
        {
            SetActive(false);
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}