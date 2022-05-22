using System;
using GEvent;
using GRBEGame.UI.Widget.CommonButton;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.OutOfVersion
{
    [RequireComponent(typeof(OpenLinkButton))]
    public class PlatformSpecificLinkButton : MonoBehaviour
    {
        [SerializeField] private GetLatestClientReleaseServerService.Platform platform;
        private OpenLinkButton _openLinkButton;
        
        private void Awake()
        {
            _openLinkButton = GetComponent<OpenLinkButton>();
            EventManager.StartListening(EventName.Server.GetLatestClientRelease, SetUrl);
        }

        private void OnEnable()
        {
            SetUrl();
        }

        private void SetUrl()
        {
            _openLinkButton.SetUrl(GetUrl());
        }

        private string GetUrl()
        {
            return GetLatestClientReleaseServerService.GetUrl(platform);
        }

    }
}