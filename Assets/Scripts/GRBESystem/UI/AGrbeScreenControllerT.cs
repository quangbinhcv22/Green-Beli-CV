using System;
using UnityEngine;

namespace GRBESystem.UI
{
    [Obsolete]
    public abstract class AGrbeScreenControllerT<T> : AGrbeScreenController
    {
        [SerializeField, Space] protected T screenData;

        private GrbeScreenDataReceiveService _screenDataReceiveService;
        private GrbeScreenDataChangeService _screenDataChangeService;

        protected abstract void RegisterEventsOnAwake();

        protected override void OtherActionOnAwake()
        {
            _screenDataReceiveService = new GrbeScreenDataReceiveService(screenId, ChangeData);
            _screenDataChangeService = new GrbeScreenDataChangeService(screenId);

            EmitEventScreenData();
            RegisterEventsOnAwake();
        }


        protected override void OtherActionOnEmitEvent()
        {
            EmitEventScreenData();
        }

        protected void EmitEventScreenData()
        {
            _screenDataChangeService.EmitEventDataChanged<T>(screenData);
        }

        private void ChangeData()
        {
            var newScreenData = _screenDataReceiveService.GetScreenData<T>();
            screenData = newScreenData;
        }
    }
}