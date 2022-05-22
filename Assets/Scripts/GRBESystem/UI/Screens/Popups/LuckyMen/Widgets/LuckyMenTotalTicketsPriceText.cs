using System.Collections.Generic;
using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;


namespace GRBESystem.UI.Screens.Popups.LuckyMen
{
    public class LuckyMenTotalTicketsPriceText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;


        private void Awake()
        {
            Assert.IsNotNull(text);
            EventManager.StartListening(EventName.PlayerEvent.SelectedTickets, UpdateView);
        }

        private void UpdateView()
        {
            if (EventManager.GetData(EventName.PlayerEvent.SelectedTickets) == null ||
                NetworkService.Instance.services.loadGameConfig.Response.data == null) return;
            
            var tickets = EventManager.GetData<List<long>>(EventName.PlayerEvent.SelectedTickets);
            var price = NetworkService.Instance.services.loadGameConfig.Response.data.lottery.price;
            var totalPrice = tickets.Count * price;
            
            text.SetText(totalPrice.ToString());
        }
    }
}