using GRBEGame.UI.DataView;
using Localization.Nation;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    public class LeaderboardPlayerNationImage : MonoBehaviour, IMemberView<LeaderboardPvpData>
    {
        [SerializeField] private LeaderboardPvpCoreView coreView;
        [SerializeField] private Image image;
        [SerializeField] private NationConfig nationConfig;

        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetNation(string.Empty);
        }

        public void UpdateView(LeaderboardPvpData data)
        {
            SetNation(data.nation);
        }

        private void SetNation(string nation) => image.sprite = nationConfig.GetNation(nation).art;
    }
}