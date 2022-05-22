using System;
using GEvent;
using Network.Controller;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Panels.Disconnect
{
    public class DisconnectHandler : MonoBehaviour
    {
        private NetworkReachability _lastNetworkReachability;

        private void Awake()
        {
            _lastNetworkReachability = Application.internetReachability;
        }

        private void Update()
        {
            if (_lastNetworkReachability == Application.internetReachability) return;
            _lastNetworkReachability = Application.internetReachability;

            if (Application.internetReachability is NetworkReachability.NotReachable)
            {
                EventManager.EmitEventData(EventName.Server.ConnectStatus, ConnectStatus.Disconnect);
                EventManager.EmitEvent(EventName.Server.Disconnect);
            }
        }
    }
}