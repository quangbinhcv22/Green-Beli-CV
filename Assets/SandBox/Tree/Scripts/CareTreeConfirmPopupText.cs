using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

public class CareTreeConfirmPopupText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string textFormat = "{0} {1}";

    private void OnEnable() =>
        EventManager.StartListening(EventName.Select.CareTreeTotalPrice,UpdateView);
    
    private void OnDisable() =>
        EventManager.StopListening(EventName.Select.CareTreeTotalPrice,UpdateView);

    private void UpdateView()
    {
        var careTree = EventManager.GetData(EventName.Select.CareTreeTotalPrice);

        if (careTree is CareTree price)
        {
            text.SetText(string.Format(textFormat,price.total,price.currency));
        }
    }
}
