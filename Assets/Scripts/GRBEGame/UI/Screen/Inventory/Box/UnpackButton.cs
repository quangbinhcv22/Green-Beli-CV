using System.Collections;
using System.Collections.Generic;
using GEvent;
using Network.Service;
using Network.Web3;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class UnpackButton : MonoBehaviour
    {
        [SerializeField] private Button button;


        private void Awake()
        {
            button.onClick.AddListener(UnpackIntoInventoryEvent);
        }

        private void UnpackIntoInventoryEvent()
        {
            var data = EventManager.GetData(EventName.UI.Select<BoxItemInfo>());
            if (NetworkService.Instance.IsNotLogged() || data is null) return;

            var boxItem = (BoxItemInfo) data;
            // Web3Token.Instance.UnpackIntoInventory(boxItem.id);
            FakeAPIUnpack(boxItem.type);
        }

        private void FakeAPIUnpack(int type)
        {
            EventManager.EmitEventData(EventName.Client.Inventory.FakeUnpack, type);
        }
    }
}
