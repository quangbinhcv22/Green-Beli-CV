using System;
using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    public class PvpSeasonCountdown : MonoBehaviour
    {
        [SerializeField] private TMP_Text textCountdown;
        [SerializeField] private long unEnablePvpFightSeconds;
        
        private int _countdownLimitSeconds;
        private int _currentSeconds;
        private bool _isUpdateTimer;
        private bool _enablePvpFight;
        

        private void OnEnable()
        {
            SetSeasonCountdownInterval();
            EventManager.StartListening(EventName.Server.GetPvpContestDetail, SetSeasonCountdownInterval);
        }

        private void OnDisable()
        {
            RemoveTimeManagerEvent();
            EventManager.StopListening(EventName.Server.GetPvpContestDetail, SetSeasonCountdownInterval);
        }
        
        private void SetSeasonCountdownInterval()
        {
            var messageResponse = NetworkService.Instance.services.getPvpContestDetail.Response;
            if (string.IsNullOrEmpty(messageResponse.error) == false) return;

            var finishedTime = messageResponse.data.finishedTime;
            var time = UnixTimeStampToDateTime(finishedTime) - DateTime.UtcNow.ToLocalTime();

            if (time.TotalSeconds <= (double) default)
                return;

            _isUpdateTimer = true;
            _enablePvpFight = true;
            _countdownLimitSeconds = (int) time.TotalSeconds;

            EmitEnablePvpFightEvent();
            TimeManager.Instance.AddEvent(UpdateSeasonCountdown);
        }
        
        private void UpdateText(string content)
        {
            textCountdown.text = content;
        }
        
        private void UpdateSeasonCountdown(int seconds)
        {
            if (_isUpdateTimer)
            {
                _currentSeconds = seconds;
                _isUpdateTimer = false;
            }

            var timer = seconds - _currentSeconds;
            if (timer > _countdownLimitSeconds)
            {
                NetworkService.Instance.services.getPvpContestDetail.SendRequest();
                return;
            }
            
            if (_countdownLimitSeconds - timer <= unEnablePvpFightSeconds && _enablePvpFight)
            {
                _enablePvpFight = false;
                EmitEnablePvpFightEvent();
            }

            UpdateText(FormattedDays(_countdownLimitSeconds - timer));
        }

        private void EmitEnablePvpFightEvent()
        {
            EventManager.EmitEventData(EventName.UI.Select<EnablePvpFight>(),
                new EnablePvpFight {enable = _enablePvpFight});
        }

        private void RemoveTimeManagerEvent()
        {
            if (TimeManager.Instance)
                TimeManager.Instance.RemoveEvent(UpdateSeasonCountdown);
        }
        
        private const int MinYear = 1970;
        private const int MinValueDateTime = 1;
        
        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var dateTime = new DateTime(MinYear, MinValueDateTime, MinValueDateTime,
                default, default, default, default, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            
            return dateTime;
        }
        
        private const int SecondPerDay = 86400;

        private string FormattedDays(int seconds)
        {
            var day = seconds / SecondPerDay;
            return $"{day:D2}d {TimeManager.FormattedSeconds(seconds - day * SecondPerDay)}";
        }
    }

    [System.Serializable]
    public class EnablePvpFight
    {
        public bool enable;
    }
}
