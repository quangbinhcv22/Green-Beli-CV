using System.Collections;
using System.Collections.Generic;
using GNetwork;
using GRBEGame.Resources;
using GRBEGame.UI.DataView;
using GRBESystem.Entity.Element;
using UnityEngine;
using UnityEngine.UI;

public class TreeImage : MonoBehaviour, IMemberView<Tree>
{
    [SerializeField] private TreeCellView treeCellView;
    [SerializeField] private Image image;
    [SerializeField] private ElementArtSet artSet;
    [SerializeField] private Element elementDefault;

    private void Awake() => treeCellView.AddCallbackUpdate(this);
    
    public void UpdateDefault()
    {
        image.sprite = artSet.GetSprite(elementDefault);
    }

    public void UpdateView(Tree tree)
    {
        image.sprite = artSet.GetSprite(tree.Element);
    }
}
