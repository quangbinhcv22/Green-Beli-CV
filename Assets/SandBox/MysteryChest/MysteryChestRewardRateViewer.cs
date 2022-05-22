using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace SandBox.MysteryChest
{
    [DefaultExecutionOrder(500)]
    public class MysteryChestRewardRateViewer : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private MysteryChestRewardRateFrameCellView cellView;

        private bool _hasContent;
        private List<MysteryChestRateResponse> _dataList;


        private void Awake()
        {
            _hasContent = false;
            _dataList ??= new List<MysteryChestRateResponse>();
            scroller.Delegate = this;
        }

        private void OnEnable()
        {
            if (_hasContent) return;
            
            LoadData();
            EventManager.StartListening(GEvent.EventName.Server.CalculateMysteryChestRate, LoadData);
        }

        private void OnDisable()
        {
            if(EventManager.EventExists(GEvent.EventName.Server.CalculateMysteryChestRate))
                EventManager.StopListening(GEvent.EventName.Server.CalculateMysteryChestRate, LoadData);
        }

        private void LoadData()
        {
            if (NetworkService.Instance.IsNotLogged() ||
                CalculateMysteryChestRateServerService.Response.IsError)
                return;

            _hasContent = true;
            _dataList = CalculateMysteryChestRateServerService.SortDataList();
            scroller.ReloadData();
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scrollerView, int dataIndex, int cellIndex)
        {
            var cellItem = (MysteryChestRewardRateFrameCellView) scrollerView.GetCellView(cellView);
            cellItem.name = dataIndex.ToString();
            cellItem.UpdateView(_dataList[dataIndex]);

            return cellItem;
        }
        
        public int GetNumberOfCells(EnhancedScroller scrollerView) => _dataList.Count;
        public float GetCellViewSize(EnhancedScroller scrollerView, int dataIndex) => 63;
    }
}
