using System;
using GEvent;
using Manager.Inventory;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

public class CareTreePriceText : MonoBehaviour
{
    [SerializeField] private MoneyType moneyType;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string textFormat = "{0}";
    
     private int waterSinglePrice;
     private int fertilizerSinglePrice;

    private const int ZeroTree = 0;

    private void OnEnable()
    {        
        SetUp(moneyType);

        UpdateDefault();
        EventManager.StartListening(EventName.Select.Watering, UpdateView);
        EventManager.StartListening(EventName.Select.Fertilizing, UpdateView);
    }
    
    private void OnDisable()
    {
        EventManager.StopListening(EventName.Select.Watering, UpdateView);
        EventManager.StopListening(EventName.Select.Fertilizing, UpdateView);
    }

    private void UpdateDefault()
    {
        var waterPrice = ZeroTree * waterSinglePrice;
        var fertilizerPrice = ZeroTree * fertilizerSinglePrice;
        var totalPrice = waterPrice + fertilizerPrice;

        text.SetText(string.Format(textFormat, totalPrice));
    }

    private void SetUp(MoneyType moneyType)
    {
        var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
        if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError) return;

        var tree = loadGameResponse.data.tree.general;

        switch (moneyType)
        {
            case MoneyType.BeLi:
                fertilizerSinglePrice = tree.beLiCostPerFertilizing is null ? 0 : int.Parse(tree.beLiCostPerFertilizing);
                waterSinglePrice = tree.beLiCostPerWatering is null ? 0 : int.Parse(tree.beLiCostPerWatering) ;
                break;
            case MoneyType.GFruit:
                fertilizerSinglePrice = tree.gFruitCostPerFertilizing is null ? 0 : int.Parse(tree.gFruitCostPerFertilizing);
                waterSinglePrice = tree.gFruitCostPerWatering is null ? 0 : int.Parse(tree.gFruitCostPerWatering) ;
                break;
        }
    }
    
    private void UpdateView()
    {
        var wateringQuantity = EventManager.GetData(EventName.Select.Watering);
        var fertilizingQuantity = EventManager.GetData(EventName.Select.Fertilizing);

        if (wateringQuantity is int waterQuantity && fertilizingQuantity is int fertilizerQuantity)
        {
            var waterPrice = waterQuantity * waterSinglePrice;
            var fertilizerPrice = fertilizerQuantity * fertilizerSinglePrice;
            var totalPrice = waterPrice + fertilizerPrice;

            text.SetText(string.Format(textFormat, totalPrice));
        }
    }
}