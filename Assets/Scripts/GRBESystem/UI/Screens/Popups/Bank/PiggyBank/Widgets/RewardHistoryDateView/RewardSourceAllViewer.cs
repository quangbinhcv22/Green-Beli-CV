using System;
using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using Service.Client.ViewRewardDateDetail;
using TigerForge;
using UnityEngine;
using Utils;

namespace GRBESystem.UI.Screens.Popups.Bank.PiggyBank.Widgets.RewardHistoryDateView
{
    public class RewardSourceAllViewer : MonoBehaviour, IEnhancedScrollerDelegate
    {
        private readonly Dictionary<string, List<RewardHistorySourceResponse>> _cachedRewardHistorySourceResponses = new Dictionary<string, List<RewardHistorySourceResponse>>();

        [SerializeField] private ViewRewardDateDetailClientService viewRewardDateDetailClientService;
        [SerializeField] private RewardHistoryAllViewer rewardHistoryAllViewer;

        [SerializeField] [Space] private EnhancedScroller scroller;
        [SerializeField] private RewardSourceCellView cellViewTemplate;
        
        List<RewardHistorySourceResponse> _rewardHistorySources = new List<RewardHistorySourceResponse>();



        private void Awake()
        {
            EventManager.StartListening(EventName.Server.GetRewardHistoryByDate, OnGetRewardByDateResponse);
            EventManager.StartListening(EventName.Server.EndGame, OnEndGameResponse);
            
            EventManager.StartListening(EventName.Server.SkipAllGame, OnSkipAll);

            EventManager.StartListening(EventName.PlayerEvent.ViewRewardDateDetail, OnViewRewardDateDetail);
            EventManager.StartListening(EventName.PlayerEvent.CollapseRewardDateDetail, Hide);

            scroller.Delegate = this;
            
            Hide();
        }

        private void OnSkipAll()
        {
            var todayKey = DateTime.UtcNow.ToString(DateTimeUtils.FranceFormatDate);
            
            if (_cachedRewardHistorySourceResponses.ContainsKey(todayKey))
            {
                _cachedRewardHistorySourceResponses.Remove(todayKey);
            }
        }


        private void OnGetRewardByDateResponse()
        {
            var getRewardHistoryByDateResponse = NetworkService.Instance.services.getRewardHistoryByDate.Response;

            if (getRewardHistoryByDateResponse.IsError) return;
            if (getRewardHistoryByDateResponse.data.Any() is false) return;

            var responseDate = ConvertDateTimeFullFormatToOnlyDateFormat(getRewardHistoryByDateResponse.data.First().date);

            if (_cachedRewardHistorySourceResponses.ContainsKey(responseDate))
            {
                _cachedRewardHistorySourceResponses[responseDate] = getRewardHistoryByDateResponse.data;
            }
            else
            {
                _cachedRewardHistorySourceResponses.Add(responseDate, getRewardHistoryByDateResponse.data);
            }

            UpdateView();
        }

        private string ConvertDateTimeFullFormatToOnlyDateFormat(string dateTimeFullFormat)
        {
            const int onlyDateFormatLength = 10;
            return dateTimeFullFormat.Substring(0, onlyDateFormatLength);
        }

        private void OnEndGameResponse()
        {
            var endGameResponse = EndGameServerService.Response;
            if (endGameResponse.IsError) return;

            var today = DateTime.UtcNow.ToString(DateTimeUtils.FranceFormatDate);
            if (_cachedRewardHistorySourceResponses.ContainsKey(today) is false) return;

            var playerInfo = endGameResponse.data.player.Find(player => player.playerId == NetworkService.Instance.services.login.MessageResponse.data.id);
            
            var newRewardHistorySource = new RewardHistorySourceResponse()
            {
                amount = playerInfo.TotalToken, date = DateTime.UtcNow.ToString(DateTimeUtils.FullFranceFormatDate),
                type = "PVE"
            };

            _cachedRewardHistorySourceResponses[today].Add(newRewardHistorySource);
        }


        private void OnViewRewardDateDetail()
        {
            var dateKeyToView = GetDateKeyToView();

            if (_cachedRewardHistorySourceResponses.ContainsKey(dateKeyToView))
            {
                UpdateView();
            }
            else SendRequestUpdateData();
        }

        private void SendRequestUpdateData()
        {
            SendRequestUpdateData(GetDateKeyToView());
        }
        
        private void SendRequestUpdateData(string dateKey)
        {
            NetworkService.Instance.services.getRewardHistoryByDate.SendRequest(dateKey);
        }

        private string GetDateKeyToView()
        {
            return viewRewardDateDetailClientService.GetEventEmitData().dateKey;
        }


        private void UpdateView()
        {
            var drawRewardHistorySources = _cachedRewardHistorySourceResponses[GetDateKeyToView()];
            _rewardHistorySources = drawRewardHistorySources.OrderByDescending(history => history.date.ToDateTime(DateTimeUtils.FullFranceFormatDate)).ToList();

            Show();
            SortSiblingIndex();
            
            scroller.ReloadData();
        }


        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void SortSiblingIndex()
        {
            var newSiblingIndex = viewRewardDateDetailClientService.GetEventEmitData().siblingIndex;
            var debug = new List<Transform>();

            for (int i = 0; i < rewardHistoryAllViewer.rewardHistoryDateViewers.Count; i++)
            {
                debug.Add(rewardHistoryAllViewer.rewardHistoryDateViewers[i].transform);

                if (rewardHistoryAllViewer.rewardHistoryDateViewers[i].transform.GetSiblingIndex() == newSiblingIndex)
                {
                    debug.Add(transform);
                }
            }

            for (int i = 0; i < debug.Count; i++)
            {
                debug[i].SetSiblingIndex(i);
            }
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        
        
        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _rewardHistorySources.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 68f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (RewardSourceCellView) scroller.GetCellView(cellViewTemplate);
            cellView.UpdateView(_rewardHistorySources[dataIndex]);

            return cellView;
        }
    }
}