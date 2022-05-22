using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using GEvent;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank.History
{
    public class LogMysteryChestPanel : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private LogMysteryChestCellView prefab;
        [SerializeField] private TMP_Text emptyLogText;
        private List<LogMysteryResponse> _dataList;


        private void Awake()
        {
            scroller.Delegate = this;
            _dataList ??= new List<LogMysteryResponse>();
        }

        private void OnEnable()
        {
            EventManager.StartListening(EventName.Server.GetLogMysteryChest, OnLoadResponse);
            GetLogMysteryChestServerService.SendRequest();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetLogMysteryChest, OnLoadResponse);
        }
        
        private void OnLoadResponse()
        {
            if(GetLogMysteryChestServerService.Response.IsError) return;
            _dataList = GetLogMysteryChestServerService.Sort();
            
            emptyLogText.gameObject.SetActive(_dataList.Any() is false);
            
            scroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _dataList.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 160f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (LogMysteryChestCellView) scroller.GetCellView(prefab);
            cellView.UpdateView(_dataList[dataIndex]);

            return cellView;
        }
    }
}
