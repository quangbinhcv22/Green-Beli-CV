using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using GEvent;
using Manager.UseFeaturesPermission;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    public class SelectTicketPanel : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller ticketsScroller;
        [SerializeField] private TicketCellView ticketCellViewTemplate;

        [SerializeField] private List<long> toSelectHeroIds = new List<long>();


        private void Awake()
        {
            ticketsScroller.Delegate = this;

            EventManager.StartListening(EventName.Server.BuyLotteryTicket, SendRequestCheckExistLotteryLuckyNumberToday);
            EventManager.StartListening(EventName.Server.GetListHero, SendRequestCheckExistLotteryLuckyNumberToday);
        }

        private void OnEnable()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn) return;
            EventManager.StartListening(EventName.Server.CheckExistCurrentLotteryLuckyNumber, OnCheckExistLotteryLuckyNumberTodayResponse);
            EventManager.StartListening(EventName.PlayerEvent.EndCountdownLottery, SendRequestCheckExistLotteryLuckyNumberToday);

            SendRequestCheckExistLotteryLuckyNumberToday();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.CheckExistCurrentLotteryLuckyNumber, OnCheckExistLotteryLuckyNumberTodayResponse);
            EventManager.StopListening(EventName.PlayerEvent.EndCountdownLottery, SendRequestCheckExistLotteryLuckyNumberToday);

        }

        private void SendRequestCheckExistLotteryLuckyNumberToday()
        {
            var allHeroIds = NetworkService.Instance.services.getHeroList.HeroResponses.Select(hero => hero.GetID())
                .ToList();
            NetworkService.Instance.services.checkExistLotteryLuckyNumberToday.SendRequest(
                new CheckExistLotteryLuckyNumberTodayRequest() { heroIds = allHeroIds });
        }

        private void OnCheckExistLotteryLuckyNumberTodayResponse()
        {
            if (PermissionUseFeature.CanUse(FeatureId.LuckyGreenbie) is false)
            {
                toSelectHeroIds = new List<long>();
                ticketsScroller.ReloadData();

                return;
            }
            
            var response = NetworkService.Instance.services.checkExistLotteryLuckyNumberToday.Response;
            if (response.IsError) return;

            var existHeroes = response.data;
            var allHeroIds = NetworkService.Instance.services.getHeroList.HeroResponses.Select(hero => hero.GetID()).ToList();

            // toSelectHeroIds = allHeroIds.Except(existHeroes).ToList();
            toSelectHeroIds = allHeroIds;
            existHeroes.ForEach(hero =>
            {
                if (toSelectHeroIds.Contains(hero))
                    toSelectHeroIds.Remove(hero);
            });
            
            ticketsScroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return toSelectHeroIds.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 130f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (TicketCellView)scroller.GetCellView(ticketCellViewTemplate);
            cellView.UpdateView(toSelectHeroIds[dataIndex]);

            return cellView;
        }
    }
}