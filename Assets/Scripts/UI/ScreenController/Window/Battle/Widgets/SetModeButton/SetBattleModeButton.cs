using GEvent;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ScreenController.Window.Battle.Widgets.SetModeButton
{
    [RequireComponent(typeof(Button))]
    public class SetBattleModeButton : MonoBehaviour
    {
        [SerializeField] private BattleMode battleMode;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(SetBattleMode);
        }

        private void SetBattleMode()
        {
            EventManager.EmitEventData(EventName.Client.Battle.BattleMode, battleMode);
        }
    }
}