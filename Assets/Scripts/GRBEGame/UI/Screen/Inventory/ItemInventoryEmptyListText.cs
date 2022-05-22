using System.Linq;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory
{
    public class ItemInventoryEmptyListText : MonoBehaviour
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string emptyListString;


        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.LoadInventory, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.LoadInventory, UpdateView);
        }

        private void UpdateView()
        {
            var response = LoadInventoryServerService.Response;
            if(response.IsError) return;

            var isHasItem = itemType switch
            {
                ItemType.Fragment => response.data.fragments.Any(),
                ItemType.Material => response.data.materials.Any(),
                _ => default
            };
        
            text.SetText(isHasItem ? string.Empty : emptyListString);
            SetActive(isHasItem is false);
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }

    public enum ItemType
    {
        None = 0,
        Fragment = 1,
        Box = 2,
        Item = 3,
        Material = 4,
    }
}