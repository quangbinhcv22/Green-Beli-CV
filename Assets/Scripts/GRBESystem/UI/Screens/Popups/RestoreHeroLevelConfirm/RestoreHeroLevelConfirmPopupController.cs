using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.RestoreHeroLevelConfirm
{
    public class RestoreHeroLevelConfirmPopupController : AGrbeScreenController
    {
        [SerializeField] private Button confirmButton;
        
        
        protected override void OtherActionOnAwake()
        {
            confirmButton.onClick.AddListener(EmitRestoreHeroLevelsEvent);
        }
        
        protected override void OtherActionOnEnable()
        {
        }

        protected override void OtherActionOnDisable()
        {
        }

        protected override void HandleDataOpenScreenRequest(object data)
        {
        }

        private void EmitRestoreHeroLevelsEvent()
        {
           EventManager.EmitEventData(EventName.PlayerEvent.ConfirmRestoreLevels, true);
        }
    }
}