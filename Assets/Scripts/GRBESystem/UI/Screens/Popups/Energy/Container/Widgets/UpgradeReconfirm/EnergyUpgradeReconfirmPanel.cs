using GEvent;
using Manager.Inventory;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Energy.Container.Widgets.UpgradeReconfirm
{
    public class EnergyUpgradeReconfirmPanel : MonoBehaviour
    {
        [SerializeField] private Button okButton;
        private bool _isCanUpgrade;
        

        private void Awake()
        {
            okButton.onClick.AddListener(() => EmitReconfirmEvent(true));
        }
        
        private void OnEnable()
        {
            EventManager.StopListening(EventName.Client.Energy.OpenUpgradeEnergyReconfirmPanel, OpenUpgrade);
            OpenUpgrade();
        }

        private void OnDisable()
        {
            EventManager.StartListening(EventName.Client.Energy.OpenUpgradeEnergyReconfirmPanel, OpenUpgrade);
        }

        private void OpenUpgrade()
        {
            _isCanUpgrade = true;
        }

        private void EmitReconfirmEvent(bool confirm)
        {
            var moneyData = EventManager.GetData(EventName.Client.Energy.OpenUpgradeEnergyReconfirmPanel);

            if (_isCanUpgrade is false || confirm is false || moneyData is null)
            {
                _isCanUpgrade = false;
                return;
            }
            
            _isCanUpgrade = false;
            UpgradeEnergyServerService.SendRequest((MoneyType) moneyData);
        }
    }
}
