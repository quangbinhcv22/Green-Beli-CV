using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GRBEGame.UI.Screen.Inventory.Fragment;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentItemCellViewGroup : EnhancedScrollerCellView
    {
        [SerializeField] private List<FragmentItemCoreView> fragmentItemCoreViews;

        public void UpdateView(List<FragmentItemInfo> dataList, int startIndex)
        {
            for (int i = 0; i < fragmentItemCoreViews.Count; i++)
            {
                var data = startIndex + i < dataList.Count ? dataList[startIndex + i] : null;

                if (data is null) fragmentItemCoreViews[i].UpdateDefault();
                else fragmentItemCoreViews[i].UpdateView(data);
            }
        }

        public int GetNumberItemInPerCellView()
        {
            return fragmentItemCoreViews.Count;
        }
    }
}