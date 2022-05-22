using EnhancedUI.EnhancedScroller;
using QB.ViewData;
using UnityEngine;

public class CellView : EnhancedScrollerCellView
{
    [SerializeField] private DataCoreView coreView;

    public void UpdateView(Tree tree)
    {
        if (coreView) coreView.UpdateView(tree);
    }

    public void UpdateDefault()
    {
        if (coreView) coreView.UpdateDefault();
    }
}
