using GEvent;
using Network.Service;
using Network.Service.Implement;
using QuangBinh.UIFramework.Screen;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class UnboxEventHandler : MonoBehaviour
    {
        [SerializeField] private ScreenID goToScreen;
        private const string UnboxEventName = EventName.Client.Inventory.FakeUnbox;


        private void Awake()
        {
            EventManager.StartListening(UnboxEventName, OnUnbox);
        }

        private void OnUnbox()
        {
            if (NetworkService.Instance.IsNotLogged()) return;

            LoadInventoryServerService.SendRequest();
            EventManager.EmitEventData(EventName.ScreenEvent.RequestOpenScreen,
                data: new OpenScreenRequest() {screenId = goToScreen});
        }
    }
}
