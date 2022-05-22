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
    public class LogPvpPanel : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private LogPvpCellView prefab;
        [SerializeField] private TMP_Text emptyLogText;
        private List<LogPvpResponse> _dataList;


        private void Awake()
        {
            scroller.Delegate = this;
            _dataList ??= new List<LogPvpResponse>();
        }

        private void OnEnable()
        {
            EventManager.StartListening(EventName.Server.GetLogPvp, OnLoadResponse);
            GetLogPvpServerService.SendRequest();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetLogPvp, OnLoadResponse);
        }
        
        private void OnLoadResponse()
        {
            if(GetLogPvpServerService.Response.IsError) return;
            _dataList = GetLogPvpServerService.Sort();
            
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
            var cellView = (LogPvpCellView) scroller.GetCellView(prefab);
            cellView.UpdateView(_dataList[dataIndex]);

            return cellView;
        }
    }
}
