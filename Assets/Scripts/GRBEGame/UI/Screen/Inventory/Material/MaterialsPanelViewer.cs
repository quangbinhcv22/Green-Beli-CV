using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using GEvent;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Material
{
    [DefaultExecutionOrder(500)]
    public class MaterialsPanelViewer : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller enhanceScroller;
        [SerializeField] private MaterialCellViewGroup materialPrefab;
        
        private List<MaterialInfo> _dataList;
        private List<MaterialResponse> _materialResponseList;
        private int _numberItemInPerRow;


        private void Awake()
        {
            _dataList ??= new List<MaterialInfo>();
            _numberItemInPerRow = materialPrefab.GetNumberItemInPerCellView();
            enhanceScroller.Delegate = this;
        }

        private void OnEnable()
        {
            OnLoadMaterialInventory();
            EventManager.StartListening(EventName.Server.LoadInventory, OnLoadMaterialInventory);
        }
        
        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.LoadInventory, OnLoadMaterialInventory);
        }

        private void OnLoadMaterialInventory()
        {
            var response = LoadInventoryServerService.Response;
            if (response.IsError) return;
            
            _materialResponseList = response.data.materials;
            LoadData();
        }

        private void LoadData()
        {
            _dataList = _materialResponseList.Select(response => new MaterialInfo(response)).ToList();
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
            var cellView = (MaterialCellViewGroup) scroller.GetCellView(materialPrefab);
            cellView.name = $"Cell {dataIndex * _numberItemInPerRow} to {dataIndex * _numberItemInPerRow + _numberItemInPerRow - 1}";
            cellView.UpdateView(_dataList, dataIndex * _numberItemInPerRow);

            return cellView;
        }
    }
}
