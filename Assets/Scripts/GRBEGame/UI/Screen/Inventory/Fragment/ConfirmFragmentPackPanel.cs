using GEvent;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class ConfirmFragmentPackPanel : MonoBehaviour
    {
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button okButton;
        [SerializeField] private Slider quantitySlider;
        [SerializeField] private int fragmentMultiple;
        

        private void Awake()
        {
            okButton.onClick.AddListener(() => AssembleFragmentEvent(true));
            cancelButton.onClick.AddListener(() => AssembleFragmentEvent(false));
        }

        private void OnEnable()
        {
            EventManager.StopListening(EventName.UI.Select<ConfirmFragmentPackPanel>(),
                () => gameObject.SetActive(true));
        }

        private void OnDisable()
        {
            EventManager.StartListening(EventName.UI.Select<ConfirmFragmentPackPanel>(),
                () => gameObject.SetActive(true));
        }

        private void AssembleFragmentEvent(bool enable)
        {
            gameObject.SetActive(false);
            if (enable is false) return;

            var data = EventManager.GetData(EventName.UI.Select<FragmentItemInfo>());
            if (NetworkService.Instance.IsNotLogged() || data is null) return;

            var fragmentItem = (FragmentItemInfo) data;
            // Web3Token.Instance.PackIntoBox(fragmentItem.type, (int) (quantitySlider.value + 1) * fragmentMultiple);
            FakePackAPI(fragmentItem.type); 
        }

        private void FakePackAPI(int type)
        {
            EventManager.EmitEventData(EventName.Client.Inventory.FakePackFragment, type);
        }
    }
}
