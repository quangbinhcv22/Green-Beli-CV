using System.Collections.Generic;
using GEvent;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket.TicketCellViewMember
{
    [DefaultExecutionOrder(1000)]
    public class TicketCellViewBoughtBasedActiveSetter : MonoBehaviour, ITicketCellViewMember
    {
        [SerializeField] private TicketCellView owner;
        [SerializeField] private SelectTicketConfig selectTicketConfig;
        [SerializeField] private bool activeOnSelected;

        private long _ticketId;

        private void Awake()
        {
            owner.AddCallBackUpdateView(this);
            EventManager.StartListening(EventName.PlayerEvent.SelectedTickets, OnSelectedTickets);
        }

        private void OnEnable()
        {
            OnSelectedTickets();
        }

        private void OnSelectedTickets()
        {
            var boughtTickets = GetBoughtTickets();
            gameObject.SetActive(boughtTickets.Contains(_ticketId) == activeOnSelected);
        }

        private List<long> GetBoughtTickets()
        {
            return NetworkService.Instance.services.getMyLotteryTicketToday.GetLuckyNumbers() ?? new List<long>();
        }

        public void UpdateDefault()
        {
            gameObject.SetActive(false);
        }

        public void UpdateView(long heroId)
        {
            _ticketId = heroId;
            OnSelectedTickets();
        }
    }
}