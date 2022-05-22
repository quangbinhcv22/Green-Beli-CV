using System;
using GEvent;
using Localization.Nation;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Player
{
    public class SelfPlayerNationImage : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private NationConfig nationConfig;

        private void OnEnable()
        {
            EventManager.StartListening(EventName.Server.SetNation, UpdateView);
            UpdateView();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.SetNation, UpdateView);
        }

        private void UpdateView()
        {
            var loginResponse = NetworkService.Instance.services.login.MessageResponse;
            if (loginResponse.IsError) return;

            var selfNation = loginResponse.data.nation;
            image.sprite = nationConfig.GetNation(selfNation).art;
        }
    }
}