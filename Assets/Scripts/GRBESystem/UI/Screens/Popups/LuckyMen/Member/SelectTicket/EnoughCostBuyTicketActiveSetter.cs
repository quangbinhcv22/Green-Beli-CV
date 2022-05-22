using System.Collections.Generic;
using GEvent;
using Manager.Inventory;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    public class EnoughCostBuyTicketActiveSetter : MonoBehaviour
    {
        [SerializeField] private bool isActiveOnEnoughCost;

        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.SelectedTickets, OnSelectedTickets);
        }

        private void OnSelectedTickets()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn) return;

            var selectToBuyTickets = EventManager.GetData<List<long>>(EventName.PlayerEvent.SelectedTickets);
            var ticketPrice = NetworkService.Instance.services.loadGameConfig.Response.data.lottery.price;

            var isEnoughCost = NetworkService.playerInfo.inventory.GetMoney(MoneyType.GFruit) >= selectToBuyTickets.Count * ticketPrice;

            gameObject.SetActive(isEnoughCost == isActiveOnEnoughCost);
        }
    }
}