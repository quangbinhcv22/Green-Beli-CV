using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class ItemInventoryDefaultSelect : MonoBehaviour
    {
        private ItemInventory _itemInventory;


        private void OnLoadInventory()
        {
            var response = LoadInventoryServerService.Response;
            if (NetworkService.Instance.IsNotLogged() || response.IsError) return;

            var expCards = response.data.expCards;
            var fusionStones = response.data.fusionStones;
            var trainingHouses = response.data.trainingHouses;

            var items = new List<ItemInventory>();
            items.AddRange(expCards.Select(ItemInventoryDirector.GetItemInventory));
            items.AddRange(fusionStones.Select(ItemInventoryDirector.GetItemInventory));
            items.AddRange(trainingHouses.Select(ItemInventoryDirector.GetItemInventory));
            
            _itemInventory = items.Any() ? items[default] : null;
            EventManager.EmitEventData(EventName.UI.Select<FragmentItemInfo>(), _itemInventory);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.LoadInventory, OnLoadInventory);
        }

        private void OnEnable()
        {
            OnLoadInventory();

            EventManager.StartListening(EventName.Server.LoadInventory, OnLoadInventory);
            EventManager.EmitEventData(EventName.UI.Select<FragmentItemInfo>(), _itemInventory);
        }
    }
}
