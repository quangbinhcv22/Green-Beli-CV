using System;
using Manager.Inventory;
using Network.Messages;
using Network.Messages.LoadGame;
using Network.Service;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GRBEGame.UI.BuyPvpTicket
{
    public class BuyPvpTicketPricePanel : MonoBehaviour
    {
        [SerializeField] [Space] private TMP_InputField ticketsInput;

        [SerializeField] [Space] private TMP_Text originPriceText;
        [SerializeField] private TMP_Text discountPriceText;
        [SerializeField] private TMP_Text originCostText;
        [SerializeField] private TMP_Text discountCostText;

        [SerializeField] [Space] private string textFormat = "{0:N0}";
        [SerializeField] private int valueDefault;

        [SerializeField] [Space] private GameObject insufficientBalance;

        [SerializeField] [Space] private UnityEvent onValueDefault;
        [SerializeField] private UnityEvent onValueCustom;


        private void Awake()
        {
            ticketsInput.onValueChanged.AddListener(OnValueInputChanged);
        }

        private void OnEnable()
        {
            OnValueInputChanged(ticketsInput.text);
        }

        private void OnValueInputChanged(string input)
        {
            int.TryParse(ticketsInput.text, out var ticketBuy);


            MessageResponse<LoadGameConfigResponse> gameConfigResponse;

            try
            {
                gameConfigResponse = NetworkService.Instance.services.loadGameConfig.Response;
            }
            catch (NullReferenceException)
            {
                return;
            }

            if (gameConfigResponse.IsError)
            {
                UpdatePriceDefault();
                return;
            }


            var pvpConfig = gameConfigResponse.data.pvp;

            var discountTicketsBuyPrice = pvpConfig.ticket_price * ticketBuy;
            var originTicketsBuyPrice = pvpConfig.ticketOriginPrice * ticketBuy;

            UpdatePriceView(pvpConfig.ticketOriginPrice, pvpConfig.ticket_price, originTicketsBuyPrice, discountTicketsBuyPrice);

            if (ticketBuy > (int)default) onValueCustom?.Invoke();
            else onValueDefault?.Invoke();
        }


        private void UpdatePriceDefault()
        {
            UpdatePriceView(valueDefault, valueDefault, valueDefault, valueDefault);
        }

        private void UpdatePriceView(int originPrice, int discountPrice, int originCost, int discountCost)
        {
            SetPriceText(originPriceText, originPrice);
            SetPriceText(discountPriceText, discountPrice);
            SetPriceText(originCostText, originCost);
            SetPriceText(discountCostText, discountCost);

            var balance = NetworkService.playerInfo.inventory.GetMoney(MoneyType.GFruit);
            var isEnoughBalance = balance >= discountCost;
            insufficientBalance.SetActive(!isEnoughBalance);


            void SetPriceText(TMP_Text textMesh, int price) => textMesh.SetText(string.Format(textFormat, price));
        }
    }
}