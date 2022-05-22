using GEvent;
using Network.Service.Implement;
using TigerForge;
using UIFlow;
using UnityEngine;

namespace UI.ServerResponseHandler
{
    public class ServerResponseScreenHandler : MonoBehaviour
    {
        [SerializeField] private UIRequest battleOpinionDisconnectRequest;
        [SerializeField] private UIRequest battleResultStatusRequest;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.EndGame,
                () => Invoke(nameof(OnPvpEndgame), EndGameServerService.DelayConfig.battleResultPopup));
        }

        private void OnPvpEndgame()
        {
            if(EndGameServerService.Response.IsError) return;
            
            if(EndGameServerService.Data.IsOpinionQuitPvp())
                battleOpinionDisconnectRequest.SendRequest();
            else
                battleResultStatusRequest.SendRequest();
        }
    }
}
