using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GRBEGame.UI.Screen.Inventory;
using GRBEGame.UI.Screen.Inventory.Fragment;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.SkipAllBattle.Widgets.Report
{
    public class SkipAllItemFragmentRewardPanel : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private FragmentItemCellView fragmentItemCellView;

        private int _rewardNumberPVPTicket = 0;
        private List<FragmentItemInfo> _fragmentItems = new List<FragmentItemInfo>();

        public void LoadData(List<FragmentItemInfo> fragmentItems, int rewardNumberPVPTicket = default)
        {
            _rewardNumberPVPTicket = rewardNumberPVPTicket;
            _fragmentItems = fragmentItems;
            
            scroller.Delegate = this;
        }


        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            var count = _rewardNumberPVPTicket == default ? default : 1;
            return _fragmentItems.Count + count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 100f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (FragmentItemCellView)scroller.GetCellView(fragmentItemCellView);
            if(dataIndex >= _fragmentItems.Count)
                cellView.UpdateView(_rewardNumberPVPTicket);
            else
                cellView.UpdateView(_fragmentItems[dataIndex]);

            return cellView;
        }
    }
}