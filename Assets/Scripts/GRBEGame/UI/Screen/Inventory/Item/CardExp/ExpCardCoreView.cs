using UnityEngine;
using UnityEngine.Events;

namespace GRBEGame.UI.Screen.Inventory
{
    public class ExpCardCoreView : MonoBehaviour, ICoreView<ExpCardItem>
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<ExpCardItem> _onUpdateView;


        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(ExpCardItem data)
        {
            _onUpdateView?.Invoke(data);
        }

        public void AddCallBackUpdateView(IMemberView<ExpCardItem> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }
}
