using Manager.Inventory;
using Network.Service;

namespace GRBESystem.UI.Screens.Windows.Breeding.Widgets.CostCalculator
{
    [System.Serializable]
    public struct CoinCost
    {
        public int gFruit;
        public int grbe;
    }

    public static class CoinCostUtils
    {
        public static bool IsEnoughCoinCost(this CoinCost coinCost)
        {
            var playerGFruit = NetworkService.playerInfo.inventory.GetMoney(MoneyType.GFruit);
            var playerGrbe = NetworkService.playerInfo.inventory.GetMoney(MoneyType.Grbe);
            
            return playerGFruit >= coinCost.gFruit && playerGrbe >= coinCost.grbe;
        }
    }
    
    public interface ICoinCostUpdateViewer
    {
        void UpdateView(CoinCost coinCost);
    }
}