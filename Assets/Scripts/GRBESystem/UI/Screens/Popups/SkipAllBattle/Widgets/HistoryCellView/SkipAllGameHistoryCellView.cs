using EnhancedUI.EnhancedScroller;
using GRBEGame.UI.Screen.Inventory;
using Network.Messages.SkipAllGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.SkipAllBattle.Widgets.HistoryCellView
{
    public class SkipAllGameHistoryCellView : EnhancedScrollerCellView
    {
        [SerializeField] private Image medal;
        [SerializeField] private TMP_Text resultBattleText;
        [SerializeField] private TMP_Text expRewardText;

        [SerializeField] [Space] private SkipGameScoreView score;
        [SerializeField] private SkipGameRewardItem item;

        [SerializeField, Space] private ResultString resultString;
        [SerializeField] private ResultSprite resultSprite;


        public void UpdateView(SkipAllGameResponse skipAllGameResponse)
        {
            medal.sprite = skipAllGameResponse.isWin ? resultSprite.win : resultSprite.lose;
            resultBattleText.SetText(skipAllGameResponse.isWin ? resultString.win : resultString.lose);
            expRewardText.SetText(skipAllGameResponse.rewardExp.ToString());

            score.UpdateView(skipAllGameResponse.totalAtkDamageScore,
                skipAllGameResponse.lastHitScore,
                skipAllGameResponse.totalAtkDamageScore + skipAllGameResponse.lastHitScore);

            if (skipAllGameResponse.rewardBeLiToken > (int) default)
                item.UpdateView(RewardItemType.BeLi, skipAllGameResponse.rewardBeLiToken);
            else if (skipAllGameResponse.rewardGFruitToken > (int) default)
                item.UpdateView(RewardItemType.GFruit, skipAllGameResponse.rewardGFruitToken);
            else if (skipAllGameResponse.rewardFragment != null)
                item.UpdateView(RewardItemType.Fragment, default,
                    new FragmentItemInfo(skipAllGameResponse.rewardFragment));
            else if (skipAllGameResponse.rewardNumberPvpTicket > (int) default)
                item.UpdateView(RewardItemType.PvpTicket, skipAllGameResponse.rewardNumberPvpTicket);
            else
                item.UpdateView();
        }
    }

    [System.Serializable]
    public struct ResultString
    {
        public string win;
        public string lose;
    }
    
    [System.Serializable]
    public struct ResultSprite
    {
        public Sprite win;
        public Sprite lose;
    }
}