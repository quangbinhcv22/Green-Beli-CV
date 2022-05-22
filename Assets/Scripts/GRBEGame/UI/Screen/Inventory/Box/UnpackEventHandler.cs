using GEvent;
using Network.Service;
using Network.Service.Implement;
using QuangBinh.UIFramework.Screen;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class UnpackEventHandler : MonoBehaviour
    {
        [SerializeField] private ScreenID goToScreen;
        private const string UnpackEventName = EventName.Client.Inventory.FakeUnpack;


        private void Awake()
        {
            EventManager.StartListening(UnpackEventName, OnUnpack);
        }

        private void OnUnpack()
        {
            if (NetworkService.Instance.IsNotLogged()) return;

            LoadInventoryServerService.SendRequest();
            EventManager.EmitEventData(EventName.ScreenEvent.RequestOpenScreen,
                data: new OpenScreenRequest() {screenId = goToScreen});
        }
    }
}
