using GEvent;
using Manager.Inventory;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UI.Widget.Reflector;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank.Withdraw.Widgets
{
    public class WithdrawInputReflector : MonoBehaviour
    {
        private int GfrToken => NetworkService.playerInfo.inventory.GetMoney(MoneyType.GFruit);
        
        [SerializeField] private TMP_InputField withdrawInput;
        [SerializeField] private WithdrawConfig withdrawConfig;

        [SerializeField, Space] private TMP_Text statusText;
        [SerializeField] private ButtonInteractReflector buttonInteractReflector;

        [SerializeField] private string orangeColorCode = "FFAC23";
        [SerializeField] private string greenColorCode = "00FF0C";


        private void Awake()
        {
            withdrawInput.onValueChanged.AddListener(OnInputChanged);
            buttonInteractReflector.SetInteractCondition(() =>
                GetValidInputValue() >= withdrawConfig.minValue && GetValidInputValue() <= withdrawConfig.maxValue &&
                GetValidInputValue() <= GfrToken);

            EventManager.StartListening(EventName.Server.CheckCanWithdraw, () => OnInputChanged(withdrawInput.text));
        }

        private void OnEnable()
        {
            OnInputChanged(withdrawInput.text);
        }

        private void OnInputChanged(string input)
        {
            if (WithdrawCheckService.ResponseData is {canWithdraw: false})
            {
                statusText.text = FormattedString(statusText.text, greenColorCode);
                return;
            }

            if (input == string.Empty)
            {
                statusText.text = FormattedString("Please enter the value", orangeColorCode);
            }
            else if (input.Length >= (long.MaxValue).ToString().Length)
            {
                statusText.text = FormattedString("Number is too big", orangeColorCode);
            }
            else if (GetValidInputValue() < withdrawConfig.minValue)
            {
                statusText.text = FormattedString($"Value must be greater than {withdrawConfig.minValue:N0}", orangeColorCode);
            }
            else if (GetValidInputValue() > withdrawConfig.maxValue)
            {
                statusText.text = FormattedString($"Value must be less than {withdrawConfig.maxValue:N0}", orangeColorCode);
            }
            else if (GetValidInputValue() > GfrToken)
            {
                statusText.text = FormattedString("Insufficient balance", orangeColorCode);
            }
            else
            {
                var realClaim = GetValidInputValue() * ( 1 - GetFeePercent());
                var statusMessage = $"You will received {realClaim:N0} GFRUIT";
                
                statusText.text = FormattedString(statusMessage, greenColorCode);
            }

            buttonInteractReflector.ReflectInteract();
        }

        private float GetFeePercent()
        {
            return withdrawConfig.info.fee; //fake
        }

        private long GetValidInputValue()
        {
            if (withdrawInput.text == string.Empty || withdrawInput.text.Length >= (long.MaxValue).ToString().Length ||
                withdrawInput.text.CompareTo("-") == 0)
            {
                return 0;
            }

            return long.Parse(withdrawInput.text);
        }

        private string FormattedString(string originalString, string colorCode)
        {
            return $"<color=#{colorCode}>{originalString}</color>";
        }
    }
}