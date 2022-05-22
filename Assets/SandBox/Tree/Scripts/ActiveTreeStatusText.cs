using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using TigerForge;
using TMPro;
using UnityEngine;

public class ActiveTreeStatusText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string textFormat = "{0}";

    private const string Active = "<color=green>Active</color>";
    private const string Inactive = "<color=red>Inactive</color>";
    private void OnEnable()
    {
        EventManager.StartListening(EventName.Select.ActiveTreeStatus,UpdateView);
    }
    
    private void OnDisable()
    {
        EventManager.StartListening(EventName.Select.ActiveTreeStatus,UpdateView);
    }

    private void UpdateView()
    {
        var isActiveData = EventManager.GetData(EventName.Select.ActiveTreeStatus);

        if (isActiveData is bool isActive)
        {
            var tmpText = isActive switch
            {
                true => Active,
                false => Inactive
            };
            text.SetText(string.Format(textFormat,tmpText)); 
        }
    }
}
