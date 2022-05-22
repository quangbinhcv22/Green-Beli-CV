using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    public class SelectTicketHandler : MonoBehaviour
    {
        [SerializeField] private SelectTicketConfig selectConfig;

        private void Awake()
        {
            ResetSelection();

            EventManager.StartListening(EventName.PlayerEvent.SelectTicket, OnSelectTicket);
            EventManager.StartListening(EventName.Server.BuyLotteryTicket, OnBuyLotteryTicketResponse);
            EventManager.StartListening(EventName.Server.GetListHero, OnGetListHeroResponse);
            EventManager.StartListening(EventName.PlayerEvent.EndOpenBuyLottery, OnEndOpenBuyLottery);
        }

        private void OnEnable()
        {
            ResetSelection();
        }

        private static void EmitEventSelectedTicket(List<long> tickets)
        {
            EventManager.EmitEventData(EventName.PlayerEvent.SelectedTickets, tickets);
        }
        
        private void ResetSelection()
        {
            EmitEventSelectedTicket(new List<long>());
        }


        private void OnSelectTicket()
        {
            var newSelectedTicket = EventManager.GetData<long>(EventName.PlayerEvent.SelectTicket);
            var selectedTickets = EventManager.GetData<List<long>>(EventName.PlayerEvent.SelectedTickets);
            var boughtTickets = NetworkService.Instance.services.getMyLotteryTicketToday.GetLuckyNumbers();

            if (selectedTickets.Contains(newSelectedTicket) || (selectedTickets.Union(boughtTickets).Count() >= selectConfig.maxTicket)) return;

            selectedTickets.Add(newSelectedTicket);
            EmitEventSelectedTicket(selectedTickets);
        }

        private void OnBuyLotteryTicketResponse()
        {
            ResetSelection();
        }

        private void OnGetListHeroResponse()
        {
            var selectedTickets = EventManager.GetData<List<long>>(EventName.PlayerEvent.SelectedTickets);
            var allHeroes = NetworkService.Instance.services.getHeroList.HeroResponses.Select(hero => hero.GetID()).ToList();

            EmitEventSelectedTicket(selectedTickets.Intersect(allHeroes).ToList());
        }

        private void OnEndOpenBuyLottery()
        {
            ResetSelection();
        }
    }
}