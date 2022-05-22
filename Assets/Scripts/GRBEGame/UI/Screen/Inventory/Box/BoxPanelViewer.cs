using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class BoxPanelViewer : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller enhanceScroller;
        [SerializeField] private BoxItemCellViewGroup boxPrefab;
        
        private List<BoxItemInfo> _dataList;
        private List<BoxResponse> _boxResponseList;
        private List<PackResponse> _packResponseList;
        private int _numberItemInPerRow;


        private void Awake()
        {
            _dataList ??= new List<BoxItemInfo>();
            _numberItemInPerRow = boxPrefab.GetNumberItemInPerCellView();
            enhanceScroller.Delegate = this;
        }

        private void OnEnable()
        {
            OnLoadBoxInventory();
            EventManager.StartListening(EventName.Server.LoadInventory, OnLoadBoxInventory);
        }
        
        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.LoadInventory, OnLoadBoxInventory);
        }

        private void OnLoadBoxInventory()
        {
            var response = LoadInventoryServerService.Response;
            if (response.IsError) return;
            
            _boxResponseList = response.data.boxes;
            _packResponseList = response.data.packs;
            
            LoadData();
        }

        private void LoadData()
        {
            _dataList.Clear();
            _dataList.AddRange(_boxResponseList.Select(response => new BoxItemInfo(response)));
            _dataList.AddRange(_packResponseList.Select(response => new BoxItemInfo(response)));
            
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
            var cellView = (BoxItemCellViewGroup) scroller.GetCellView(boxPrefab);
            cellView.name = $"Cell {dataIndex * _numberItemInPerRow} to {dataIndex * _numberItemInPerRow + _numberItemInPerRow - 1}";
            cellView.UpdateView(_dataList, dataIndex * _numberItemInPerRow);

            return cellView;
        }
    }
}