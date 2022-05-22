using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    [RequireComponent(typeof(LeaderboardPvpCoreView))]
    public class LeaderboardCellView : EnhancedScrollerCellView
    {
        [SerializeField] private LeaderboardPvpCoreView leaderboardPvpCoreView;
        
        public void UpdateView(LeaderboardPvpData data)
        {
            leaderboardPvpCoreView.UpdateView(data);
        }

        private void OnValidate()
        {
            leaderboardPvpCoreView = GetComponent<LeaderboardPvpCoreView>();
        }
    }

    [System.Serializable]
    public struct LeaderboardPvpData
    {
        public string owner;
        public int numberGoldChest;
        public int rank;
        public int numberSpendPvpTicket;
        public string nation;
    }
}
