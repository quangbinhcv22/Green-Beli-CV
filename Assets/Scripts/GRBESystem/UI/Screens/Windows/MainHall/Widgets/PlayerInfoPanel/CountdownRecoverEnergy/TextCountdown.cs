using System;
using Manager.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GRBESystem.UI.Screens.Windows.MainHall.Widgets.PlayerInfoPanel.CountdownRecoverEnergy
{
    public class TextCountdown : MonoBehaviour
    {
        [SerializeField] private TMP_Text countdownText;
        [SerializeField] private string defaultCountdownContent;

        [SerializeField, Space] private TimeConfig timeConfig;


        private Func<DateTime> _fromCountdownTime;
        private Func<DateTime> _toCountdownTime;

        // private UnityAction _onEndCountdown;


        private bool CanActive => _fromCountdownTime != null && _toCountdownTime != null && isActiveAndEnabled;

        private void Awake()
        {
            countdownText.SetText(defaultCountdownContent);
        }

        private void OnEnable()
        {
            InvokeRepeating(nameof(Countdown), 0, 1);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(Countdown));
        }

        public void StartCountdown(Func<DateTime> fromDateTime, Func<DateTime> toDateTime)
        {
            _fromCountdownTime = fromDateTime;
            _toCountdownTime = toDateTime;
        }

        // public void StartCountdown(Func<DateTime> fromDateTime, Func<DateTime> toDateTime, UnityAction onEndCountdown)
        // {
        //     StartCountdown(fromDateTime, toDateTime);
        //     _onEndCountdown = onEndCountdown;
        // }

        private void Countdown()
        {
            if (_fromCountdownTime is null || _toCountdownTime is null) return;
            
            var recoverTime = _toCountdownTime.Invoke() - _fromCountdownTime.Invoke();
            countdownText.SetText(timeConfig.FormattedTimeSpan(recoverTime));

            // if (recoverTime == TimeSpan.Zero)
            // {
            //     _onEndCountdown?.Invoke();
            // }
        }
    }
}