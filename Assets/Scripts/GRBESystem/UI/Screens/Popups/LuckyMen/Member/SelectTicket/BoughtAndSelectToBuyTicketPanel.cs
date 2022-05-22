using System;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GEvent;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    public class BoughtAndSelectToBuyTicketPanel : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller ticketsScroller;
        [SerializeField] private TicketCellView ticketCellViewTemplate;

        [SerializeField] private List<long> boughtAndSelectToBuyTickets = new List<long>();
        [SerializeField] private List<long> boughtTickets = new List<long>();
        [SerializeField] private List<long> selectToBuyTickets = new List<long>();


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.BuyLotteryTicket, OnBuyLotteryTicketResponse);
            EventManager.StartListening(EventName.Server.GetMyCurrentLotteryTicket, OnGetMyLotteryTicketTodayResponse);
            EventManager.StartListening(EventName.PlayerEvent.SelectedTickets, OnSelectedTickets);
            
            ticketsScroller.Delegate = this;
        }

        private void OnEnable()
        {
            if(NetworkService.Instance.services.login.IsNotLoggedIn) return;
            SendRequest();
            EventManager.StartListening(EventName.PlayerEvent.EndCountdownLottery, SendRequest);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.PlayerEvent.EndCountdownLottery, SendRequest);
        }


        private void SendRequest()
        {
            NetworkService.Instance.services.getMyLotteryTicketToday.SendRequest();
        }

        private void OnBuyLotteryTicketResponse()
        {
            var response = NetworkService.Instance.services.buyLotteryTicket.Response;
            if (response.IsError) return;

            SendRequest();
        }

        private void OnGetMyLotteryTicketTodayResponse()
        {
            var response = NetworkService.Instance.services.getMyLotteryTicketToday.Response;
            if (response.IsError) return;

            boughtTickets = NetworkService.Instance.services.getMyLotteryTicketToday.GetLuckyNumbers();
            UpdateBoughtAndSelectToBuyTickets();
        }

        private void OnSelectedTickets()
        {
            selectToBuyTickets = EventManager.GetData<List<long>>(EventName.PlayerEvent.SelectedTickets);
            UpdateBoughtAndSelectToBuyTickets();
        }

        private void UpdateBoughtAndSelectToBuyTickets()
        {
            boughtAndSelectToBuyTickets = new List<long>();
            boughtAndSelectToBuyTickets.AddRange(boughtTickets);
            boughtAndSelectToBuyTickets.AddRange(selectToBuyTickets);

            UpdateView();
        }

        private void UpdateView()
        {
            ticketsScroller.ReloadData();
        }


        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return boughtAndSelectToBuyTickets.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 130f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (TicketCellView)scroller.GetCellView(ticketCellViewTemplate);
            cellView.UpdateView(boughtAndSelectToBuyTickets[dataIndex]);

            return cellView;
        }
    }
}