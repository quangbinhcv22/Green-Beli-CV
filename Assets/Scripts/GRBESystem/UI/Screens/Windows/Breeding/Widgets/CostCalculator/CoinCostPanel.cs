using TMPro;
using UnityEngine;
using Utils;

namespace GRBESystem.UI.Screens.Windows.Breeding.Widgets.CostCalculator
{
    public class CoinCostPanel : MonoBehaviour, ICoinCostUpdateViewer
    {
        [SerializeField] private CoinCostCalculator costCalculator;
        [SerializeField] private TMP_Text gFruitText;
        [SerializeField] private TMP_Text grbeText;

        private void Awake()
        {
            costCalculator.AddCallbackUpdateView(this);
        }

        public void UpdateView(CoinCost coinCost)
        {
            gFruitText.SetText(NumberUtils.FormattedNumber(coinCost.gFruit));
            grbeText.SetText(NumberUtils.FormattedNumber(coinCost.grbe));
        }
    }
}