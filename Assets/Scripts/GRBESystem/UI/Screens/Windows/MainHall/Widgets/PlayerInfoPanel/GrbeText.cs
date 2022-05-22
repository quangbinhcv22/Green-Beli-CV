using System;
using Cysharp.Threading.Tasks;
using GEvent;
using Manager.Inventory;
using Network.Service;
using Network.Web3;
using TMPro;
using UnityEngine;
using Utils;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GrbeText : MonoBehaviour
{
    [SerializeField] private string defaultValue = "-";
    
    private TextMeshProUGUI _textValue;

    private void Awake()
    {
        _textValue = GetComponentInChildren<TextMeshProUGUI>(true);
        _textValue.SetText(defaultValue);
    }
    
    private bool isUpdating;
        
    private async void OnEnable()
    {
        isUpdating = true;
        
        while (isUpdating && NetworkService.playerInfo.address != null)
        {
            SetMoney(await Web3Controller.Instance.GrbeToken.GetToken());
            await UniTask.Delay(TimeSpan.FromSeconds(60), true);
        }
    }
    
    private void SetMoney(int value)
    {
        NetworkService.playerInfo.inventory.SetMoney(MoneyType.Grbe, value);
        _textValue.text = NumberUtils.FormattedNumber(value);
    }
    
    private void OnDisable()
    {
        isUpdating = false;
    }
}
