using GEvent;
using GNetwork;
using GRBESystem.UI.Screens.Windows.MatchPvp;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(CancelPlayPvpServerService), menuName = "NetworkAPI/CancelPlayPvP")]
    public class CancelPlayPvpServerService : ScriptableObject, IServerAPI
    {
        private static CancelPlayPvpServerService Instance => NetworkApiManager.GetAPI<CancelPlayPvpServerService>();
        public static MessageResponse<CancelPlayPvpResponse> Response => Instance._response;

        [System.NonSerialized] private MessageResponse<CancelPlayPvpResponse> _response;


        public static void SendRequest()
            => Message.Instance().SetId(EventName.Server.CancelPlayPvp).Send();

        public string APIName => EventName.Server.CancelPlayPvp;
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<CancelPlayPvpResponse>>(message);
        }
    }
}
