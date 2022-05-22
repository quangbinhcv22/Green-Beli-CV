using System;
using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GRBEGame.UI.Player
{
    public class SelfPlayerNationNextChangeCountdowner : MonoBehaviour
    {
        [SerializeField] [Space] private TMP_Text text;
        [SerializeField] private string format = "{0}";

        [SerializeField] [Space] private UnityEvent onLockCountdown;
        [SerializeField] private UnityEvent onUnLockCountdown;

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

            var lockDayChangeNation = loginResponse.data.LockDayChangeNation;

            if (lockDayChangeNation > (int)default)
            {
                text.SetText(string.Format(format, lockDayChangeNation));
                onLockCountdown?.Invoke();
            }
            else
            {
                onUnLockCountdown?.Invoke();
            }
        }
    }
}