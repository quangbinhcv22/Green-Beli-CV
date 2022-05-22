using System;
using GEvent;
using Network.Messages.LoadGame;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;
using Utils;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.TodayReward
{
    public class TodayRewardPanel : MonoBehaviour
    {
        private LoadGameConfigResponse.Lottery LotteryConfig => NetworkService.Instance.services.loadGameConfig.ResponseData.lottery;

        private static string TodayDateKey => DateTime.UtcNow.ToString(DateTimeUtils.FullFranceFormatDate);

        [SerializeField] private TMP_Text totalPriceText;
        [SerializeField] private TMP_Text totalSoldTicketText;
        [SerializeField] private string stringFormat = "{0}";
        

        private void Awake()
        {
            EventManager.StartListening(EventName.UI.Select<GetLotteryResultByDateResponse>(), UpdateView);
        }

        private void UpdateView()
        {
            var response = GetLotteryResultByDateServerService.Response;
            if (response.IsError) return;
            
            if(TodayDateKey.ToDateTime(DateTimeUtils.FullFranceFormatDate) != response.data.GetDate) return;
            
            EventManager.StopListening(EventName.Server.GetLotteryResult, UpdateView);
            
            var ticketCount = response.data.numberTicket;
            var totalTicketsPrice = LotteryConfig.GetTotalPriceTickets(ticketCount);

            totalSoldTicketText.SetText(string.Format(stringFormat, ticketCount));
            totalPriceText.SetText(string.Format(stringFormat, totalTicketsPrice));
        }
    }
}