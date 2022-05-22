using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class CareTreeButtonSetter : MonoBehaviour
{
    [SerializeField] private Button button;
    
    private void OnEnable()
    {
        EventManager.StartListening(EventName.Select.ActiveTree,UpdateView);
    }
    
    private void OnDisable()
    {
        EventManager.StopListening(EventName.Select.ActiveTree,UpdateView);
    }

    private void UpdateView()
    {
        var isActiveData = EventManager.GetData(EventName.Select.ActiveTree);

        if (isActiveData is bool isActive)
        {
            button.interactable = isActive;
        }
    }
    
}
