using GEvent;
using GRBESystem.UI.Screens.Popups.Energy.Container.Widgets.StagePanel;
using GRBESystem.UI.Screens.Popups.Energy.Container.Widgets.UpgradeButton;
using Network.Messages.LoadGame;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Energy.Container.Widgets.InfoContainer
{
    public class EnergyInfoContainer : MonoBehaviour
    {
        private const string UpgradeEnergyCapacityEvent = EventName.Server.UpgradeEnergyCapacity;

        [SerializeField] private EnergyContainerStagePanel levelPanel;
        [SerializeField] private EnergyContainerStagePanel capacityPanel;

        [SerializeField, Space] private EnergyUpgradeButton gBeliEnergyUpgradeButton;
        [SerializeField] private EnergyUpgradeButton gFruitEnergyUpgradeButton;
        

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(UpgradeEnergyCapacityEvent, UpdateView);
            EventManager.StartListening(EventName.Inventory.Change, UpdateCurrentView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(UpgradeEnergyCapacityEvent, UpdateView);
            EventManager.StopListening(EventName.Inventory.Change, UpdateCurrentView);
        }

        private bool IsNotNetworkConnect()
        {
            return NetworkService.Instance.IsNotLogged() ||
                   EventManager.GetData(EventName.Server.LoadGameConfig) == null;
        }

        private void UpdateView()
        {
            if (IsNotNetworkConnect()) return;
            
            var loadGameResponse = EventManager.GetData<LoadGameConfigResponse>(EventName.Server.LoadGameConfig);
            var panelData = EnergyContainerPopupData.Create(NetworkService.playerInfo.energyInfo, loadGameResponse);

            if (panelData.isNotCapacityLevelMax)
            {
                levelPanel.UpdateViewStage(panelData.currentLevel.ToString(),
                    panelData.nextLevel.ToString());
                capacityPanel.UpdateViewStage(panelData.currentCapacity.ToString(),
                    panelData.nextCapacity.ToString());
            }
            else
            {
                levelPanel.UpdateViewMaxStage(NetworkService.playerInfo.energyInfo.Level.ToString());
                capacityPanel.UpdateViewMaxStage(NetworkService.playerInfo.energyInfo.Capacity.ToString());
            }
            
            gBeliEnergyUpgradeButton.UpdateUpgradeButtonView(loadGameResponse, panelData.currentLevel, panelData.isNotCapacityLevelMax);
            gFruitEnergyUpgradeButton.UpdateUpgradeButtonView(loadGameResponse, panelData.currentLevel, panelData.isNotCapacityLevelMax);
        }

        private void UpdateCurrentView()
        {
            if (IsNotNetworkConnect()) return;
            
            var loadGameResponse = EventManager.GetData<LoadGameConfigResponse>(EventName.Server.LoadGameConfig);
            var panelData = EnergyContainerPopupData.Create(NetworkService.playerInfo.energyInfo, loadGameResponse);
            
            gBeliEnergyUpgradeButton.UpdateUpgradeButtonView(loadGameResponse, panelData.currentLevel, panelData.isNotCapacityLevelMax);
            gFruitEnergyUpgradeButton.UpdateUpgradeButtonView(loadGameResponse, panelData.currentLevel, panelData.isNotCapacityLevelMax);
        }
    }
}
