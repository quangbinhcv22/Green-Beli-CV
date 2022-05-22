using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class ItemInventoryCellViewGroup : EnhancedScrollerCellView
    {
        [SerializeField] private List<ItemInventoryCoreView> itemInventoryCoreViews;

        public void UpdateView(List<ItemInventory> dataList, int startIndex)
        {
            for (int i = 0; i < itemInventoryCoreViews.Count; i++)
            {
                var data = startIndex + i < dataList.Count ? dataList[startIndex + i] : null;

                if (data is null) itemInventoryCoreViews[i].UpdateDefault();
                else itemInventoryCoreViews[i].UpdateView(data);
            }
        }

        public int GetNumberItemInPerCellView()
        {
            return itemInventoryCoreViews.Count;
        }
    }
}
