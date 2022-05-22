using Config.Mechanism;
using GEvent;
using TigerForge;
using UIFlow.InGame;
using UnityEngine;

namespace Manager.UseFeaturesPermission.AccessFeature
{
    public class AccessFeatureCollider : MonoBehaviour
    {
        [SerializeField] private FeatureId featureId;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private RequestUICollider navigationCollider;

        [SerializeField] private Color activeColor = Color.white;
        [SerializeField] private Color blockColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.8745098f);

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

            var featureState = ((UseFeaturesPermissionConfig) useFeaturesPermissionConfig).GetFeatureState(featureId);

            switch (featureState)
            {
                case FeatureState.Active:
                    spriteRenderer.color = activeColor;
                    navigationCollider.canAccess = true;
                    break;
                default:
                    spriteRenderer.color = blockColor;
                    navigationCollider.canAccess = false;
                    break;
            }
        }
    }
}