using System;
using GEvent;
using GRBEGame.UI.Nation;
using Network.Messages;
using Newtonsoft.Json;
using TigerForge;
using UI.Widget.Toast;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(SetNationServerService), menuName = "ScriptableObject/Service/Server/SetNation")]
    public class SetNationServerService : ScriptableObject, IDeserializeResponseMessage<string>
    {
        [NonReorderable] private MessageResponse<string> _response;
        public MessageResponse<string> Response => _response;

        public void SendRequest(NationRequest request) =>
            Message.Instance().SetId(EventName.Server.SetNation).SetRequest(request).Send();

        public MessageResponse<string> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<string>>(message);

            if (_response.IsError is false)
            {
                var nationSelection = EventManager.GetData(EventName.UI.Select<NationSelection>());
                NetworkService.Instance.services.login.SetNation(nationSelection is null
                    ? "OT"
                    : ((NationSelection)nationSelection).nationCode);
            }

            var toastData = _response.IsError
                ? new ToastData { toastLevel = ToastData.ToastLevel.Danger, content = _response.error }
                : new ToastData { toastLevel = ToastData.ToastLevel.Safe, content = _response.data };

            EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, toastData);

            return _response;
        }
    }

    [Serializable]
    public class NationRequest
    {
        public string nationCode;
    }
}