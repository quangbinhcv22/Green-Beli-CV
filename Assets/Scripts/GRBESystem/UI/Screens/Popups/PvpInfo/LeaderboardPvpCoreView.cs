using UnityEngine;
using UnityEngine.Events;
using GRBEGame.UI.DataView;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    public class LeaderboardPvpCoreView : MonoBehaviour, ICoreView<LeaderboardPvpData>
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<LeaderboardPvpData> _onUpdateView;
        
        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(LeaderboardPvpData data)
        {
            _onUpdateView?.Invoke(data);
        }

        public void AddCallBackUpdateView(IMemberView<LeaderboardPvpData> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }
}
