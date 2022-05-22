using GEvent;
using Network.Service;
using TigerForge;
using UI.Widget.Toast;
using UnityEngine;

namespace UI.ServerResponseHandler
{
    public class ServerResponseToastHandler : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.SetPassword, OnBindingAccountResponse);
        }

        private void OnBindingAccountResponse()
        {
            if (NetworkService.Instance.services.setPassword.Response.IsError)
            {
                EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, new ToastData()
                {
                    content = $"Set password failed: {NetworkService.Instance.services.setPassword.Response.error}",
                    toastLevel = ToastData.ToastLevel.Danger,
                });
            }
            else
            {
                EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, new ToastData()
                {
                    content = "Set password successful",
                    toastLevel = ToastData.ToastLevel.Safe,
                });
            }
        }
    }
}