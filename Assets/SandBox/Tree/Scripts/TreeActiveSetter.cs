using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using GNetwork;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class TreeActiveSetter : MonoBehaviour
{
    [SerializeField] private GameObject firstTimePanel;
    [SerializeField] private GameObject treeFarmPanel;
    

    //private void Awake() => OpenTreePanel();

    private void OnEnable()
    {
        EventManager.StartListening(EventName.Server.GetListTree, OpenTreePanel);
        OpenTreePanel();
    }

    private void OnDisable() => EventManager.StopListening(EventName.Server.GetListTree, OpenTreePanel);

    private void OpenTreePanel()
    {
        if (GetListTreeServerService.Response.IsError) return;

        if (GetListTreeServerService.StageOneResponse.Any())
        {
            firstTimePanel.gameObject.SetActive(false);
            treeFarmPanel.gameObject.SetActive(true);
        }
        else
        {
            firstTimePanel.gameObject.SetActive(true);
            treeFarmPanel.gameObject.SetActive(false);
        }
    }
}