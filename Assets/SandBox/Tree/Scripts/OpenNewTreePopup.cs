using System;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using QB.ViewData;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class OpenNewTreePopup : MonoBehaviour
{
    [SerializeField] private DataCoreView cellView;
    [SerializeField] private TMP_Text note;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private string textNoteFormat = "{0}";
    [SerializeField] private string textDefault;

    private List<Tree> _trees;
    private int _currentViewTreeIndex;
    private const int OnlyTree = 1;
    private const int StopViewPoint = 0;

    private bool _firstUpdate;
    
    private void Awake()
    {
        ReloadData();
        openButton.onClick.AddListener(ShowPreviousTree);
        EventManager.StartListening(EventName.Server.HaveNewTree, ReloadData);
    }

    private void OnEnable()
    {
        ResetButton();
        if(_firstUpdate is false) ReloadData();
    }

    private void ReloadData()
    {
        if (TreeHasChangedServerService.Response.IsError) return;
        ResetButton();
        _trees = GetListTreeServerService.NewTrees();
        
        _currentViewTreeIndex = _trees.Count - OnlyTree;
        ShowTreeDetail(_currentViewTreeIndex);
        NextViewTreeIndex();
        
        _firstUpdate = true;
    }

    private void ShowPreviousTree()
    {
        ShowTreeDetail(_currentViewTreeIndex);
        NextViewTreeIndex();
    }

    private void ShowTreeDetail(int viewIndex)
    {
        if (viewIndex < StopViewPoint)
        {
            EventManager.EmitEvent(EventName.Select.AllTreesOpened);
            return;
        }
        
        if (viewIndex > OnlyTree) note.SetText(string.Format(textNoteFormat,viewIndex));
        else if (IsOnlyTree(viewIndex)) note.SetText(string.Format(textNoteFormat,OnlyTree));
        else
        {
            note.SetText(textDefault);
            openButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
        }
        
        var tree = _trees[viewIndex];
        cellView.UpdateView(tree);
    }

    private void NextViewTreeIndex()
    {
        _currentViewTreeIndex--;
    }
    
    private bool IsOnlyTree(int buyTreeQuantity)
    {
        return buyTreeQuantity == OnlyTree;
    }

    private void ResetButton()
    {
        openButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(false);
    }
    // private void OnValidate()
    // {
    //     if (openButton is null)
    //         Debug.Log($"I'm <color=yellow>{name}</color>.<color=yellow>open</color> is <color=yellow>null</color>");
    // }
}