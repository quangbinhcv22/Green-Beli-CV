using UnityEngine;
using UnityEngine.Events;

namespace GRBEGame.UI.Screen.Inventory.Material
{
    public class MaterialCoreView : MonoBehaviour, ICoreView<MaterialInfo>
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<MaterialInfo> _onUpdateView;


        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(MaterialInfo data)
        {
            _onUpdateView?.Invoke(data);
        }

        public void AddCallBackUpdateView(IMemberView<MaterialInfo> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }
}
