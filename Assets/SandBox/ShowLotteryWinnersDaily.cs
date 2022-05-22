using System;
using Network.Service;
using UIFlow;
using UnityEngine;
using Utils;

public class ShowLotteryWinnersDaily : MonoBehaviour
{
    private const string ShowDateKey = "LastLotteryWinners";

    [SerializeField] private UIRequest request;

    public void Show()
    {
        if (NetworkService.Instance.services.login.MessageResponse.IsError) return;

        var todayDate = DateTime.UtcNow.ToString(DateTimeUtils.FranceFormatDate);
        if (HaveShowed(todayDate)) return;
        
        PlayerPrefs.SetString(ShowDateKey, todayDate);
        request.SendRequest();
    }

    private static bool HaveShowed(string date)
    {
        return PlayerPrefs.HasKey(ShowDateKey) && (PlayerPrefs.GetString(ShowDateKey) == date);
    }
}