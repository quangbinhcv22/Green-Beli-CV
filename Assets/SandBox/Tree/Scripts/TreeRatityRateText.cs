using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using GRBEGame.UI.DataView;
using TMPro;
using UnityEngine;

public class TreeRatityRateText : MonoBehaviour, IMemberView<Tree>
{
    [SerializeField] private TreeCellView treeCellView;
    [SerializeField] private TMP_Text text;
    [SerializeField] private string textFormat = "{0}";
    [SerializeField] private string textDefault;
    private void Awake() => treeCellView.AddCallbackUpdate(this);
    public void UpdateDefault()
    {
        text.SetText(textDefault);
    }

    public void UpdateView(Tree tree)
    {
        text.SetText(string.Format(textFormat,tree.fruitRate));
    }
}
