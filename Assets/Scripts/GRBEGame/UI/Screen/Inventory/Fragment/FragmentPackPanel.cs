using GEvent;
using GRBEGame.Define;
using Network.Service;
using TigerForge;
using TMPro;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentPackPanel : MonoBehaviour
    {
        [SerializeField, Space] private Image icon;
        [SerializeField] private FragmentArtSet fragmentArtSet;
        [SerializeField] private TMP_Text quantityText;


        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Client.Inventory.FakePackFragment, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Client.Inventory.FakePackFragment, UpdateView);
        }

        private void UpdateView()
        {
            var data = EventManager.GetData(EventName.Client.Inventory.FakePackFragment);
            if (NetworkService.Instance.IsNotLogged() || data is null) return;

            icon.sprite = fragmentArtSet.GetFragmentIcon((FragmentType) (int) data);
            quantityText.SetText("10");
        }
    }
}
