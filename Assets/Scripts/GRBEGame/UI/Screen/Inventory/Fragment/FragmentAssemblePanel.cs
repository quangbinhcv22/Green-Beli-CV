using GEvent;
using GRBEGame.Define;
using Network.Service;
using TigerForge;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentAssemblePanel : MonoBehaviour
    {
        [SerializeField, Space] private Image icon;
        [SerializeField] private FragmentArtSet fragmentArtSet;


        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Client.Inventory.FakeAssembleFragment, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Client.Inventory.FakeAssembleFragment, UpdateView);
        }

        private void UpdateView()
        {
            var data = EventManager.GetData(EventName.Client.Inventory.FakeAssembleFragment);
            if (NetworkService.Instance.IsNotLogged() || data is null) return;

            icon.sprite = fragmentArtSet.GetFragmentIcon((FragmentType) (int) data);
        }
    }
}
