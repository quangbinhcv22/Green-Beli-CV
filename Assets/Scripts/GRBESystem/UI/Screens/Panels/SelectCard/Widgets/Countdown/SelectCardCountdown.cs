using GEvent;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Panels.SelectCard.Widgets.Countdown
{
    public class SelectCardCountdown : MonoBehaviour
    {
        const int SecondsInMinutes = 60;

        [SerializeField, Space] private TMP_Text timeText;
        [SerializeField] private Color safeColor;
        [SerializeField] private Color quickColor;
        [SerializeField] private float quickTime = 10;
        [SerializeField] private CountdownType countdownType;

        private enum CountdownType
        {
            OnSelect = 0,
            AfterSelect = 1,
        }

        private int _nextCountdownSeconds;

        private void Awake()
        {
            if (countdownType is CountdownType.AfterSelect) gameObject.SetActive(false);

            EventManager.StartListening(EventName.Server.StartRound, StartCountdown);

            EventManager.StartListening(EventName.Server.SelectCard, OnSelectCard);
            EventManager.StartListening(EventName.Server.AttackBoss, OnAttackBoss);
        }

        private void StartCountdown()
        {
            gameObject.SetActive(countdownType is CountdownType.OnSelect);

            var secondsCounting =  StartRoundServerService.Response.data.RoundSecondsOut;
            _nextCountdownSeconds = TimeManager.Instance.Seconds + secondsCounting;

            TimeManager.Instance.AddEvent(UpdateCountdown);
        }

        private void UpdateCountdown(int value)
        {
            var seconds = _nextCountdownSeconds - value;
            var isQuickTime = seconds <= quickTime;

            timeText.SetText(GetFormattedTimeString(seconds));
            timeText.color = isQuickTime ? quickColor : safeColor;
        }

        private void OnSelectCard()
        {
            {
                if (countdownType is CountdownType.AfterSelect)
                {
                    gameObject.SetActive(true);
                    return;
                }

                StopCountdown();
            }
        }

        private void OnAttackBoss()
        {
            gameObject.SetActive(false);
            StopCountdown();
        }

        private void StopCountdown()
        {
            TimeManager.Instance.RemoveEvent(UpdateCountdown);
        }

        private string GetFormattedTimeString(float seconds)
        {
            seconds = (int)seconds;
            return $"{Mathf.Floor(seconds / SecondsInMinutes):00} : {(seconds % SecondsInMinutes):00}";
        }
    }
}