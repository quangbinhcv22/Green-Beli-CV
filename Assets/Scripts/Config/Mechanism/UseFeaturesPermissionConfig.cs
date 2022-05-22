using Manager.UseFeaturesPermission;
using QB.Collection;
using UnityEngine;

namespace Config.Mechanism
{
    [CreateAssetMenu(fileName = nameof(UseFeaturesPermissionConfig), menuName = "ScriptableObject/Config/Mechanism/UseFeaturesPermissionConfig")]
    public class UseFeaturesPermissionConfig : ScriptableObject
    {
        [SerializeField] private DefaultableDictionary<FeatureId, FeatureState> featurePermissions;

        public FeatureState GetFeatureState(FeatureId featureId)
        {
            return featurePermissions[featureId];
        }
    }
}