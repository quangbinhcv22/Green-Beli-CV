using System;
using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace UI.Widget.Time
{
    [RequireComponent(typeof(TMP_Text))]
    public class CountdownNextLotteryCycleText : MonoBehaviour
    {
        [SerializeField] private string countdownFormat = "hh\\:mm\\:ss";
        [SerializeField] private int cycleSeconds;
        
        [SerializeField] private Color colorOnOpenTime;
        [SerializeField] private Color colorOnCloseTime;

        private bool _hasContent;
        private TMP_Text _countdownText;
        
        

        private void Awake() => _countdownText = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            TimeManager.Instance.AddEvent(UpdateTime);

            if(_hasContent) return;
            
            OnLoadGameConfigResponse();
            EventManager.StartListening(EventName.Server.LoadGameConfig, OnLoadGameConfigResponse);
        }

        private void OnDisable()
        {
            if(TimeManager.Instance is null) return;
            TimeManager.Instance.RemoveEvent(UpdateTime);
        }
        
        private void UpdateTime(int currentSeconds)
        {
            if(cycleSeconds is (int) default) return;
            
            const int nextCycleCount = 1;
            var nextCycleSeconds = ((Mathf.Floor( (float) currentSeconds / cycleSeconds) + nextCycleCount) * cycleSeconds - currentSeconds);
            var nextCycleSecondsDuration = TimeSpan.FromSeconds(nextCycleSeconds).Duration();
            
            _countdownText.SetText(string.Format(nextCycleSecondsDuration.ToString(countdownFormat)));
            _countdownText.color = IsOpenTime() ? colorOnOpenTime : colorOnCloseTime;
            
            const int endCountdown = 1;
            if((int) nextCycleSecondsDuration.TotalSeconds is endCountdown) EventManager.EmitEvent(EventName.PlayerEvent.EndCountdownLottery);
            if(nextCycleSecondsDuration == GetOpenTime()) EventManager.EmitEvent(EventName.PlayerEvent.EndOpenBuyLottery);
        }

        private bool IsOpenTime()
        {
            return NetworkService.Instance.services.loadGameConfig.Response.data.lottery.IsOpenBuy();
        }
        
        private TimeSpan GetOpenTime()
        {
            return NetworkService.Instance.services.loadGameConfig.Response.data.lottery.GetOpenTime();
        }

        private void OnLoadGameConfigResponse()
        {
            var response = NetworkService.Instance.services.loadGameConfig.Response;
            if(response.IsError) return;

            _hasContent = true;
            cycleSeconds = response.data.lottery.GetRewardScheduleSeconds();
            
            EventManager.StopListening(EventName.Server.LoadGameConfig, OnLoadGameConfigResponse);
        }
    }
}