using GEvent;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Material
{
    public class SelectedMaterialItemHandler : MonoBehaviour
    {
        [SerializeField] private MaterialCoreView selectedCoreView;


        private void OnEnable()
        {
            OnCoreViewSelected();
            EventManager.StartListening(EventName.UI.Select<MaterialInfo>(), OnCoreViewSelected);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.Select<MaterialInfo>(), OnCoreViewSelected);
        }

        private void OnCoreViewSelected()
        {
            if (EventManager.GetData(EventName.UI.Select<MaterialInfo>()) is null)
            {
                selectedCoreView.UpdateDefault();
                return;
            }

            var data = (MaterialInfo) EventManager.GetData(EventName.UI.Select<MaterialInfo>());
            if (data is null) selectedCoreView.UpdateDefault();
            else selectedCoreView.UpdateView(data);
        }
    }
}
