using GRBEGame.UI.Screen.Inventory;
using GRBEGame.UI.Screen.Inventory.Fragment;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.SkipAllBattle.Widgets.HistoryCellView
{
    public class SkipGameRewardItem : MonoBehaviour
    {
        private const int Simple = 1;

        [SerializeField] private SkipGameTokenReward totalBeLit;
        [SerializeField] private SkipGameTokenReward totalGFruit;
        [SerializeField] private FragmentItemCoreView fragmentItemCoreView;
        [SerializeField] private PvpTicketCoreView pvpTicketCoreView;

        
        public void UpdateView(RewardItemType type = default, int number = Simple, FragmentItemInfo fragmentItemInfo = null)
        {
            totalBeLit.UpdateDefault();
            totalGFruit.UpdateDefault();
            fragmentItemCoreView.UpdateDefault();
            pvpTicketCoreView.UpdateDefault();

            switch (type)
            {
                case RewardItemType.BeLi:
                    totalBeLit.UpdateView(number);
                    break;
                case RewardItemType.GFruit:
                    totalGFruit.UpdateView(number);
                    break;
                case RewardItemType.Fragment:
                    fragmentItemCoreView.UpdateView(fragmentItemInfo);
                    break;
                case RewardItemType.PvpTicket:
                    pvpTicketCoreView.UpdateView(new PvpTicket {quantity = number});
                    break;
                case RewardItemType.None:
                default:
                    break;
            }
        }
    }
    
    [System.Serializable]
    public enum RewardItemType
    {
        None = 0,
        BeLi = 1,
        GFruit = 2,
        Fragment = 3,
        PvpTicket = 4,
    }
}
