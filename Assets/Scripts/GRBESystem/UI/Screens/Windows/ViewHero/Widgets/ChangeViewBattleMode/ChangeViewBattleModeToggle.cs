using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.ViewHero.Widgets.ChangeViewBattleMode
{
    public class ChangeViewBattleModeToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private BattleMode battleModeWhenOn;
        [SerializeField] private BattleMode battleModeWhenOff;

        private BattleMode _battleMode = BattleMode.PvE;
        

        private void Awake()
        {
            toggle.onValueChanged.AddListener(ChangeViewBattleMode);
        }

        private void OnEnable()
        {
            var data = EventManager.GetData(EventName.Client.Battle.BattleMode);
            if (data is null) toggle.isOn = default;
            else toggle.isOn = (BattleMode) data == battleModeWhenOn;
        }

        private void ChangeViewBattleMode(bool isOn)
        {
            _battleMode = isOn switch
            {
                true => battleModeWhenOn,
                _ => battleModeWhenOff,
            };
            SetBattleMode();
        }
        
        private void SetBattleMode()
        {
            EventManager.EmitEventData(EventName.Client.Battle.BattleMode, _battleMode);
            NetworkService.Instance.services.getHeroList.SendRequest(_battleMode is BattleMode.PvP
                ? GameMode.PVP
                : GameMode.PVE);
        }
    }
}
