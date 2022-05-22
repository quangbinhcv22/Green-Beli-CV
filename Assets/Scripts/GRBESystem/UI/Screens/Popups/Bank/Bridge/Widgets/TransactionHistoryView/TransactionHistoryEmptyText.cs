using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

public class TransactionHistoryEmptyText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string formatString;


    private void OnEnable()
    {
        UpdateView();
        EventManager.StartListening(EventName.Server.GetTransactionHistory, UpdateView);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Server.GetTransactionHistory, UpdateView);
    }

    private void UpdateView()
    {
        var getRewardAllResponse = NetworkService.Instance.services.getTransactionHistory.Response;
        if (getRewardAllResponse.IsError) return;
        var rewardHistoryAllResponse = getRewardAllResponse.data;

        text.SetText(rewardHistoryAllResponse.Any() ? string.Empty : formatString);
        SetActive(rewardHistoryAllResponse.Any() is false);
    }

    private void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
