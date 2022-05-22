using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.MatchPvp
{
    [RequireComponent(typeof(TMP_Text))]
    public class FindCompetitorTimerText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textDefault = "00:00";
        [SerializeField] private int secondPerUpdate = 1;
        [SerializeField] private int timeSecondLimit = 30;

        private int _startFindCompetitorTimer;


        private void Awake() => text ??= GetComponent<TMP_Text>();

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.PlayPvp, UpdateView);
            EventManager.StartListening(EventName.Server.CancelPlayPvp, OnCancelFindPvp);
        }
        
        private void OnDisable()
        {
            StopCountTime();
            EventManager.StopListening(EventName.Server.PlayPvp, UpdateView);
            EventManager.StopListening(EventName.Server.CancelPlayPvp, OnCancelFindPvp);
        }
        
        private void UpdateView()
        {
            if (NetworkService.Instance.IsNotLogged() || PlayPvpServerService.Response.IsError)
            {
                StopCountTime();
                return;
            }
            StartCountTime();
        }

        private void OnCancelFindPvp()
        {
            StopCountTime();
            UpdateViewDefault();
        }

        private void StartCountTime()
        {
            _startFindCompetitorTimer = default;
            
            StopCountTime();
            InvokeRepeating(nameof(UpdateTime), default, secondPerUpdate);
        }
        
        private void StopCountTime()
        {
            _startFindCompetitorTimer = default;
            if(IsInvoking(nameof(UpdateTime)))
                CancelInvoke(nameof(UpdateTime));
        }

        private void UpdateTime()
        {
            _startFindCompetitorTimer += secondPerUpdate;
            var timeString = $"00:{_startFindCompetitorTimer:00}";
            text.SetText(timeString);
            
            if(_startFindCompetitorTimer >= timeSecondLimit)
                StopCountTime();
        }
        
        private void UpdateViewDefault()
        {
            text.SetText(textDefault);
        }
    }
}
