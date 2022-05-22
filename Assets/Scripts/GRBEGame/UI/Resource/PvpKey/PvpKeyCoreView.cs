using GRBEGame.UI.DataView;
using UnityEngine;
using UnityEngine.Events;

namespace GRBEGame.UI.Resource.PvpKey
{
    public class PvpKeyCoreView : MonoBehaviour, ICoreView<PvpChest>
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<PvpChest> _onUpdateView;


        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(PvpChest data)
        {
            _onUpdateView?.Invoke(data);
        }

        public void AddCallBackUpdateView(IMemberView<PvpChest> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }
}