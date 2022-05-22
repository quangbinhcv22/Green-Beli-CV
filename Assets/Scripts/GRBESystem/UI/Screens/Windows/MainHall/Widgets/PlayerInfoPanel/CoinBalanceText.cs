using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Manager.Inventory;
using Network.Service;
using Network.Web3;
using TigerForge;
using TMPro;
using UIFlow;
using UnityEngine;
using Utils;

[DefaultExecutionOrder(100)]
[RequireComponent(typeof(CoinBalanceText))]
public class CoinBalanceText : MonoBehaviour
{
    [SerializeField] private CoinType coinType;
    [SerializeField] private string defaultValue = "-";
    [SerializeField] private string textFormat = "{0}";

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _text.SetText(defaultValue);
    }


    private bool _isUpdating;

    private async void OnEnable()
    {
        _isUpdating = true;

        while (_isUpdating && NetworkService.playerInfo.address != null)
        {
            SetMoney(await coinType.GetWeb3Token().GetToken());
            await UniTask.Delay(TimeSpan.FromSeconds(60), true);
        }
    }

    private void SetMoney(int value)
    {
        NetworkService.playerInfo.inventory.SetMoney(coinType.ToMoneyType(), value);

        if (coinType == CoinType.GMeta)
        {
            _text.SetText(string.Format(textFormat, NumberUtils.FormattedNumber(value)));
        }
        else
        {
            _text.text = NumberUtils.FormattedNumber(value);
        }
        
    }

    private void OnDisable()
    {
        _isUpdating = false;
    }
}



public enum CoinType
{
    Grbe = 1,
    GMeta = 2,
    BUsd = 3,
}

[Serializable]

public static class CoinTypeExtension
{
    public static Web3Token GetWeb3Token(this CoinType coinType)
    {
        return coinType switch
        {
            CoinType.Grbe => Web3Controller.Instance.GrbeToken,
            CoinType.GMeta => Web3Controller.Instance.GmetaToken,
            CoinType.BUsd => Web3Controller.Instance.BusdToken,
            _ => throw new KeyNotFoundException(),
        };
    }

    public static MoneyType ToMoneyType(this CoinType coinType)
    {
        return coinType switch
        {
            CoinType.Grbe => MoneyType.Grbe,
            CoinType.GMeta => MoneyType.GMeta,
            CoinType.BUsd => MoneyType.BUsd,
            _ => throw new KeyNotFoundException(),
        };
    }
}