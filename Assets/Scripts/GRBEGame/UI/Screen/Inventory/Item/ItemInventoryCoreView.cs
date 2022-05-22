using UnityEngine;
using UnityEngine.Events;

namespace GRBEGame.UI.Screen.Inventory
{
    public class ItemInventoryCoreView : MonoBehaviour, ICoreView<ItemInventory>
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<ItemInventory> _onUpdateView;


        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(ItemInventory data)
        {
            _onUpdateView?.Invoke(data);
        }

        public void AddCallBackUpdateView(IMemberView<ItemInventory> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }
}
