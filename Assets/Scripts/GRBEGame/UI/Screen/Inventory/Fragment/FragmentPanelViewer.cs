using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Fragment
{
    public class FragmentPanelViewer : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller enhanceScroller;
        [SerializeField] private FragmentItemCellViewGroup fragmentPrefab;
        
        private List<FragmentItemInfo> _dataList;
        private List<FragmentResponse> _fragmentResponseList;
        private int _numberItemInPerRow;


        private void Awake()
        {
            _dataList ??= new List<FragmentItemInfo>();
            _numberItemInPerRow = fragmentPrefab.GetNumberItemInPerCellView();
            enhanceScroller.Delegate = this;
        }

        private void OnEnable()
        {
            OnLoadFragmentInventory();
            EventManager.StartListening(EventName.Server.LoadInventory, OnLoadFragmentInventory);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.LoadInventory, OnLoadFragmentInventory);
        }

        private void OnLoadFragmentInventory()
        {
            var response = LoadInventoryServerService.Response;
            if (response.IsError) return;
            
            _fragmentResponseList = response.data.fragments;
            LoadData();
        }

        private void LoadData()
        {
            _dataList = _fragmentResponseList.Select(response => new FragmentItemInfo(response)).ToList();
            enhanceScroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return Mathf.CeilToInt((float)_dataList.Count / _numberItemInPerRow);
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 200f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (FragmentItemCellViewGroup) scroller.GetCellView(fragmentPrefab);

            cellView.name = $"Cell {dataIndex * _numberItemInPerRow} to {dataIndex * _numberItemInPerRow + _numberItemInPerRow - 1}";
            cellView.UpdateView(_dataList, dataIndex * _numberItemInPerRow);

            return cellView;
        }
    }
}