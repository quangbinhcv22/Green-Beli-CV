using System;
using GEvent;
using Network.Messages.GetHeroList;
using Network.Messages.UpdateEnergy;
using Network.Service;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;

namespace GRBESystem.Entity
{
    public class EnergyInfo
    {
        private ServerServiceGroup ServerServices => NetworkService.Instance.services;

        private int _level = 0;
        private int _current = 0;
        private int _capacity = 0;

        public int Level
        {
            get => _level;
            set
            {
                if (_level != value)
                {
                    _level = value;

                    Capacity = GetCapacity();

                    EventManager.EmitEventData(EventName.Client.Energy.UpgradeEnergyCapacity, Capacity);
                    EmitEventDataChanged();
                }
            }
        }

        public int Current
        {
            get => _current;
            set
            {
                _current = value;
                EmitEventDataChanged();
            }
        }

        public int Capacity
        {
            get => _capacity;
            private set
            {
                _capacity = value;
                EmitEventDataChanged();
            }
        }

        private int cacheNewEnergy;

        public EnergyInfo()
        {
            EventManager.StartListening(EventName.Server.UpdateEnergy, OnUpdateEnergyResponse);
            EventManager.StartListening(EventName.Server.LoadGameConfig, OnLoadGameConfig);
            EventManager.StartListening(EventName.Server.StartGame, OnStartGame);
        }

        private void EmitEventDataChanged()
        {
            EventManager.EmitEventData(EventName.Client.Energy.UpdateEnergy, this);
        }

        public void OnUpdateEnergyResponse()
        {
            var data = EventManager.GetData<UpdateEnergyResponse>(EventName.Server.UpdateEnergy);
            Current = data.energy;
            // cacheNewEnergy = data.energy;
        }

        public void OnLoadGameConfig()
        {
            Level = ServerServices.login.LoginResponse.energyCapacityLevel;
        }

        public void UpdateCurrentEnergy()
        {
            // this.Current = this.cacheNewEnergy;
            // this.cacheNewEnergy = -1;
            // EventManager.EmitEventData(EventName.Client.Energy.UPDATE_ENERGY, this);
        }

        private void Sub(int value)
        {
            if (CanPlayPVE())
            {
                Current -= value;
                // cacheNewEnergy -= value;
            }
            else
            {
                throw new Exception($"Energy input is invalid: {Current} => {value}");
            }
        }

        public bool IsMaxCapacity()
        {
            return Current >= _capacity;
        }

        private int GetCapacity()
        {
            var energy = ServerServices.loadGameConfig.ResponseData.energy;

            return energy.GetCapacity(Level);
        }

        private void OnStartGame()
        {
            var battleMode = EventManager.GetData(EventName.Client.Battle.BattleMode);
            if (battleMode is null || (BattleMode)battleMode is BattleMode.PvP) return;

            var spendEnergy = EnergyNeedForBattle();
            Sub(spendEnergy);
        }

        public int EnergyNeedForBattle()
        {
            var star = ServerServices.getHeroList.HeroResponses.GetMainHero().star;
            var spendEnergy = ServerServices.loadGameConfig.ResponseData.energy.GetEnergyPerPve(star);

            return spendEnergy;
        }

        public bool CanPlayPVE()
        {
            return Current >= EnergyNeedForBattle();
        }
    }
}