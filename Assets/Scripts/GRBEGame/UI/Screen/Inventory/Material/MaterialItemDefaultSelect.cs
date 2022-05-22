using System.Linq;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Material
{
    [DefaultExecutionOrder(1000)]
    public class MaterialItemDefaultSelect : MonoBehaviour
    {
        private MaterialInfo _materialItemInfo;


        private void OnLoadInventory()
        {
            var response = LoadInventoryServerService.Response;
            if (NetworkService.Instance.IsNotLogged() || response.IsError) return;

            var materialResponses = response.data.materials;
            _materialItemInfo = materialResponses.Any() ? new MaterialInfo(materialResponses[default]) : null;

            EventManager.EmitEventData(EventName.UI.Select<MaterialInfo>(), _materialItemInfo);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.LoadInventory, OnLoadInventory);
        }

        private void OnEnable()
        {
            OnLoadInventory();

            EventManager.StartListening(EventName.Server.LoadInventory, OnLoadInventory);
            EventManager.EmitEventData(EventName.UI.Select<MaterialInfo>(), _materialItemInfo);
        }
    }
}
