using UnityEngine;
using UnityEngine.Events;


namespace GRBEGame.UI.Screen.Inventory
{
    public class BoxItemCoreView : MonoBehaviour, ICoreView<BoxItemInfo>
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<BoxItemInfo> _onUpdateView;


        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(BoxItemInfo data)
        {
            _onUpdateView?.Invoke(data);
        }

        public void AddCallBackUpdateView(IMemberView<BoxItemInfo> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }
}