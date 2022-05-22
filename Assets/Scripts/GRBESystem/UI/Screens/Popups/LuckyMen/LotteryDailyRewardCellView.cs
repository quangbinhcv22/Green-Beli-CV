using EnhancedUI.EnhancedScroller;
using TMPro;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.LuckyMen
{
    public class LotteryDailyRewardCellView : EnhancedScrollerCellView
    {
        [SerializeField] private Image rank;
        [SerializeField] private TMP_Text rankText;
        [SerializeField] private TMP_Text gFruit;
        [SerializeField] [Space] private LuckyGreenbieRankArtSet rankArtSet;


        public void UpdateView(int top, long token)
        {
            rank.gameObject.SetActive(top <= rankArtSet.GetMaxCount());
            rankText.gameObject.SetActive(top > rankArtSet.GetMaxCount());
            
            if (top <= rankArtSet.GetMaxCount())
                rank.sprite = rankArtSet.GetRankIcon(top);
            else
                rankText.SetText(top.ToString());

            gFruit.SetText(token.ToString("N0"));
        }
    }
}
