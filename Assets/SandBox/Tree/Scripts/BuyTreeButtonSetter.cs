using System;
using GEvent;
using Manager.Inventory;
using Network.Messages.LoadGame;
using Network.Service;
using TigerForge;
using UIFlow;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class BuyTreeButtonSetter : MonoBehaviour
{
    private const long OneBillion = 1000000000;

    [SerializeField] private Button button;
    [SerializeField] private GameObject waringText;
    [SerializeField] private Slider slider;

    private long _bUsdSinglePrice;
    private long _gMetaSinglePrice;
    
    private int TreeQuantity => (int) slider.value;

    private TreePrice TotalPrice => new TreePrice()
        {BUsd = TreeQuantity * _bUsdSinglePrice, GMeta = TreeQuantity * _gMetaSinglePrice / OneBillion};

    private bool IsEnoughBalance
    {
        get
        {
            var totalPrice = TotalPrice;
            var bUsdBalance = NetworkService.playerInfo.inventory.GetMoney(MoneyType.BUsd);
            var gMetaBalance = NetworkService.playerInfo.inventory.GetMoney(MoneyType.GMeta);
            
            return bUsdBalance >= totalPrice.BUsd && gMetaBalance >= totalPrice.GMeta;
        }
    }

    private void Awake()
    {
        slider.onValueChanged.AddListener(_ => SetButtonInteractable());
        button.onClick.AddListener(EmitEventTreeQuantity);

        EventManager.StartListening(EventName.Inventory.Change, SetButtonInteractable);
    }
    

    private void SetButtonInteractable()
    {
        var isEnoughBalance = IsEnoughBalance;
        if (button.interactable == isEnoughBalance) return;
        waringText.SetActive(!isEnoughBalance);

        button.interactable = isEnoughBalance;
    }

    
    private void EmitEventTreeQuantity() =>
        EventManager.EmitEventData(EventName.Select.TreeQuantity, (int) slider.value);

    private void OnEnable()
    {
        FirstTimeSetup();
    }

    private void FirstTimeSetup()
    {
        SetupDefaultPrice();
        SetButtonInteractable();
    }

    private void SetupDefaultPrice()
    {
        var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
        if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError) return;

        var singlePrice = loadGameResponse.data.tree.price;

        _bUsdSinglePrice = singlePrice[(int) LoadGameConfigResponse.TreeConfig.TreeCoinType.BUsd].quantity;
        _gMetaSinglePrice = singlePrice[(int) LoadGameConfigResponse.TreeConfig.TreeCoinType.GMeta].quantity;
    }
}

[Serializable]
public class TreePrice
{
    public long BUsd;
    public long GMeta;
}