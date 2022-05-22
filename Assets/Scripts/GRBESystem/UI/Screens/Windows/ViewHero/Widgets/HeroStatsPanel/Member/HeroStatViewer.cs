using TMPro;
using UnityEngine;
using Utils;

namespace GRBESystem.UI.Screens.Windows.ViewHero.Widgets.HeroStatsPanel.Member
{
    public class HeroStatViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueText;

        public void UpdateView(float value)
        {
            valueText.text = NumberUtils.FormattedNumber(value, round: 2);
        }

        public void UpdateViewPercent(float value)
        {
            valueText.text = NumberUtils.FormattedPercent(value);
        }

        public void UpdateView(int currentValue, int maxValue)
        {
            valueText.text = NumberUtils.FormattedFraction(currentValue, maxValue);
        }
    }
}