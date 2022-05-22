using GEvent;
using Network.Service.Implement;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;
using UnityEngine.Events;

namespace UI.ScreenController.Window.Battle.Widgets.SetModeButton
{
    [DefaultExecutionOrder(1000)]
    public class DoEventsBasedOnBattleMode : MonoBehaviour
    {
        [SerializeField] private UnityEvent onPvEMode;
        [SerializeField] private UnityEvent onPvPMode;
        [SerializeField] private UnityEvent onPvPDisconnectMode;

        private static BattleMode CurrentBattleMode
        {
            get
            {
                var boxedBattleMode = EventManager.GetData(EventName.Client.Battle.BattleMode);
                return boxedBattleMode is null ? BattleMode.None : (BattleMode)boxedBattleMode;
            }
        }

        private void OnEnable()
        {
            var currentBattle = CurrentBattleMode;
            if (currentBattle is BattleMode.None || EndGameServerService.Response.IsError) return;

            var isOpinionQuit = EndGameServerService.Data.IsOpinionQuitPvp();
            (CurrentBattleMode switch
            {
                BattleMode.PvP when isOpinionQuit => onPvPDisconnectMode,
                BattleMode.PvP => onPvPMode,
                _ => onPvEMode,
            })?.Invoke();
        }
    }
}