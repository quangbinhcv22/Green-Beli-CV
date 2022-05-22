using EnhancedUI.EnhancedScroller;
using Network.Service.Implement;
using TMPro;
using UnityEngine;
using Utils;

namespace GRBESystem.UI.Screens.Popups.Bank.PiggyBank.Widgets.RewardHistoryDateView
{
    public class RewardSourceCellView : EnhancedScrollerCellView
    {
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text gFruitText;
        [SerializeField] private LockRewardSourceDescriptionLocalizeConfig localizeConfig;

        public void UpdateView(RewardHistorySourceResponse sourceData)
        {
            timeText.SetText(sourceData.date.ToDateTime(DateTimeUtils.FullFranceFormatDate).ToString("hh:mm:ss tt"));

            descriptionText.SetText(localizeConfig.config[sourceData.type]);
            gFruitText.SetText($"{sourceData.amount:N0}");
        }
    }
}