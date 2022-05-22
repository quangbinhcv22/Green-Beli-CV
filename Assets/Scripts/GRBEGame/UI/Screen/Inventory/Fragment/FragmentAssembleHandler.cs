using GEvent;
using Network.Service;
using Network.Service.Implement;
using QuangBinh.UIFramework.Screen;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentAssembleHandler : MonoBehaviour
    {
        [SerializeField] private ScreenID goToScreen;
        private const string FragmentAssembleEventName = EventName.Client.Inventory.FakeAssembleFragment;


        private void Awake()
        {
            EventManager.StartListening(FragmentAssembleEventName, OnFragmentAssembled);
        }

        private void OnFragmentAssembled()
        {
            if (NetworkService.Instance.IsNotLogged()) return;

            LoadInventoryServerService.SendRequest();
            EventManager.EmitEventData(EventName.ScreenEvent.RequestOpenScreen,
                data: new OpenScreenRequest() { screenId = goToScreen });
        }
    }
}
