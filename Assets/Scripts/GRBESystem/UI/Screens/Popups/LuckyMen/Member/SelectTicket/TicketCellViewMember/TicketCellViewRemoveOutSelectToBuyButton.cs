using System.Collections.Generic;
using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket.TicketCellViewMember
{
    [RequireComponent(typeof(Button))] [DefaultExecutionOrder(100)]
    public class TicketCellViewRemoveOutSelectToBuyButton : MonoBehaviour, ITicketCellViewMember
    {
        [SerializeField] private TicketCellView owner;
        [SerializeField] private long ticketId;

        private void Awake()
        {
            owner.AddCallBackUpdateView(this);
            GetComponent<Button>().onClick.AddListener(RemoveOutSelectToBuy);
        }

        public void UpdateDefault()
        {
        }

        public void UpdateView(long heroId)
        {
            ticketId = heroId;
        }

        private void RemoveOutSelectToBuy()
        {
            var selectedTickets = EventManager.GetData<List<long>>(EventName.PlayerEvent.SelectedTickets);
            if (selectedTickets.Contains(ticketId) is false) return;

            selectedTickets.Remove(ticketId);
            EventManager.EmitEventData(EventName.PlayerEvent.SelectedTickets, selectedTickets);
        }
    }
}