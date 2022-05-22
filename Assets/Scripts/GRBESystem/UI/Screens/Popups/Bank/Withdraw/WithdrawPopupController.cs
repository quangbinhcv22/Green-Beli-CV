using Network.Service.Implement;
using Network.Web3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Bank.Withdraw
{
    public class WithdrawPopupController : AGrbeScreenControllerT<WithdrawPopup>
    {
        [SerializeField, Space] private WithdrawConfig withdrawConfig;

        [SerializeField, Space] private TMP_Text feeText;
        [SerializeField] private string formatText;
        
        [SerializeField, Space] private Button withdrawButton;
        [SerializeField] private TMP_InputField withdrawValueInput;

        private int WithdrawValue => int.Parse(withdrawValueInput.text);
        private bool IsValidInput => WithdrawValue >= withdrawConfig.minValue;

        protected override void RegisterEventsOnAwake()
        {
            withdrawButton.onClick.AddListener(Withdraw);
        }

        private void Withdraw()
        {
            if (IsValidInput == false)
            {
                ShowInvalidInputPopup();
                return;
            }

            Web3Controller.Instance.GfruitToken.WithDraw(WithdrawValue);
        }

        private void ShowInvalidInputPopup()
        {
            // const string openInvalidInputPopupEvent = EventName.ScreenEvent.RequestOpenCustomTextPopup;
            // EventManager.EmitEventData(openInvalidInputPopupEvent, withdrawConfig.InvalidInputPopupData());
        }


        protected override void OtherActionOnEnable()
        {
            // feeText.text = $"<color=green>{withdrawConfig.info.fee * 100}%</color> GFRUIT";
            feeText.SetText(string.Format(formatText, withdrawConfig.info.fee * 100));
            
            WithdrawCheckService.WithdrawCheckRequest();
        }

        protected override void OtherActionOnDisable()
        {
        }

        protected override void HandleDataOpenScreenRequest(object data)
        {
        }
    }

    public struct WithdrawPopup
    {
    }
}