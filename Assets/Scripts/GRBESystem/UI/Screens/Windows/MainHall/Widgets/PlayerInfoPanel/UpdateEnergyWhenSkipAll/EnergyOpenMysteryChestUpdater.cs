using GEvent;
using GRBESystem.Entity;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UI.Widget;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.MainHall.Widgets.PlayerInfoPanel.UpdateEnergyWhenSkipAll
{
    public class EnergyOpenMysteryChestUpdater : MonoBehaviour
    {
        [SerializeField] private ProcessBar energyBar;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.OpenMysteryChest, UpdateEnergyInfo);
        }

        private void UpdateEnergyInfo()
        {
            if (OpenMysteryChestServerService.Response.IsError) return;
            UpdateView();
        }

        private void UpdateView()
        {
            var energyInfo = EventManager.GetData<EnergyInfo>(EventName.Client.Energy.UpdateEnergy);
            energyBar.UpdateView(NetworkService.playerInfo.energyInfo.Current, energyInfo.Capacity);
        }
    }
}