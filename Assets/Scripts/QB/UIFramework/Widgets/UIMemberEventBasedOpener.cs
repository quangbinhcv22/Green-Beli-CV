using System;
using QuangBinh.UIFramework.Actions;
using QuangBinh.UIFramework.Member;
using UnityEngine;

namespace QuangBinh.UIFramework.Widgets
{
    [Obsolete]
    [DefaultExecutionOrder(100)]
    public class UIMemberEventBasedOpener : UIMember
    {
        [SerializeField, Space] private UIEventGroup enableOnEvents;
        [SerializeField] private UIEventGroup disableOnEvents;


        protected override void OtherActionOnAwake()
        {
            enableOnEvents.StartListening(() => SetActive(true));
            disableOnEvents.StartListening(() => SetActive(false));
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}