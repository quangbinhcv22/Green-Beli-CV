using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.Breeding.Widgets.CostCalculator.User
{
    public class CoinCostInsufficientBalanceText : MonoBehaviour, ICoinCostUpdateViewer
    {
        [SerializeField] private CoinCostCalculator costCalculator;
        [SerializeField] private bool isActiveDefault;

        private void Awake()
        {
            costCalculator.AddCallbackUpdateView(this);
            gameObject.SetActive(isActiveDefault);
        }

        public void UpdateView(CoinCost coinCost)
        {
            gameObject.SetActive(coinCost.IsEnoughCoinCost() is false);
        }
    }
}