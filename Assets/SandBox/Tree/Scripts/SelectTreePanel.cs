using System;
using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using GEvent;
using GNetwork;
using TigerForge;
using UnityEngine;

public class SelectTreePanel : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] private EnhancedScroller scroller;
    [SerializeField] private TreeCellView treeCellViewPrefab;
    [SerializeField] private GameObject emptyTreeListNote;

    private List<Tree> _trees;


    private void OnEnable()
    {
        EventManager.StartListening(EventName.Server.GetListTree, ReloadData);
        ReloadData();
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Server.GetListTree,ReloadData);
    }

    private void ReloadData()
    {
        if (GetListTreeServerService.Response.IsError) return;
        _trees = GetListTreeServerService.StageOneResponse;
        
        ReloadScroller();
        emptyTreeListNote.SetActive(_trees.Any() is false);
    }

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
        var cellView = (TreeCellView) scroller.GetCellView(treeCellViewPrefab);

        cellView.UpdateView(_trees[dataIndex]);
        
        return cellView;
    }
}