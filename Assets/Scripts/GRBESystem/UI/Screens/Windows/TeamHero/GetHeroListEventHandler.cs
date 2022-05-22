using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.TeamHero
{
    public class GetHeroListEventHandler : MonoBehaviour
    {
        [SerializeField] private BattleMode gameMode;


        private void OnEnable()
        {
            if (NetworkService.Instance.IsNotLogged() || gameMode == GetBattleMode()) return;
            
            EventManager.EmitEventData(EventName.Client.Battle.BattleMode, gameMode);
            NetworkService.Instance.services.getHeroList.SendRequest(GetGameMode());
        }

        private GameMode GetGameMode()
        {
            return gameMode is BattleMode.PvE ? GameMode.PVE : GameMode.PVP;
        }

        private BattleMode GetBattleMode()
        {
            var battleModeData = EventManager.GetData(EventName.Client.Battle.BattleMode);
            return battleModeData is null ? BattleMode.PvE :
                (BattleMode) battleModeData is BattleMode.PvP ? BattleMode.PvP : BattleMode.PvE;
        }
    }
}
