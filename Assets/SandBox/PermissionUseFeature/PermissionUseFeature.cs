using Config.Mechanism;
using GEvent;
using Manager.UseFeaturesPermission;
using TigerForge;

public static class PermissionUseFeature
{
    public static bool CanUse(FeatureId feature)
    {
        var useFeaturesPermissionConfig = EventManager.GetData(EventName.PlayerEvent.UseFeaturesPermission);
        if (useFeaturesPermissionConfig is null) return false;

        var featureState = ((UseFeaturesPermissionConfig) useFeaturesPermissionConfig).GetFeatureState(feature);

        return featureState switch
        {
            FeatureState.Active => true,
            _ => false
        };
    }
}