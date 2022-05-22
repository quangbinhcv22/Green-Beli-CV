using System;
using GEvent;
using TigerForge;
using UnityEngine;

public class TreeEventTimeSetter : MonoBehaviour
{
    private int whitelistTime;
    private int endTime;
    
    private DateTime timeNow;

    private void Awake()
    {
        EventManager.StartListening(EventName.Server.CheckWhiteListBuyTree, SetTimeEvent);
        EventManager.StartListening(EventName.Server.CheckWhiteListBuyTree, UpdateTimeCountdown);
    }

    private void OnEnable()
    {
        timeNow = TimeManager.Instance.ServerTime;
    }

    private void SetTimeEvent()
    {
        if (CheckWhiteListBuyTreeService.Response is null) return;

        var whitelistTime = CheckWhiteListBuyTreeService.Response.time;
        var whitelistDay = Convert.ToDateTime(whitelistTime.whitelist).ToUniversalTime();
        var endDay = Convert.ToDateTime(whitelistTime.end).ToUniversalTime();

        this.whitelistTime = TimeManager.ConvertTimeSpanToSecond(whitelistDay - timeNow);
        endTime = TimeManager.ConvertTimeSpanToSecond(endDay - timeNow);
    }

    private void UpdateTimeCountdown() => EventManager.EmitEventData(EventName.Server.BuyTreeStage, GetEventStage());
    
    private BuyTreeStage GetEventStage()
    {
        var isWhitelistTimeStart = whitelistTime >= 0;
        var isEndTime = endTime <= 0;
    
        if (isEndTime) return BuyTreeStage.TimeOut;
        return isWhitelistTimeStart ? BuyTreeStage.WhiteListStart : BuyTreeStage.WhiteListEnd;
    }
}

[Serializable]
public enum BuyTreeStage
{
    WhiteListStart = 1,
    WhiteListEnd = 2,
    SoldOut = 3,
    TimeOut = 4,
}