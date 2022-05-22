using UnityEngine;
using UnityEngine.Events;

namespace GRBEGame.UI.Screen.Inventory
{
    public class FusionStoneCoreView : MonoBehaviour, ICoreView<FusionStoneItem>
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<FusionStoneItem> _onUpdateView;


        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(FusionStoneItem data)
        {
            _onUpdateView?.Invoke(data);
        }

        public void AddCallBackUpdateView(IMemberView<FusionStoneItem> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }
}
