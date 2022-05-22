using System.Linq;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Fragment
{
    public class FragmentItemDefaultSelect : MonoBehaviour
    {
        private FragmentItemInfo _fragmentItemInfo;
        

        private void OnLoadInventory()
        {
            var response = LoadInventoryServerService.Response;
            if (NetworkService.Instance.IsNotLogged() is false && response.IsError is false)
            {
                var fragmentResponses = response.data.fragments;
                _fragmentItemInfo = fragmentResponses.Any() ? new FragmentItemInfo(fragmentResponses[default]) : null;
            }
            EmitSelectDefaultEvent();
        }

        private void EmitSelectDefaultEvent()
        {
            EventManager.EmitEventData(EventName.UI.Select<FragmentItemInfo>(), _fragmentItemInfo);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.LoadInventory, OnLoadInventory);
        }

        private void OnEnable()
        {
            _fragmentItemInfo = null;

            OnLoadInventory();
            EventManager.StartListening(EventName.Server.LoadInventory, OnLoadInventory);
        }
    }
}