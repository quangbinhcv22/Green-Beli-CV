using Config.Mechanism;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using QB.Collection;
using TigerForge;
using UnityEngine;

namespace Manager.UseFeaturesPermission
{
    public class UseFeaturesPermissionSetter : MonoBehaviour
    {
        [SerializeField] private DefaultableDictionary<LoginServerService.LoginMode, UseFeaturesPermissionConfig>
            permissionFeaturesConfigs;

        private void Awake()
        {
            EventManager.StartListening(EventName.Server.LoginByMetamask, OnLogin);
        }

        private void OnLogin()
        {
            EventManager.EmitEventData(EventName.PlayerEvent.UseFeaturesPermission,
                permissionFeaturesConfigs[NetworkService.Instance.services.login.loginMode]);
        }
    }
}