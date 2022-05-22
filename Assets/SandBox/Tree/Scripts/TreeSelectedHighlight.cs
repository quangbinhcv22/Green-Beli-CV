using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using GRBEGame.UI.DataView;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class TreeSelectedHighlight : MonoBehaviour, IMemberView<Tree>
{
    [SerializeField] private TreeCellView owner;
    [SerializeField] private GameObject highlight;

    private void Awake() => owner.AddCallbackUpdate(this);

    private void OnEnable()
    {
        EventManager.StartListening(EventName.Select.Tree, OnSelectedTree);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Select.Tree, OnSelectedTree);
    }

    private void OnSelectedTree()
    {
        var nullableTree = EventManager.GetData(EventName.Select.Tree);

        if (nullableTree is Tree tree)
        {
            var isSelected = tree.id == owner.Tree.id;
            highlight.SetActive(isSelected);
        }
    }

    public void UpdateDefault()
    {
    }

    public void UpdateView(Tree tree)
    {
        OnSelectedTree();
    }
}