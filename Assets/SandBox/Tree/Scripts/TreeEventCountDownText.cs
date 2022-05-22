using System;
using GEvent;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class TreeEventCountDownText : MonoBehaviour
{
    [SerializeField] private TMP_Text eventTime;
    [SerializeField] private TMP_Text countDown;
    [SerializeField] private GameObject countDownGameObject;
    [SerializeField] private Button button;
    [SerializeField] private string defaultEventText;

    private DateTime startTime;
    private DateTime timeNow;
    private int startCountDown;

    private void Awake()
    {
        button.interactable = false;
        EventManager.StartListening(EventName.Server.SetEventTime, UpdateEventTimer);
    }

    private void OnEnable() => UpdateEventTimer();

    private void OnDisable()
    {
        if (TimeManager.Instance) TimeManager.Instance.RemoveEvent(UpdateTimeCountdown);

        // try
        // {
        //     TimeManager.Instance.RemoveEvent(UpdateTimeCountdown);
        // }
        // catch (NullReferenceException)
        // {
        // }
    }

    private void UpdateEventTimer()
    {
        SetTimeEvent();
        TimeManager.Instance.AddEvent(UpdateTimeCountdown);
    }

    private void SetTimeEvent()
    {
        timeNow = TimeManager.Instance.ServerTime;

        if (CheckWhiteListBuyTreeService.Response is null) return;

        startTime = Convert.ToDateTime(CheckWhiteListBuyTreeService.Response.time.start).ToUniversalTime();
        var openTimeText = CheckWhiteListBuyTreeService.Response.info;
        SetTimeText(string.IsNullOrEmpty(openTimeText) ? defaultEventText : openTimeText.Replace("T", " "));
    }

    private void UpdateTimeCountdown(int currentSeconds)
    {
        var _startTimer = currentSeconds;
        var countDownTime = startTime - timeNow;

        var countDownTimeToSecond = TimeManager.ConvertTimeSpanToSecond(countDownTime);
        var currentCountDownSecond = TimeManager.ConvertDateTimeToSecond(startTime) - currentSeconds + _startTimer;
        var isCountDownUnderOneDay = countDownTimeToSecond - currentSeconds <= 0;

        StartCountDown(isCountDownUnderOneDay ? currentCountDownSecond : countDownTimeToSecond, currentSeconds);
    }

    private void StartCountDown(int countdownSecond, int currentSeconds)
    {
        SetText(countdownSecond, currentSeconds);
        StopCountdown(countdownSecond, currentSeconds);
        CountDownStopped(countdownSecond,currentSeconds);
    }

    private void StopCountdown(int currentCountDown, int currentSeconds)
    {
        if (currentCountDown - currentSeconds <= 0) TimeManager.Instance.RemoveEvent(UpdateTimeCountdown);
    }

    private void CountDownStopped(int currentCountDown, int currentSeconds)
    {
        if (currentSeconds - currentCountDown < 0) return;
        button.interactable = true;
        countDownGameObject.SetActive(false);
    }
    
    private void SetText(int currentCountDown, int currentSeconds)
    {
        
        countDown.SetText(currentSeconds - currentCountDown >= 0
            ? "00:00:00"
            : TimeManager.FormattedSeconds(currentCountDown - currentSeconds));
    }

    private void SetTimeText(string text)
    {
        eventTime.SetText(text.Contains("Close") ? text : text.Remove(text.Length - 5));
    }
}