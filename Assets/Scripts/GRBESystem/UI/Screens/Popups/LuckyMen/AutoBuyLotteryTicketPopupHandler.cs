using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UIFlow;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GRBESystem.UI.Screens.Popups.LuckyMen
{
    public class AutoBuyLotteryTicketPopupHandler : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Button button;
        [SerializeField] [Space] private UIRequest request;

        private List<long> _toSelectHeroIds;


        private void Awake()
        {
            _toSelectHeroIds ??= new List<long>();
            button.onClick.AddListener(FastSelectFullTicket);
            
            EventManager.StartListening(EventName.Server.GetListHero, ReloadTicketNumber);
            EventManager.StartListening(EventName.Server.CheckExistCurrentLotteryLuckyNumber, ReloadTicketNumber);
            EventManager.StartListening(EventName.Server.HeroHasChanged, EndOpenBuyLottery);
            EventManager.StartListening(EventName.PlayerEvent.EndCountdownLottery, SendRequestCheckExistLotteryLuckyNumberToday);
        }

        private void OnEnable()
        {
            SendRequestCheckExistLotteryLuckyNumberToday();

            if (NetworkService.Instance.services.loadGameConfig.Response.data.lottery.IsOpenBuy())
                EventManager.StartListening(EventName.PlayerEvent.EndOpenBuyLottery, EndOpenBuyLottery);
            else
                EndOpenBuyLottery();
        }

        private void OnDisable()
        {
            EventManager.StartListening(EventName.PlayerEvent.EndOpenBuyLottery, EndOpenBuyLottery);
        }

        private void SendRequestCheckExistLotteryLuckyNumberToday()
        {
            var allHeroIds = NetworkService.Instance.services.getHeroList.HeroResponses.Select(hero => hero.GetID())
                .ToList();
            NetworkService.Instance.services.checkExistLotteryLuckyNumberToday.SendRequest(
                new CheckExistLotteryLuckyNumberTodayRequest() { heroIds = allHeroIds });
        }

        private void EndOpenBuyLottery()
        {
            request.action = UIAction.Close;
            request.id = UIId.AutoBuyLotteryTicket;
            request.SendRequest();
        }

        private void ReloadTicketNumber()
        {
            var response = NetworkService.Instance.services.checkExistLotteryLuckyNumberToday.Response;
            if (response.IsError) return;

            var existHeroes = response.data;
            var allHeroIds = NetworkService.Instance.services.getHeroList.HeroResponses.Select(hero => hero.GetID()).ToList();

            _toSelectHeroIds = allHeroIds.Except(existHeroes).ToList();
            slider.maxValue = _toSelectHeroIds.Count - 1;
            slider.minValue = default;
            slider.value = default;
        }
        
        private void FastSelectFullTicket()
        {
            EventManager.EmitEventData(EventName.PlayerEvent.SelectedTickets, new List<long>());
            
            var canSelectTickets = _toSelectHeroIds;
            var selectedTicketCount = (int) slider.value + 1;

            for (int i = 0; i < selectedTicketCount; i++)
            {
                var randomSelectedTicket = canSelectTickets[Random.Range(default, canSelectTickets.Count)];
                canSelectTickets.Remove(randomSelectedTicket);

                EventManager.EmitEventData(EventName.PlayerEvent.SelectTicket, randomSelectedTicket);
            }
        }
    }
}
