using Config.Mechanism;
using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace Manager.UseFeaturesPermission.AccessFeature
{
    public class AccessFeatureButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private FeatureId featureId;

        // private void Awake()
        // {
        //     EventManager.StartListening(EventName.PlayerEvent.UseFeaturesPermission, SetInteractableByLoginMode);
        // }
        //
        // private void OnEnable()
        // {
        //     SetInteractableByLoginMode();
        // }
        //
        // public void SetInteractableByLoginMode()
        // {
        //     var useFeaturesPermissionConfig = EventManager.GetData(EventName.PlayerEvent.UseFeaturesPermission);
        //     if (useFeaturesPermissionConfig is null) return;
        //
        //     var featureState = ((UseFeaturesPermissionConfig)useFeaturesPermissionConfig).GetFeatureState(featureId);
        //
        //     switch (featureState)
        //     {
        //         case FeatureState.ComingSoon:
        //             button.interactable = false;
        //             break;
        //         case FeatureState.Active:
        //             button.interactable = true;
        //             break;
        //         case FeatureState.Block:
        //             button.interactable = false;
        //             break;
        //     }
        // }
        //
        // public void SetButtonInteractable(bool isInteractable)
        // {
        //     button.interactable = isInteractable;
        // }
    }
}