using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using Manager.UseFeaturesPermission;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    public class FullSelectTicketButtonInteractableSetter : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private bool isActiveOnFullSelection;

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

            var isFullSelection = selectToBuyTickets.Union(boughtTickets).Count() >=
                                  NetworkService.Instance.services.getHeroList.HeroResponses.Count;
            
            button.interactable = isFullSelection == isActiveOnFullSelection && PermissionUseFeature.CanUse(FeatureId.LuckyGreenbie);
        }
    }
}