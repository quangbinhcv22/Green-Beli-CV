using System.Linq;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    [DefaultExecutionOrder(1000)]
    public class BoxItemDefaultSelect : MonoBehaviour
    {
        private BoxItemInfo _boxItemInfo;


        private void OnLoadInventory()
        {
            var response = LoadInventoryServerService.Response;
            if (NetworkService.Instance.IsNotLogged() || response.IsError) return;

            var boxResponses = response.data.boxes;
            _boxItemInfo = boxResponses.Any() ? new BoxItemInfo(boxResponses[default]) : null;

            EventManager.EmitEventData(EventName.UI.Select<BoxItemInfo>(), _boxItemInfo);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.LoadInventory, OnLoadInventory);
        }

        private void OnEnable()
        {
            OnLoadInventory();

            EventManager.StartListening(EventName.Server.LoadInventory, OnLoadInventory);
            EventManager.EmitEventData(EventName.UI.Select<BoxItemInfo>(), _boxItemInfo);
        }
    }
}
