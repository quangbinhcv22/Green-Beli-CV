using GEvent;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter
{
    public class HeroSelectSlotInteractableModeEmitter : MonoBehaviour
    {
        [SerializeField] private SelectHeroMode interactableMode;

        private void OnEnable()
        {
            EmitEvent();
        }

        public void EmitEvent()
        {
            EventManager.EmitEventData(EventName.PlayerEvent.SelectHeroMode, data: interactableMode);
        }
    }
}