using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UIFlow;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.ServerRequest
{
    [RequireComponent(typeof(Button))]
    public class PlayPvPButton : MonoBehaviour
    {
        private void Awake() => GetComponent<Button>().onClick.AddListener(UpdateListHero);

        private void UpdateListHero()
        {
            NetworkService.Instance.services.getHeroList.SendRequest(GameMode.PVP);
            EventManager.StartListening(EventName.Server.GetListHero, SendFightPvpRequest);
        }

        private void SendFightPvpRequest()
        {
            EventManager.StopListening(EventName.Server.GetListHero, SendFightPvpRequest);
            
            var pvpRoom = EventManager.GetData(EventName.Client.Battle.PvpRoom);
            PlayPvpServerService.SendRequest(pvpRoom is null ? default : (int)pvpRoom);
            
            EventManager.EmitEventData(EventName.Client.Battle.BattleMode, BattleMode.PvP);
        }
    }
}