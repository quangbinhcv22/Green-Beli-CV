using System;
using System.Collections.Generic;
using System.Globalization;
using GEvent;
using Log;
using Network.Service;
using Pattern;
using TigerForge;
using UnityEngine;
using Utils;

public class TimeManager : Singleton<TimeManager>
{
    private const int SECOND_PER_DAY = 86400;
    private const int SECOND_PER_MINUTE = 60;
    private const int SECOND_PER_HOUR = 3600;
    private const int MINUTE_PER_HOUR = 60;
    private const int SECOND_DELAY = 15;

    private readonly List<Action<int>> updateTimeEvents = new List<Action<int>>();
    private readonly List<Action<int>> removeTimeEvents = new List<Action<int>>();
    private readonly List<Action<int>> addTimeEvents = new List<Action<int>>();

    private int _second = 0;

    public int Seconds
    {
        get => _second;
        private set => _second = value;
    }

    public DateTime ServerTime { get; private set; }

    private void Awake()
    {
        EventManager.StartListening(EventName.Server.LoadGameConfig, SetServerSeconds);
    }

    void Start()
    {
        Application.runInBackground = true;

        SetDefaultSeconds();
    }

    private int time = -1;

    private void Update()
    {
        var currentSecond = DateTime.UtcNow.Second;
        if (DateTime.UtcNow.Second != time)
        {
            time = currentSecond;
            AddPerSecond();
        }
    }

    public void AddEvent(Action<int> e)
    {
        if (removeTimeEvents.Contains(e))
        {
            removeTimeEvents.Remove(e);
        }
        else
        {
            addTimeEvents.Add(e);

            e.Invoke(Seconds);
        }
    }

    public void RemoveEvent(Action<int> e)
    {
        if (addTimeEvents.Contains(e))
        {
            addTimeEvents.Remove(e);
        }
        else
        {
            removeTimeEvents.Add(e);
        }
    }

    private void UpdateEvents()
    {
        updateTimeEvents.AddRange(addTimeEvents);
        foreach (var removeTimeEvent in removeTimeEvents)
        {
            updateTimeEvents.Remove(removeTimeEvent);
        }

        addTimeEvents.Clear();
        removeTimeEvents.Clear();

        foreach (var updateTimeEvent in updateTimeEvents)
        {
            updateTimeEvent.Invoke(Seconds);
        }
    }

    private void AddPerSecond()
    {
        AddSeconds(1);
    }

    private void AddSeconds(int quantity)
    {
        Seconds = (Seconds + quantity) % SECOND_PER_DAY;

        UpdateEvents();
    }

    private void SetDefaultSeconds()
    {
        Seconds = ConvertDateToSecond(DateTime.UtcNow) - SECOND_DELAY;
    }

    private void SetServerSeconds()
    {
        if (!NetworkService.Instance.HasServerConfig())
            return;

        var formattedServerTime = NetworkService.Instance.services.loadGameConfig.ResponseData.serverTime;
        this.ServerTime = formattedServerTime.ToDateTime(DateTimeUtils.GreenBeliFullDateFormat);

        this.Seconds = ConvertDateToSecond(ServerTime) - SECOND_DELAY;
    }

    private int ConvertDateToSecond(DateTime dateTime)
    {
        return dateTime.Hour * SECOND_PER_HOUR + dateTime.Minute * SECOND_PER_MINUTE + dateTime.Second;
    }

    public static string FormattedTimeSpan(TimeSpan timeSpan)
    {
        return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }

    public static string FormattedSeconds(int seconds)
    {
        int second = seconds % SECOND_PER_MINUTE;
        int hour = seconds / SECOND_PER_HOUR;
        int minute = (seconds - hour * SECOND_PER_HOUR) / MINUTE_PER_HOUR;
        return $"{hour:D2}:{minute:D2}:{second:D2}";
    }

    public static DateTime ConvertStringToDateTime(string date)
    {
        return Convert.ToDateTime(date).ToUniversalTime();
    }
    
    public static int ConvertTimeSpanToSecond(TimeSpan dateTime)
    {
        return dateTime.Days * SECOND_PER_DAY + dateTime.Hours * SECOND_PER_HOUR + dateTime.Minutes * SECOND_PER_MINUTE + dateTime.Seconds;
    }
    
    public static int ConvertDateTimeToSecond(DateTime dateTime)
    {
        return + dateTime.Hour * SECOND_PER_HOUR + dateTime.Minute * SECOND_PER_MINUTE + dateTime.Second;
    }
}