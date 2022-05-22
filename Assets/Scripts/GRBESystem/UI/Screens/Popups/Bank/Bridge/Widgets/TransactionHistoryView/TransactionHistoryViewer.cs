using System;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GEvent;
using GRBESystem.UI.Screens.Popups.Bank.Bridge.Widgets.TransactionHistoryView.CellView;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank.Bridge.Widgets.TransactionHistoryView
{
    public class TransactionHistoryViewer : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private TransactionHistoryCellView cellViewTemplate;
        
        private List<TransactionHistoryResponse> _transactionHistoryList;


        private void Awake()
        {
            _transactionHistoryList ??= new List<TransactionHistoryResponse>();
            scroller.Delegate = this;
        }

        private void OnEnable()
        {
            EventManager.StartListening(EventName.Server.GetTransactionHistory, OnGetTransactionHistoryResponse);
            
            if (NetworkService.Instance.services.login.IsNotLoggedIn) return;
            NetworkService.Instance.services.getTransactionHistory.SendRequest();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetTransactionHistory, OnGetTransactionHistoryResponse);
        }

        private void OnGetTransactionHistoryResponse()
        {
            var messageResponse = NetworkService.Instance.services.getTransactionHistory.Response;
            if (messageResponse.IsError) return;

            _transactionHistoryList = messageResponse.data;
            
            scroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _transactionHistoryList.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 68f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (TransactionHistoryCellView)scroller.GetCellView(cellViewTemplate);

            cellView.name = $"Cell {dataIndex}";
            cellView.UpdateView(_transactionHistoryList[dataIndex]);

            return cellView;
        }
    }
}