using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GEvent;
using GRBESystem.UI.Screens.Popups.SkipAllBattle.Widgets.HistoryCellView;
using Network.Messages.SkipAllGame;
using Network.Service;
using Network.Service.Implement;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.SkipAllBattle.Widgets.StatisticsScrollView
{
    public class SkipAllGameStatisticsView : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private SkipAllGameHistoryCellView skipAllGameHistoryPrefab;

        private List<SkipAllGameResponse> _statisticResponses;


        private void OnEnable()
        {
            OnGetTransactionHistoryResponse();
        }

        private void OnGetTransactionHistoryResponse()
        {
            var messageResponse = SkipAllGameServerService.Response;
            if (messageResponse.IsError) return;

            _statisticResponses = messageResponse.data;

            scroller.Delegate = this;
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _statisticResponses.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 68f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (SkipAllGameHistoryCellView) scroller.GetCellView(skipAllGameHistoryPrefab);
            cellView.UpdateView(_statisticResponses[dataIndex]);

            return cellView;
        }
    }
}