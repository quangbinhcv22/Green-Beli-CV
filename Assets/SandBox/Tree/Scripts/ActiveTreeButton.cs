using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class ActiveTreeButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private bool isActive = true;

    private void Awake() => button.onClick.AddListener(ButtonClicked);

    private void OnEnable() =>
        EventManager.StartListening(EventName.Select.ActiveTree, UpdateStatus);


    private void OnDisable() =>
        EventManager.StopListening(EventName.Select.ActiveTree, UpdateStatus);


    private void UpdateStatus()
    {
        var treeStatus = EventManager.GetData(EventName.Select.ActiveTree);
        if (treeStatus is bool status) isActive = status;
    }

    private void ButtonClicked() =>
        EventManager.EmitEventData(EventName.Select.ActiveTreeStatus, isActive);
}