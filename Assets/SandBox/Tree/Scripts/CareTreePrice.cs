using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class CareTreePrice : MonoBehaviour
{
    [SerializeField] private CareTreeType careTreeType;
    [SerializeField] private Slider slider;

    private const int OneTree = 0;

    private void Awake()
    {
        slider.onValueChanged.AddListener(_ => UpdateView((int) slider.value));
        UpdateView(OneTree);
    }

    private void UpdateView(int value)
    {
        switch (careTreeType)
        {
            case CareTreeType.Water:
                EventManager.EmitEventData(EventName.Select.Watering, value);
                break;
            case CareTreeType.Fertilizing:
                EventManager.EmitEventData(EventName.Select.Fertilizing, value);
                break;
        }
    }
}