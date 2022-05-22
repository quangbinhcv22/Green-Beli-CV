using GEvent;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace GRBEGame.UI.BuyPvpTicket
{
    public class BuyPvpTicketQuantityInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField input;

        private void Awake()
        {
            input.onValueChanged.AddListener(OnInputValueChanged);
        }

        private void OnInputValueChanged(string value)
        {
            if (string.IsNullOrEmpty(input.text)) return;

            int.TryParse(input.text, out var inputValue);
            EventManager.EmitEventData(EventName.Select.BuyPvpTicketQuantity, inputValue);
        }

        private void OnValidate()
        {
            Assert.IsNotNull(input);
        }
    }
}