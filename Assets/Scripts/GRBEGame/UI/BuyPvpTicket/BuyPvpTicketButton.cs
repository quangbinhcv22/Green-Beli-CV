using System;
using Manager.Inventory;
using Network.Messages;
using Network.Messages.LoadGame;
using Network.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.BuyPvpTicket
{
    [RequireComponent(typeof(Button))]
    public class BuyPvpTicketButton : MonoBehaviour
    {
        [SerializeField] [Space] private TMP_InputField ticketsInput;
        [SerializeField] private float minValue;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            ticketsInput.onValueChanged.AddListener(OnInputFieldValueChanged);
        }

        private void OnEnable()
        {
            OnInputFieldValueChanged(ticketsInput.text);
        }

        private void OnInputFieldValueChanged(string input)
        {
            try
            {
                if (NetworkService.Instance.services.login.IsNotLoggedIn) return;
            }
            catch (Exception)
            {
                return;
            }

            var gameConfigResponse = NetworkService.Instance.services.loadGameConfig.Response;


            int.TryParse(ticketsInput.text, out var inputValue);
            var isPositiveNumber = inputValue > minValue;

            int.TryParse(ticketsInput.text, out var ticketBuy);
            var ticketPrice = gameConfigResponse.data.pvp.ticket_price;
            var ticketsCost = ticketBuy * ticketPrice;

            var balance = NetworkService.playerInfo.inventory.GetMoney(MoneyType.GFruit);
            var isEnoughBalance = balance >= ticketsCost;


            var isValidValue = isPositiveNumber && isEnoughBalance;
            _button.interactable = isValidValue;
        }
    }
}