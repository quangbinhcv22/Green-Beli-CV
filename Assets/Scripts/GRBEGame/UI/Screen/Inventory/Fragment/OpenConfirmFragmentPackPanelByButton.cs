using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Screen.Inventory
{
    public class OpenConfirmFragmentPackPanelByButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Awake()
        {
            button.onClick.AddListener(EmitOpenConfirmPanelEvent);
        }

        private void EmitOpenConfirmPanelEvent()
        {
            EventManager.EmitEvent(EventName.UI.Select<ConfirmFragmentPackPanel>());
        }
    }
}
