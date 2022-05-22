using QuangBinh.UIFramework.Screen;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Term.Widgets.AcceptTermButton
{
    public class AcceptTermButton : MonoBehaviour
    {
        private const ScreenID ScreenOwnerID = ScreenID.TermsPopup;
        private GrbeScreenDataReceiveService _screenDataReceiveService;
        private GrbeScreenDataChangeService _screenDataChangeService;

        [SerializeField] private Button acceptButton;


        private void Awake()
        {
            _screenDataReceiveService = new GrbeScreenDataReceiveService(ScreenOwnerID, InteractableIfIsTermAgreed);
            _screenDataChangeService = new GrbeScreenDataChangeService(ScreenOwnerID);
            
            acceptButton.onClick.AddListener(SetIsAccepted);
            SetDefaultInteractableAcceptButton();
        }

        
        private void InteractableIfIsTermAgreed()
        {
            SetInteractableAcceptButton(_screenDataReceiveService.GetScreenData<TermsPopupData>().isAgreed);
        }

        private void SetInteractableAcceptButton(bool isInteractable)
        {
            acceptButton.interactable = isInteractable;
        }

        private void SetDefaultInteractableAcceptButton()
        {
            const bool isAcceptValueDefault = false;
            SetInteractableAcceptButton(isAcceptValueDefault);
        }

        
        private void SetIsAccepted()
        {
            var popupData = _screenDataReceiveService.GetScreenData<TermsPopupData>();
            popupData.isAccepted = true;

            _screenDataChangeService.EmitEventDataChanged(popupData);
        }
    }
}