using Network.Service;
using Network.Web3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Bank.Deposit
{
    public class DepositPopupController : AGrbeScreenControllerT<DepositPopupData>
    {
        [SerializeField] private Button depositButton;
        [SerializeField] private TMP_InputField depositValueInput;
        private int DepositValue => string.IsNullOrEmpty(depositValueInput.text) ? 0 :  int.Parse(depositValueInput.text);

        protected override void RegisterEventsOnAwake()
        {
            depositButton.onClick.AddListener(Deposit);
        }

        private void Deposit()
        {
            Web3Controller.Instance.GfruitToken.Deposit(DepositValue);
        }

        protected override void OtherActionOnEnable()
        {
            ResetDepositValueInput();
        }

        private void ResetDepositValueInput()
        {
            depositValueInput.text = string.Empty;
        }


        protected override void OtherActionOnDisable()
        {
        }

        protected override void HandleDataOpenScreenRequest(object data)
        {
        }
    }

    public struct DepositPopupData
    {
    }
}