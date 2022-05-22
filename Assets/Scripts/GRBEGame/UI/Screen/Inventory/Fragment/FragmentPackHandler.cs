using GEvent;
using Network.Service;
using Network.Service.Implement;
using QuangBinh.UIFramework.Screen;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentPackHandler : MonoBehaviour
    {
        [SerializeField] private ScreenID goToScreen;
        private const string FragmentPackEventName = EventName.Client.Inventory.FakePackFragment;


        private void Awake()
        {
            EventManager.StartListening(FragmentPackEventName, OnFragmentAssembled);
        }

        private void OnFragmentAssembled()
        {
            if (NetworkService.Instance.IsNotLogged()) return;
            
            LoadInventoryServerService.SendRequest();
            EventManager.EmitEventData(EventName.ScreenEvent.RequestOpenScreen,
                data: new OpenScreenRequest() {screenId = goToScreen});
        }
    }
}
