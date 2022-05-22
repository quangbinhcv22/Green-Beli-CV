using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;


namespace GRBESystem.UI.Screens.Popups.LuckyMen
{
    [RequireComponent(typeof(Button))]
    public class OpenPanelLuckyMenConfirmButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        private void Awake()
        {
            button.onClick.AddListener(EmitOpenConfirmPanelEvent);
        }

        private void EmitOpenConfirmPanelEvent()
        {
            EventManager.EmitEvent(EventName.Client.LuckyMen.OpenLuckyMenConfirmPanel);
        }
    }
}