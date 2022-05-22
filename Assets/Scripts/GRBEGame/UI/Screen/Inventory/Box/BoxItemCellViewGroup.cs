using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory
{
    public class BoxItemCellViewGroup : EnhancedScrollerCellView
    {
        [SerializeField] private List<BoxItemCoreView> boxItemCoreViews;

        public void UpdateView(List<BoxItemInfo> dataList, int startIndex)
        {
            for (int i = 0; i < boxItemCoreViews.Count; i++)
            {
                var data = startIndex + i < dataList.Count ? dataList[startIndex + i] : null;

                if (data is null) boxItemCoreViews[i].UpdateDefault();
                else boxItemCoreViews[i].UpdateView(data);
            }
        }

        public int GetNumberItemInPerCellView()
        {
            return boxItemCoreViews.Count;
        }
    }
}
