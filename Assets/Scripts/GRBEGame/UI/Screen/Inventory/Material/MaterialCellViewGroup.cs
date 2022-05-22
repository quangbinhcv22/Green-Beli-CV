using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Material
{
    public class MaterialCellViewGroup : EnhancedScrollerCellView
    {
        [SerializeField] private List<MaterialCoreView> materialItemCoreViews;

        public void UpdateView(List<MaterialInfo> dataList, int startIndex)
        {
            for (int i = 0; i < materialItemCoreViews.Count; i++)
            {
                var data = startIndex + i < dataList.Count ? dataList[startIndex + i] : null;

                if (data is null) materialItemCoreViews[i].UpdateDefault();
                else materialItemCoreViews[i].UpdateView(data);
            }
        }

        public int GetNumberItemInPerCellView()
        {
            return materialItemCoreViews.Count;
        }
    }
}
