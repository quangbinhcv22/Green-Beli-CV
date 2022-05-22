using System;
using GEvent;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

public class MaintainInfoText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string textDefault;

    private bool _isFirstUpdate;
    
    
    private void Awake() => EventManager.StartListening(EventName.Server.Info,UpdateView);

    private void OnEnable()
    {
        if(_isFirstUpdate is false) UpdateView();
    }

    private void UpdateView()
    {
        if (ServerMaintainServerService.Response.IsError)
        {
            text.SetText(textDefault);
            return;
        }

        _isFirstUpdate = true;
        text.SetText(ServerMaintainServerService.Response.data.content);
    }
}
