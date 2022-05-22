using Network.Service.Implement;
using TMPro;
using UI.ArtVisual;
using UI.Widget.HeroCard;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.WinThePrice
{
    public class LotteryRewardInfoPanel : MonoBehaviour
    {
        [SerializeField] private GameObject jackpot;
        [SerializeField] private Image top1;
        [SerializeField] private TMP_Text topText;
        [SerializeField] [Space] private HeroVisual ticketAvatar;
        [SerializeField] private LuckyGreenbieRankArtSet artSet;
        
        [SerializeField][Space] private TMP_Text ticketIdText;
        [SerializeField] private string ticketIdFormat = "#{0}";

        [SerializeField] [Space] private TMP_Text gFruitRewardText;
        
        
        public UnityAction onDisable;
        private bool _isEnable;

        private void OnEnable()
        {
            _isEnable = true;
        }

        private void OnDisable()
        {
            if (_isEnable is false) return;

            _isEnable = false;
            onDisable?.Invoke();
        }

        public void UpdateView(WinLotteryResponse winLotteryResponse)
        {
            ticketAvatar.UpdateView(winLotteryResponse.luckyNumber);

            top1.gameObject.SetActive(winLotteryResponse.type == "DAILY" && winLotteryResponse.winOrder <= artSet.GetMaxCount());
            topText.gameObject.SetActive(winLotteryResponse.type == "DAILY" && winLotteryResponse.winOrder > artSet.GetMaxCount());
            jackpot.SetActive(winLotteryResponse.type == "JACKPOT");

            if (winLotteryResponse.type == "DAILY" && winLotteryResponse.winOrder <= artSet.GetMaxCount())
                top1.sprite = artSet.GetRankIcon(winLotteryResponse.winOrder);
                    
            topText.SetText(winLotteryResponse.winOrder.ToString());
            ticketIdText.SetText(string.Format(ticketIdFormat, winLotteryResponse.luckyNumber));
            gFruitRewardText.SetText(winLotteryResponse.rewardGfrToken.ToString("N0"));
        }
    }
}
