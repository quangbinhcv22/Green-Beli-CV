using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.TeamHero
{
    public class SetSelectButtonGroupBattleModeRequester : MonoBehaviour
    {
        [SerializeField] private SelectButton pveModeButton;
        [SerializeField] private SelectButton pvpModeButton;


        private void Awake()
        {
            pvpModeButton.AddOnSelectedCallback(() => SetBattleModeRequest(BattleMode.PvP));
            pveModeButton.AddOnSelectedCallback(() => SetBattleModeRequest(BattleMode.PvE));
        }

        private void OnEnable()
        {
            var battleModeData = EventManager.GetData(EventName.Client.Battle.BattleMode);
            if ((BattleMode?) battleModeData is BattleMode.PvP) 
                pvpModeButton.Set(true);
            else 
                pveModeButton.Set(true);
        }
        
        private void SetBattleModeRequest(BattleMode battleMode)
        {
            var battleModeData = EventManager.GetData(EventName.Client.Battle.BattleMode);
            var mode = battleModeData is null ? BattleMode.PvE :
                (BattleMode) battleModeData is BattleMode.PvP ? BattleMode.PvP : BattleMode.PvE;
            battleMode = battleMode is BattleMode.PvP ? BattleMode.PvP : BattleMode.PvE;
            if(mode == battleMode) return;

            EventManager.EmitEventData(EventName.Client.Battle.BattleMode, battleMode);
            NetworkService.Instance.services.getHeroList.SendRequest(battleMode is BattleMode.PvP
                ? GameMode.PVP
                : GameMode.PVE);
        }
    }
}
