using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using TigerForge;
using TMPro;
using UnityEngine;

public class TreeFruitQuantityText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string textFormat;
    [SerializeField] private int numberFruitPerWatering;
    [SerializeField] private int numberFruitPerFertilizing;
    
    private const int ZeroFruit = 0;
        
    private void Awake()
    {
        SetupDefaultQuantity();
    }

    private void SetupDefaultQuantity()
    {
        if (GetListTreeServerService.Response.IsError) return;

        var nullableTreeId = EventManager.GetData(EventName.Select.Tree);
        if (nullableTreeId is Tree tree)
        {
            GetListTreeServerService.ResetTotalFruits(tree.id);
        }
    }

    private void OnEnable()
    {
        SetupDefault();
        EventManager.StartListening(EventName.Select.Watering, SetUp);
        EventManager.StartListening(EventName.Select.Fertilizing, SetUp);
    }
    
    private void OnDisable()
    {
        EventManager.StopListening(EventName.Select.Watering, SetUp);
        EventManager.StopListening(EventName.Select.Fertilizing, SetUp);
    }

    private void SetupDefault()
    {
        if (GetListTreeServerService.Response.IsError) return;

        var nullableTreeId = EventManager.GetData(EventName.Select.Tree);
        if (nullableTreeId is Tree tree)
        {
            var fruits = GetListTreeServerService.GetTotalFruits(tree.id);
            Debug.Log($"<color=red>{tree.id}</color>");
            text.SetText(fruits <= ZeroFruit? string.Format(textFormat, ZeroFruit) : string.Format(textFormat, fruits));
        }
    }
    
    private void SetUp()
    {
        var wateringQuantity = EventManager.GetData(EventName.Select.Watering);
        var fertilizingQuantity = EventManager.GetData(EventName.Select.Fertilizing);

        if (wateringQuantity is int waterQuantity && fertilizingQuantity is int fertilizerQuantity)
        {
            var totalFruitByWatering = waterQuantity * numberFruitPerWatering;
            var totalFruitByFertilizing = fertilizerQuantity * numberFruitPerFertilizing;
            var totalFruit = totalFruitByWatering + totalFruitByFertilizing;
            
            text.SetText(string.Format(textFormat, totalFruit));
        }
    }
}

