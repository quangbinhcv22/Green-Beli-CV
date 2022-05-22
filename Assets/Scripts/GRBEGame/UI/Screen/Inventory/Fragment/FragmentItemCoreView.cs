using UnityEngine;
using UnityEngine.Events;

namespace GRBEGame.UI.Screen.Inventory.Fragment
{
    public class FragmentItemCoreView : MonoBehaviour, ICoreView<FragmentItemInfo>
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<FragmentItemInfo> _onUpdateView;


        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(FragmentItemInfo data)
        {
            _onUpdateView?.Invoke(data);
        }

        public void AddCallBackUpdateView(IMemberView<FragmentItemInfo> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }
}