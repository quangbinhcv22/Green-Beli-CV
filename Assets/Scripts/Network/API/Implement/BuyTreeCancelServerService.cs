using System;
using System.Collections.Generic;
using GEvent;
using Network.Messages;
using TigerForge;
using UI.Widget.Toast;
using UIFlow;
using UnityEngine;

namespace GNetwork
{
    [CreateAssetMenu(menuName = "NetworkAPI/BuyTreeCancel", fileName = nameof(BuyTreeCancelServerService))]
    public class BuyTreeCancelServerService : ScriptableObject, IServerAPI
    {

        private static BuyTreeCancelServerService Instance => NetworkApiManager.GetAPI<BuyTreeCancelServerService>();

        [NonSerialized] private MessageResponse<string> _response;
        public static MessageResponse<string> Response => Instance._response;

        public string APIName => EventName.Server.BuyTreeCancel;

        public void OnResponse(string message)
        {
            _response = JsonUtility.FromJson<MessageResponse<string>>(message);

            var toastData = new ToastData
            {
                content = _response.error,
                toastLevel = ToastData.ToastLevel.Danger,
            };
            
            EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, toastData);
        }
    }
}