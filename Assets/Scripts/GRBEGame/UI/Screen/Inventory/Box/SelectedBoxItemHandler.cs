using GEvent;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class SelectedBoxItemHandler : MonoBehaviour
    {
        [SerializeField] private BoxItemCoreView selectedCoreView;


        private void OnEnable()
        {
            OnBoxCoreViewSelected();
            EventManager.StartListening(EventName.UI.Select<BoxItemInfo>(), OnBoxCoreViewSelected);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.Select<BoxItemInfo>(), OnBoxCoreViewSelected);
        }

        private void OnBoxCoreViewSelected()
        {
            if (EventManager.GetData(EventName.UI.Select<BoxItemInfo>()) is null)
            {
                selectedCoreView.UpdateDefault();
                return;
            }

            var data = (BoxItemInfo) EventManager.GetData(EventName.UI.Select<BoxItemInfo>());
            if (data is null) selectedCoreView.UpdateDefault();
            else selectedCoreView.UpdateView(data);
        }
    }
}
