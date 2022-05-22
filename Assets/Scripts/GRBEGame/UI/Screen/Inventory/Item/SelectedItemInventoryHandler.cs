using GEvent;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class SelectedItemInventoryHandler : MonoBehaviour
    {
        [SerializeField] private ItemInventoryCoreView selectedCoreView;


        private void OnEnable()
        {
            OnFragmentCoreViewSelected();
            EventManager.StartListening(EventName.UI.Select<ItemInventory>(), OnFragmentCoreViewSelected);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.Select<ItemInventory>(), OnFragmentCoreViewSelected);
        }

        private void OnFragmentCoreViewSelected()
        {
            if (EventManager.GetData(EventName.UI.Select<ItemInventory>()) is null)
            {
                selectedCoreView.UpdateDefault();
                return;
            }

            var data = (ItemInventory) EventManager.GetData(EventName.UI.Select<ItemInventory>());
            if (data is null) selectedCoreView.UpdateDefault();
            else selectedCoreView.UpdateView(data);
        }
    }
}
