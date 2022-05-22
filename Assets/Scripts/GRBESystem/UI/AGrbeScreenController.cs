using System;
using Network.Service;
using QB.UIFramework.Screen;
using UnityEngine;

namespace GRBESystem.UI
{
    [Obsolete]
    public class AGrbeScreenController : AScreenController
    {
        [SerializeField, Space] private bool emitEvenEnableDisableWithoutLogin;

        protected override void OtherActionOnEnable()
        {
        }

        protected override void OtherActionOnDisable()
        {
        }

        protected override void OtherActionOnEmitEvent()
        {
        }

        protected override void HandleDataOpenScreenRequest(object data)
        {
        }

        protected override bool GetConditionEmitEventEnableDisable()
        {
            return emitEvenEnableDisableWithoutLogin || NetworkService.Instance.services.login.IsLoggedIn;
        }
    }
}