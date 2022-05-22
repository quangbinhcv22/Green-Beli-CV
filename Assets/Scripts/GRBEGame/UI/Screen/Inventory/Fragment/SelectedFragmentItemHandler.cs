using GEvent;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Fragment
{
    public class SelectedFragmentItemHandler : MonoBehaviour
    {
        [SerializeField] private FragmentItemCoreView selectedCoreView;


        private void OnEnable()
        {
            OnFragmentCoreViewSelected();
            EventManager.StartListening(EventName.UI.Select<FragmentItemInfo>(), OnFragmentCoreViewSelected);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.Select<FragmentItemInfo>(), OnFragmentCoreViewSelected);
        }

        private void OnFragmentCoreViewSelected()
        {
            if (EventManager.GetData(EventName.UI.Select<FragmentItemInfo>()) is null)
            {
                selectedCoreView.UpdateDefault();
                return;
            }

            var data = (FragmentItemInfo) EventManager.GetData(EventName.UI.Select<FragmentItemInfo>());
            if (data is null) selectedCoreView.UpdateDefault();
            else selectedCoreView.UpdateView(data);
        }
    }
}
