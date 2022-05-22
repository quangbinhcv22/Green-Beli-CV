using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Energy.Container.Widgets.StagePanel
{
    public class ActiveByUpgradeEnergyLevelSetter : MonoBehaviour
    {
        [SerializeField] private TypeSetter typeSetter;
        [SerializeField] private bool isActive;


        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.UpgradeEnergyCapacity, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.UpgradeEnergyCapacity, UpdateView);
        }

        private void UpdateView() =>
            gameObject.SetActive(isActive ? IsActiveByCapacityEnergyLevel() : IsActiveByCapacityEnergyLevel() is false);

        private bool IsActiveByCapacityEnergyLevel()
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError)
                return false;

            var panelData =
                EnergyContainerPopupData.Create(NetworkService.playerInfo.energyInfo, loadGameResponse.data);

            return typeSetter switch
            {
                TypeSetter.NotMaxLevel => panelData.isNotCapacityLevelMax,
                TypeSetter.MaxLevel => panelData.isNotCapacityLevelMax is false,
                _ => false,
            };
        }
    }
    
    [System.Serializable]
    public enum TypeSetter
    {
        NotMaxLevel = 0,
        MaxLevel = 2,
    }
}
