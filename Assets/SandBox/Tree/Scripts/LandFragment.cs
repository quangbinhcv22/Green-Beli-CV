using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using GRBEGame.Define;
using GRBEGame.UI.Screen.Inventory;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

public class LandFragment : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string textFormat = "{0}";
    
    private void Awake()
    {
        EventManager.StartListening(EventName.Server.LoadInventory,Setup);
        LoadInventoryServerService.SendRequest();
    }
    
    private void Setup()
    {
        if(LoadInventoryServerService.Response.IsError) return;

        var aa = LoadInventoryServerService.Response.data.fragments;
        if (aa is null || aa.Any() is false) return;
        
        var land = aa.Find(item => item.type == (int)FragmentType.Land);
        
        text.SetText(string.Format(textFormat,land.number));
    }
}
