using System;
using GEvent;
using Localization.Nation;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.Player
{
    public class SelfPlayerNationText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
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
            text.SetText(nationConfig.GetNation(selfNation).fullName);
        }
    }
}