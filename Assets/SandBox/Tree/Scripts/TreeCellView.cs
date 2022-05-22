using EnhancedUI.EnhancedScroller;
using GRBEGame.UI.DataView;
using QB.ViewData;
using UnityEngine;
using UnityEngine.Events;

public class TreeCellView : EnhancedScrollerCellView
{
    private UnityAction<Tree> _onUpdateView;
    private UnityAction _onUpdateViewDefault;

    public Tree Tree { get; private set; }

    [SerializeField] private DataCoreView coreView;


    public void AddCallbackUpdate(IMemberView<Tree> memberView)
    {
        _onUpdateView += memberView.UpdateView;
        _onUpdateViewDefault += memberView.UpdateDefault;
    }

    public void UpdateView(Tree tree)
    {
        Tree = tree;
        _onUpdateView?.Invoke(tree);

        if (coreView) coreView.UpdateView(tree);
    }

    public void UpdateDefault()
    {
        _onUpdateViewDefault?.Invoke();

        if (coreView) coreView.UpdateDefault();
    }
}