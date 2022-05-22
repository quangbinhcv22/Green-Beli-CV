using System.Collections.Generic;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    [RequireComponent(typeof(Button))]
    public class ConfirmBuyTicketButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(SendBuyLotteryTicketRequest);
        }

        private static void SendBuyLotteryTicketRequest()
        {
            var selectedTickets = EventManager.GetData<List<long>>(EventName.PlayerEvent.SelectedTickets);
            NetworkService.Instance.services.buyLotteryTicket.SendRequest(new BuyLotteryTicketRequest(){ heroIds = selectedTickets});
        }
    }
}
