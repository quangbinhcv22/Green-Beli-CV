using GEvent;
using Manager.Inventory;
using Network.Messages.LoadGame;
using Network.Service;
using TigerForge;
using TMPro;
using UI.Widget.Reflector;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Energy.Container.Widgets.UpgradeButton
{
    public class EnergyUpgradeButton : MonoBehaviour
    {
        [SerializeField] private ButtonInteractReflector buttonInteractReflector;
        
        [SerializeField, Space] private MoneyType moneyType;
        [SerializeField, Space] private Button upgradeButton;
        [SerializeField] private TMP_Text upgradeCostText;
        [SerializeField] private Image iconCoin;

        private bool _isInteractable;
        

        private void Awake()
        {
            _isInteractable = default;
            upgradeButton.onClick.AddListener(OpenReconfirmPanelEvent);
            buttonInteractReflector.SetInteractCondition(() => _isInteractable);
            
            EventManager.StartListening(EventName.Server.UpgradeEnergyCapacity, UpdateView);
        }

        private void OnEnable()
        {
            if (NetworkService.Instance.services.login.IsLoggedIn == false)
                return;
            
            UpdateView();
        }
        
        private void OpenReconfirmPanelEvent()
        {
            EventManager.EmitEventData(EventName.Client.Energy.OpenUpgradeEnergyReconfirmPanel, moneyType);
        }

        private void UpdateView()
        {
            buttonInteractReflector.SetInteractCondition(() => _isInteractable);
            buttonInteractReflector.ReflectInteract();
        }
        
        public void UpdateUpgradeButtonView(LoadGameConfigResponse loadGameConfigResponse, int capacityLevel, bool isNotMaxLevelCapacity)
        {
            upgradeCostText.text = isNotMaxLevelCapacity
                ? $"{loadGameConfigResponse.energy.capacity[capacityLevel].upgrade_cost:N0}"
                : $"MAX";

            iconCoin.gameObject.SetActive(isNotMaxLevelCapacity);

            _isInteractable = isNotMaxLevelCapacity && NetworkService.playerInfo.inventory.GetMoney(moneyType) >=
                loadGameConfigResponse.energy.capacity[capacityLevel].upgrade_cost;

            UpdateView();
        }
    }
}