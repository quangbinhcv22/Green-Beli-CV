using System.Linq;
using GEvent;
using Network.Service.Implement;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;

namespace Network.Service.ClientUpdate.RewardHistory
{
    public class RewardHistoryClientUpdater : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.EndGame, OnEndGameResponse);
            EventManager.StartListening(EventName.Server.SkipAllGame, OnSkipAllGameResponse);
        }

        private void OnEndGameResponse()
        {
            var battleMode = EventManager.GetData(EventName.Client.Battle.BattleMode);
            if (battleMode is null || (BattleMode)battleMode is BattleMode.PvP) return;

            var endGameResponse = EndGameServerService.Response;
            if (endGameResponse.IsError) return;

            var playerInfo = endGameResponse.data.player.Find(player =>
                player.playerId == NetworkService.Instance.services.login.MessageResponse.data.id);
            NetworkService.Instance.services.getRewardHistoryAll.AddAmountRewardToday(playerInfo.TotalToken);
        }

        private void OnSkipAllGameResponse()
        {
            if (SkipAllGameServerService.Response.IsError) return;
            NetworkService.Instance.services.getRewardHistoryAll.SendRequest();
        }
    }
}