using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Messages;
using Newtonsoft.Json;
using TigerForge;
using UIFlow;
using UnityEngine;
using Utils;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(GetRewardHistoryAllServerService), menuName = "ScriptableObject/Service/Server/GetRewardHistoryAll")]
    public class GetRewardHistoryAllServerService : ScriptableObject, IDeserializeResponseMessage<List<RewardHistoryByDateResponse>>
    {
        [NonReorderable] private MessageResponse<List<RewardHistoryByDateResponse>> _response;
        public MessageResponse<List<RewardHistoryByDateResponse>> Response => _response;

        
        public float TotalClaimed { get; private set; }
        public float TotalBlocked { get; private set; }

        
        private List<RewardHistoryByDateResponse> RewardHistoryByDateResponses => _response.data;
        public bool IsAnyCanClaim => RewardHistoryByDateResponses.Any(history => history.allowedClaimDate.ToDateTime(DateTimeUtils.FranceFormatDate).IsTodayOrPast());


        public void SendRequest()
        {
            UIRequest.ShowDelayPanel.SendRequest();
            Message.Instance().SetId(GetEventName()).SetRequest(null).SetResponse(null).Send();
        }

        private string GetEventName()
        {
            return EventName.Server.GetRewardHistoryAll;
        }


        public void AddAmountRewardToday(int amount)
        {
            const int canClaimDays = 5;
            
            var todayDateKey = DateTime.UtcNow.ToString(DateTimeUtils.FranceFormatDate);
            var queryTodayRewardHistory = _response.data.Find(history => history.date == todayDateKey);

            if (queryTodayRewardHistory is null)
            {
                var canClaimDateKey = DateTime.UtcNow.AddDays(canClaimDays).ToString(DateTimeUtils.FranceFormatDate);
                var todayHistory = new RewardHistoryByDateResponse() { date = todayDateKey, amount = amount, allowedClaimDate = canClaimDateKey };

                _response.data.Add(todayHistory);
            }
            else
            {
                queryTodayRewardHistory.amount += amount;
            }

            SortHistory();
            EventManager.EmitEventData(EventName.Server.GetRewardHistoryAll, _response.data);   
        }
        

        public MessageResponse<List<RewardHistoryByDateResponse>> DeserializeResponseMessage(string message)
        {
            UIRequest.HideDelayPanel.SendRequest();

            _response = JsonConvert.DeserializeObject<MessageResponse<List<RewardHistoryByDateResponse>>>(message);
            
            if (_response.IsError is false)
            {
                TotalClaimed = _response.data.Where(history => history.isClaimed).Select(history => history.amount).Sum();
            }

            
            SortHistory();

            return _response;
        }

        
        private void SortHistory()
        {
            if (_response.IsError) return;
            
            _response.data = (from history in _response.data
                where history.isClaimed == false
                orderby history.date.ToDateTime(DateTimeUtils.FranceFormatDate)
                select history).ToList();

            TotalBlocked = _response.data.Where(history => history.isClaimed is false).Select(history => history.amount).Sum();
        }
    }

    [System.Serializable]
    public class RewardHistoryByDateResponse
    {
        public string date;
        public float amount;
        public bool isClaimed;
        public string allowedClaimDate;
    }
}