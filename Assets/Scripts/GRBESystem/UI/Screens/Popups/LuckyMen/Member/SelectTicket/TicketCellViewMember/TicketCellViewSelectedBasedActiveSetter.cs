using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket.TicketCellViewMember
{
    [DefaultExecutionOrder(200)]
    public class TicketCellViewSelectedBasedActiveSetter : MonoBehaviour, ITicketCellViewMember
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
            var selectedTickets = GetSelectedTickets();
            var boughtTickets = GetBoughtTickets();

            var isSelectedSelf = selectedTickets.Contains(_ticketId);
            var isEnoughSelection = selectedTickets.Union(boughtTickets).Count() >= selectTicketConfig.maxTicket;
            
            gameObject.SetActive((isSelectedSelf || isEnoughSelection) == activeOnSelected);
        }

        private List<long> GetSelectedTickets()
        {
            var boxingSelectedTickets = EventManager.GetData(EventName.PlayerEvent.SelectedTickets);
            return (List<long>)boxingSelectedTickets ?? new List<long>();
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