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
    public class ItemPanelViewer : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller enhanceScroller;
        [SerializeField] private ItemInventoryCellViewGroup itemPrefab;

        private List<ItemInventory> _dataList;
        private List<ExpCardResponse> _expCardResponses;
        private List<FusionStoneResponse> _fusionStoneResponses;
        private List<TrainingHouseResponse> _trainingHouseResponses;
        private int _numberItemInPerRow;


        private void Awake()
        {
            _dataList ??= new List<ItemInventory>();
            _numberItemInPerRow = itemPrefab.GetNumberItemInPerCellView();
            enhanceScroller.Delegate = this;
        }

        private void OnEnable()
        {
            OnLoadItemInventory();
            EventManager.StartListening(EventName.Server.LoadInventory, OnLoadItemInventory);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.LoadInventory, OnLoadItemInventory);
        }

        private void OnLoadItemInventory()
        {
            var response = LoadInventoryServerService.Response;
            if (response.IsError) return;
            _expCardResponses = response.data.expCards;
            _fusionStoneResponses = response.data.fusionStones;
            _trainingHouseResponses = response.data.trainingHouses;

            LoadData();
        }

        private void LoadData()
        {
            _dataList.Clear();
            _dataList.AddRange(_expCardResponses.Select(ItemInventoryDirector.GetItemInventory));
            _dataList.AddRange(_fusionStoneResponses.Select(ItemInventoryDirector.GetItemInventory));
            _dataList.AddRange(_trainingHouseResponses.Select(ItemInventoryDirector.GetItemInventory));
            enhanceScroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return Mathf.CeilToInt((float) _dataList.Count / _numberItemInPerRow);
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 200f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (ItemInventoryCellViewGroup) scroller.GetCellView(itemPrefab);
            cellView.name =
                $"Cell {dataIndex * _numberItemInPerRow} to {dataIndex * _numberItemInPerRow + _numberItemInPerRow - 1}";
            cellView.UpdateView(_dataList, dataIndex * _numberItemInPerRow);

            return cellView;
        }
    }
}
