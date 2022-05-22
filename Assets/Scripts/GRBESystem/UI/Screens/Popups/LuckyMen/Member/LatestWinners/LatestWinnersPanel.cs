using EnhancedUI.EnhancedScroller;
using GEvent;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.LatestWinners
{
    public class LatestWinnersPanel : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private LatestWinnerCellView cellViewTemplate;
        
        private GetLotteryResultByDateResponse _lotteryResult = new GetLotteryResultByDateResponse();

        
        private void Awake()
        {
            scroller.Delegate = this;
            
            EventManager.StartListening(EventName.UI.Select<GetLotteryResultByDateResponse>(), OnGetLotteryResultResponse);
        }

        private void OnGetLotteryResultResponse()
        {
            var response = EventManager.GetData(EventName.UI.Select<GetLotteryResultByDateResponse>());
            if (response is null)
                return;

            _lotteryResult = (GetLotteryResultByDateResponse) response;
            scroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _lotteryResult.winTickets?.Count ?? default;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return cellViewTemplate.GetComponent<RectTransform>().sizeDelta.y;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (LatestWinnerCellView)scroller.GetCellView(cellViewTemplate);
            cellView.UpdateView(_lotteryResult.winTickets[cellIndex], _lotteryResult.numberTicket);

            return cellView;
        }
    }
}