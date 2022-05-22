using Network.Messages.GetPvpContestDetail;
using UnityEngine;
using UnityEngine.Events;

namespace GRBEGame.UI.DataView.Pvp
{
    public class PvpPlayerCoreView : MonoBehaviour, ICoreView<PvpPlayerInfo>
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<PvpPlayerInfo> _onUpdateView;


        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(PvpPlayerInfo data)
        {
            _onUpdateView?.Invoke(data);
        }

        public void AddCallBackUpdateView(IMemberView<PvpPlayerInfo> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }
}