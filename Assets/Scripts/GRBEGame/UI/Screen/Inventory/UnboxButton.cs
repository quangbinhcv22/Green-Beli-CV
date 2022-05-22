using GEvent;
using Network.Service;
using Network.Web3;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class UnboxButton : MonoBehaviour
    {
        [SerializeField] private Button button;


        private void Awake()
        {
            button.onClick.AddListener(UnboxIntoInventoryEvent);
        }

        private void UnboxIntoInventoryEvent()
        {
            var data = EventManager.GetData(EventName.UI.Select<BoxItemInfo>());
            if (NetworkService.Instance.IsNotLogged() || data is null) return;

            var boxItem = (BoxItemInfo) data;
            Web3Controller.Instance.UnboxIntoInventory(boxItem.id);
            FakeAPIUnbox(boxItem.type);
        }

        private void FakeAPIUnbox(int type)
        {
            EventManager.EmitEventData(EventName.Client.Inventory.FakeUnbox, type);
        }
    }
}
