using Config.Mechanism;
using GEvent;
using Manager.UseFeaturesPermission;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Energy.Container.Widgets.UpgradeButton
{
    public class EnergyUpgradeFeatureButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private FeatureId featureId = FeatureId.UpdateEnergy;

        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.UseFeaturesPermission, OnFeaturesPermission);
        }

        private void OnEnable()
        {
            OnFeaturesPermission();
        }

        private void OnFeaturesPermission()
        {
            var useFeaturesPermissionConfig = EventManager.GetData(EventName.PlayerEvent.UseFeaturesPermission);
            if (useFeaturesPermissionConfig is null) return;

            var featureState = ((UseFeaturesPermissionConfig)useFeaturesPermissionConfig).GetFeatureState(featureId);
            if (featureState is FeatureState.ComingSoon || featureState is FeatureState.Block)
                button.interactable = false;
        }
    }
}