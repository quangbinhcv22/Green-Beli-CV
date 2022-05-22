using UnityEngine;
using UnityEngine.Events;

namespace GRBESystem.UI.Screens.Windows.Breeding.Widgets.CostCalculator
{
    public class CoinCostCalculator : MonoBehaviour
    {
        protected UnityAction<CoinCost> onUpdateView;
        
        public void AddCallbackUpdateView(ICoinCostUpdateViewer updateViewer)
        {
            onUpdateView += updateViewer.UpdateView;
        }
    }
}
