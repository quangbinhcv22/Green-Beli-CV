using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class BuyTreePanel : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] private EnhancedScroller scroller;
    [SerializeField] private TreeCellView cellViewPrefab;
    [SerializeField] private List<Tree> _trees;
    
    private void Awake() => ReloadScroller();
    
    private void ReloadScroller()
    {
        if (scroller.Delegate is null)
        {
            scroller.Delegate = this;
        }
        else
        {
            scroller.ReloadData();
        }
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _trees.Count;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 210f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        var cellView = (TreeCellView) scroller.GetCellView(cellViewPrefab);

        cellView.UpdateView(_trees[dataIndex]);
        
        return cellView;
    }
}
