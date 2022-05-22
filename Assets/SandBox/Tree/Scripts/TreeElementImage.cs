using GNetwork;
using GRBEGame.UI.DataView;
using GRBESystem.Entity.Element;
using UnityEngine;
using UnityEngine.UI;

public class TreeElementImage : MonoBehaviour, IMemberView<Tree>
{
    [SerializeField] private TreeCellView treeCellView;
    [SerializeField] private Image element;
    [SerializeField] private ElementArtSet treeElementArtSet;

    private void Awake() => treeCellView.AddCallbackUpdate(this);

    public void UpdateDefault()
    {
    }

    public void UpdateView(Tree tree)
    {
        element.sprite = treeElementArtSet.GetSprite(tree.Element);
    }
}

