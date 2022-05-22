using Manager.Inventory;
using Network.Service;
using TMPro;
using UI.Widget.Reflector;
using UnityEngine;


namespace GRBESystem.UI.Screens.Popups.Bank.Deposit.Widgets
{
    public class DepositInputReflector : MonoBehaviour
    {
        private int GfrToken => NetworkService.playerInfo.inventory.GetMoney(MoneyType.GFruit);

        [SerializeField] private TMP_InputField depositInput;

        [SerializeField, Space] private TMP_Text statusText;
        [SerializeField] private ButtonInteractReflector buttonInteractReflector;

        [SerializeField] private string orangeColorCode = "FFAC23";
        //[SerializeField] private string greenColorCode = "00FF0C";


        private void Awake()
        {
            depositInput.onValueChanged.AddListener(OnInputChanged);
            buttonInteractReflector.SetInteractCondition(() => GetValidInputValue() > 0);
        }

        private void OnEnable()
        {
            OnInputChanged(depositInput.text);
        }

        private void OnInputChanged(string input)
        {
            if (input == string.Empty)
            {
                statusText.text = FormattedString("Please enter the value", orangeColorCode);
            }
            else
            {
                statusText.text = string.Empty;
            }

            buttonInteractReflector.ReflectInteract();
        }

        private long GetValidInputValue()
        {
            if (depositInput.text == string.Empty || depositInput.text.Length >= (long.MaxValue).ToString().Length ||
                depositInput.text.CompareTo("-") == 0)
            {
                return 0;
            }

            return long.Parse(depositInput.text);
        }

        private string FormattedString(string originalString, string colorCode)
        {
            return $"<color=#{colorCode}>{originalString}</color>";
        }
    }
}
