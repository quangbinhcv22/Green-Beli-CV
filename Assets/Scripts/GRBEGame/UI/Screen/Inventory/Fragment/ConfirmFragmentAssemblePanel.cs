using System;
using GEvent;
using Network.Service;
using Network.Web3;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class ConfirmFragmentAssemblePanel : MonoBehaviour
    {
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button okButton;


        private void Awake()
        {
            okButton.onClick.AddListener(() => AssembleFragmentEvent(true));
            cancelButton.onClick.AddListener(() => AssembleFragmentEvent(false));
        }

        private void OnEnable()
        {
            EventManager.StopListening(EventName.UI.Select<ConfirmFragmentAssemblePanel>(),
                () => gameObject.SetActive(true));
        }
        
        private void OnDisable()
        {
            EventManager.StartListening(EventName.UI.Select<ConfirmFragmentAssemblePanel>(),
                () => gameObject.SetActive(true));
        }

        private void AssembleFragmentEvent(bool enable)
        {
            gameObject.SetActive(false);
            if(enable is false) return;
            
            var data = EventManager.GetData(EventName.UI.Select<FragmentItemInfo>());
            if (NetworkService.Instance.IsNotLogged() || data is null) return;

            var fragmentItem = (FragmentItemInfo) data;
            // Web3Token.Instance.AssembleIntoBox(fragmentItem.type);
            FakeAssembleAPI(fragmentItem.type);
        }
        
        private void FakeAssembleAPI(int type)
        {
            EventManager.EmitEventData(EventName.Client.Inventory.FakeAssembleFragment, type);
        }
    }
}
