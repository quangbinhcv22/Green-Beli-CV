using System;
using EnhancedUI.EnhancedScroller;
using GRBEGame.UI.Screen.Inventory;
using GRBEGame.UI.Screen.Inventory.Fragment;
using Network.Service.Implement;
using SandBox.MysteryChest;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank.History
{
    public class LogMysteryChestCellView : EnhancedScrollerCellView
    {
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private string timeTextDefault = "--:--:--";
        [SerializeField] private string timeTextFormat;
        [SerializeField] [Space] private FragmentItemCoreView gFruit;
        [SerializeField] private FragmentItemCoreView beLi;
        [SerializeField] private FragmentItemCoreView luckyPoint;
        [SerializeField] private FragmentItemCoreView fragment;


        public void UpdateView(LogMysteryResponse response)
        {
            timeText.SetText(UnixTimeStampToDateTime(response.time).ToString("f"));
            
            gFruit.gameObject.SetActive(response.reward.type is RewardMysteryType.GFRUIT);
            beLi.gameObject.SetActive(response.reward.type is RewardMysteryType.BELI);
            luckyPoint.gameObject.SetActive(response.reward.type is RewardMysteryType.MISS);
            fragment.gameObject.SetActive(response.reward.type is RewardMysteryType.LAND_FRAGMENT ||
                                          response.reward.type is RewardMysteryType.EXP_CARD_FRAGMENT ||
                                          response.reward.type is RewardMysteryType.NFT_CARD_FRAGMENT ||
                                          response.reward.type is RewardMysteryType.FUSION_STONE_FRAGMENT || 
                                          response.reward.type is RewardMysteryType.TRAINING_HOUSE_FRAGMENT);

            switch (response.reward.type)
            {
                case RewardMysteryType.GFRUIT:
                    gFruit.UpdateView(new FragmentItemInfo(default, response.reward.number));
                    break;
                case RewardMysteryType.BELI:
                    beLi.UpdateView(new FragmentItemInfo(default, response.reward.number));
                    break;
                case RewardMysteryType.MISS:
                    luckyPoint.UpdateView(new FragmentItemInfo(default, response.reward.number));
                    break;
                case RewardMysteryType.LAND_FRAGMENT:
                case RewardMysteryType.EXP_CARD_FRAGMENT:
                case RewardMysteryType.NFT_CARD_FRAGMENT:
                case RewardMysteryType.TRAINING_HOUSE_FRAGMENT:
                case RewardMysteryType.FUSION_STONE_FRAGMENT:
                    fragment.UpdateView(new FragmentItemInfo((int) response.reward.type, response.reward.number));
                    break;
            }
        }

        private const int MinYear = 1970;
        private const int MinValueDateTime = 1;
        private const int UtcSecond = 25200000;

        private static DateTime UnixTimeStampToDateTime(double seconds)
        {
            var dateTime = new DateTime(MinYear, MinValueDateTime, MinValueDateTime,
                default, default, default, default, DateTimeKind.Utc);

            dateTime = dateTime.AddMilliseconds(seconds - UtcSecond).ToLocalTime();
            return dateTime;
        }
    }
}
