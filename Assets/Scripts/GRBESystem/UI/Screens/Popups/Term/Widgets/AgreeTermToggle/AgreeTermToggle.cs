using System;
using QuangBinh.UIFramework.Screen;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Term.Widgets.AgreeTermToggle
{
    [DefaultExecutionOrder(2000)]
    public class AgreeTermToggle : MonoBehaviour
    {
        private const ScreenID ScreenOwnerID = ScreenID.TermsPopup;
        private GrbeScreenDataReceiveService _screenDataReceiveService;
        private GrbeScreenDataChangeService _screenDataChangeService;

        [SerializeField] private Toggle agreeToggle;


        private void Awake()
        {
            _screenDataReceiveService = new GrbeScreenDataReceiveService(ScreenOwnerID, SetIsOn);
            _screenDataChangeService = new GrbeScreenDataChangeService(ScreenOwnerID);

            agreeToggle.onValueChanged.AddListener(SetIsAgree);

            const bool isAgreeValueDefault = false;
            agreeToggle.isOn = isAgreeValueDefault;
        }

        private void SetIsAgree(bool isAgreed)
        {
            try
            {
                var popupData = _screenDataReceiveService.GetScreenData<TermsPopupData>();
                popupData.isAgreed = isAgreed;

                _screenDataChangeService.EmitEventDataChanged(popupData);
            }
            catch (NullReferenceException)
            {
                return;
            }
        }

        private void SetIsOn()
        {
            var isAgreed = _screenDataReceiveService.GetScreenData<TermsPopupData>().isAgreed;
            if (agreeToggle.isOn == isAgreed) return;

            agreeToggle.isOn = isAgreed;
        }
    }
}