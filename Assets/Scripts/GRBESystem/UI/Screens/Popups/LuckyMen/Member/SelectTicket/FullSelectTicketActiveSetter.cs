using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    public class FullSelectTicketActiveSetter : MonoBehaviour
    {
        [SerializeField] private bool isActiveOnFullSelection;
        [SerializeField] private bool isUnActiveWhenEmptyHeroList;
        // [SerializeField] private SelectTicketConfig selectConfig;

        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.SelectedTickets, OnSelectedTickets);
            EventManager.StartListening(EventName.Server.GetMyCurrentLotteryTicket, OnSelectedTickets);
        }

        private void OnSelectedTickets()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn) return;

            var selectToBuyTickets = EventManager.GetData<List<long>>(EventName.PlayerEvent.SelectedTickets);
            var boughtTickets = NetworkService.Instance.services.getMyLotteryTicketToday.GetLuckyNumbers();
            var totalHeroes =
                NetworkService.Instance.services.getHeroList.HeroResponses.Select(hero => hero.GetID()).ToList()
                    .Except(boughtTickets).Except(selectToBuyTickets);
            var isFullSelection = totalHeroes.Any() is false;

            gameObject.SetActive(isFullSelection == isActiveOnFullSelection);
            if(isUnActiveWhenEmptyHeroList && gameObject.activeSelf)
                gameObject.SetActive(HasHeroList());
        }
        
        private bool HasHeroList()
        {
            if(NetworkService.Instance.IsNotLogged()) return false;
            return NetworkService.Instance.services.getHeroList.HeroResponses.Any();
        }
    }
}
