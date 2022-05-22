using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Fragment
{
    public class FragmentItemCellView : EnhancedScrollerCellView
    {
        [SerializeField] private FragmentItemCoreView fragmentItemCoreView;
        [SerializeField] private PvpTicketCoreView pvpTicketCoreView;

        public void UpdateView(FragmentItemInfo fragmentItemInfo)
        {
            fragmentItemCoreView.UpdateView(fragmentItemInfo);
        }

        public void UpdateView(int numberPVPTicket)
        {
            var ticket = new PvpTicket()
            {
                quantity = numberPVPTicket,
            };
            pvpTicketCoreView.UpdateView(ticket);
        }
    }
}