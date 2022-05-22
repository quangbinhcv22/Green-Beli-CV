using Config.Format;
using EnhancedUI.EnhancedScroller;
using Network.Service;
using Network.Service.Implement;
using TMPro;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.LatestWinners
{
    public class LatestWinnerCellView : EnhancedScrollerCellView
    {
        [SerializeField] private Image rank;
        [SerializeField] private TMP_Text rankText;
        [SerializeField] private TMP_Text addressText;
        [SerializeField] private TMP_Text gFruit;

        [SerializeField] [Space] private PlayerAddressFormatConfig addressFormatConfig;
        [SerializeField] private LuckyGreenbieRankArtSet rankArtSet;

        private const float RateReward = 0.5f;
        private const float RateJackReward = 0.2f;
        

        public void UpdateView(WinTicketResponse lotteryResult, int ticketCount)
        {
            rank.gameObject.SetActive(lotteryResult.isJackpot is false && lotteryResult.winOrder <= rankArtSet.GetMaxCount());
            rankText.gameObject.SetActive(lotteryResult.isJackpot || lotteryResult.winOrder > rankArtSet.GetMaxCount());

            if (lotteryResult.winOrder <= rankArtSet.GetMaxCount())
                rank.sprite = rankArtSet.GetRankIcon(lotteryResult.winOrder);
            else
                rankText.SetText(lotteryResult.isJackpot ? "Jackpot" : lotteryResult.winOrder.ToString());
            
            addressText.SetText(addressFormatConfig.FormattedAddress(lotteryResult.owner));
            
            var gameConfig = NetworkService.Instance.services.loadGameConfig.Response;
            if (gameConfig.IsError) return;

            if (lotteryResult.isJackpot is false)
                gFruit.SetText(gameConfig.data.lottery.GetGFruitReward(
                        (int) (gameConfig.data.lottery.GetTotalPriceTickets(GetLotteryResultByDateServerService.Response.data.numberTicket) * RateReward),
                        lotteryResult.winOrder).ToString("N0"));
            else
                gFruit.SetText((GetLotteryDetailServerService.Response.data.poolRewardGfrJackpot -
                                gameConfig.data.lottery.GetTotalPriceTickets((int) GetLotteryDetailServerService
                                    .Response.data.totalSoldTicketOfDay) * RateJackReward).ToString("N0"));
        }
    }
}