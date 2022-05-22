using System.Collections.Generic;
using System.Linq;
using GEvent;
using Manager.Inventory;
using Manager.UseFeaturesPermission;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    public class BuyTicketConfirmationButtonInteroperabilitySetter : MonoBehaviour
    {
        [SerializeField] private Button confirmButton;

        private void Awake()
        {
            Assert.IsNotNull(confirmButton);

            EventManager.StartListening(EventName.PlayerEvent.SelectedTickets, OnSelectedTickets);
        }

        private void OnSelectedTickets()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn) return;

            var selectToBuyTickets = EventManager.GetData<List<long>>(EventName.PlayerEvent.SelectedTickets);
            var ticketPrice = NetworkService.Instance.services.loadGameConfig.Response.data.lottery.price;

            var haveSelectTicket = selectToBuyTickets.Any();
            var isEnoughCost = NetworkService.playerInfo.inventory.GetMoney(MoneyType.GFruit) >= selectToBuyTickets.Count * ticketPrice;

            confirmButton.interactable = haveSelectTicket && isEnoughCost && PermissionUseFeature.CanUse(FeatureId.LuckyGreenbie);
        }
    }
}