using System;
using GEvent;
using GRBESystem.Entity;
using Network.Service;
using TigerForge;
using TMPro;
using UI.Widget;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.MainHall.Widgets.PlayerInfoPanel
{
    public class EnergyController : MonoBehaviour
    {
        [SerializeField] private ProcessBar energyBar;
        [SerializeField] private TMP_Text textCountdown;


        private int _regenEnergyInterval;

        private int _nextRegenSeconds;

        private void Awake()
        {
            EventManager.StartListening(EventName.Server.LoadGameConfig, SetRegenInterval);
            EventManager.StartListening(EventName.Client.Energy.UpdateEnergy, UpdateEnergyBar);
        }

        private void Start()
        {
            UpdateText(string.Empty);
        }
        
        private void OnEnable()
        {
            SetRegenInterval();
            UpdateEnergyBar();
            
            if (TimeManager.Instance) TimeManager.Instance.AddEvent(UpdateEnergy);
        }

        private void OnDisable()
        {
            if (TimeManager.Instance) TimeManager.Instance.RemoveEvent(UpdateEnergy);
        }

        private void SetRegenInterval()
        {
            var messageResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (string.IsNullOrEmpty(messageResponse.error) == false) return;

            this._regenEnergyInterval = messageResponse.data.energyRecoverIntervalSec;
        }

        private void UpdateEnergy(int seconds)
        {
            var energyInfo = NetworkService.playerInfo.energyInfo;
            if (_regenEnergyInterval <= 0 || energyInfo == null) return;

            _nextRegenSeconds = _regenEnergyInterval - seconds % _regenEnergyInterval;

            if (_nextRegenSeconds == _regenEnergyInterval && energyInfo.IsMaxCapacity() == false)
            {
                energyInfo.UpdateCurrentEnergy();
            }

            var timeFormat = TimeManager.FormattedSeconds(_nextRegenSeconds);

            UpdateText(timeFormat);
        }


        private void UpdateEnergyBar()
        {
            var energyInfo = EventManager.GetData<EnergyInfo>(EventName.Client.Energy.UpdateEnergy);
            if (energyInfo.Capacity == 0 ) return;
            
            energyBar.UpdateView(energyInfo.Current, energyInfo.Capacity);
        }

        private void UpdateText(string content)
        {
            textCountdown.text = content;
        }
    }
}