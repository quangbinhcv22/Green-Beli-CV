using Manager.UseFeaturesPermission;
using UnityEngine;
using UnityEngine.Events;

public class PermissionUseFeatureEvent : MonoBehaviour
{
    [SerializeField] private FeatureId feature;
    [SerializeField] private UnityEvent onCanUse;
    [SerializeField] private UnityEvent onCanNotUse;

    private void OnEnable()
    {
        (PermissionUseFeature.CanUse(feature) switch
        {
            true => onCanUse,
            _ => onCanNotUse,
        })?.Invoke();
    }
}