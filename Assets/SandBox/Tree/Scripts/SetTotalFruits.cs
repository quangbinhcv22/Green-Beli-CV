using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class SetTotalFruits : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake() => button.onClick.AddListener(Setup);

    private void Setup()
    {
        if (GetListTreeServerService.Response.IsError) return;
        var nullableTreeId = EventManager.GetData(EventName.Select.Tree);

        if (nullableTreeId is Tree tree)
        {
            
        }
    }
}
