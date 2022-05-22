using System;
using System.Linq;
using GEvent;
using GNetwork;
using TigerForge;
using UnityEngine;

[DefaultExecutionOrder(500)]
public class SelectTreeAuto : MonoBehaviour
{
    private void Awake()
    {
        EventManager.StartListening(EventName.Server.TreeHasChanged,FirstTreeSelect);
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventName.Server.TreeHasChanged,FirstTreeSelect);
        FirstTreeSelect();
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Server.TreeHasChanged,FirstTreeSelect);
    }

    private void FirstTreeSelect()
    {
        if (GetListTreeServerService.Response.IsError || GetListTreeServerService.Response.data.Any() is false) return;
        
        var firstTree = GetListTreeServerService.StageOneResponse.First();
        EventManager.EmitEventData(EventName.Select.Tree, firstTree);
    }
}