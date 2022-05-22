using System;
using GEvent;
using GNetwork;
using TigerForge;
using UnityEngine;


[DefaultExecutionOrder(-100)]
public class SelectedTree : MonoBehaviour
{
    [SerializeField] private TreeCellView treeCellView;

    private void OnEnable()
    {
        EventManager.StartListening(EventName.Select.Tree, OnSelectTree);
        OnSelectTree();
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Select.Tree, OnSelectTree);
    }

    private void OnSelectTree()
    {
        var nullableTree = EventManager.GetData(EventName.Select.Tree);
        
        if (nullableTree is Tree tree) treeCellView.UpdateView(tree);
        //else treeCellView.UpdateDefault();
    }
}