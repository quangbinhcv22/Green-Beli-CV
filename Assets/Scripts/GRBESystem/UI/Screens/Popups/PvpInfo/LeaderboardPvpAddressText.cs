using Config.Format;
using TMPro;
using UnityEngine;
using GRBEGame.UI.DataView;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    public class LeaderboardPvpAddressText : MonoBehaviour, IMemberView<LeaderboardPvpData>
    {
        [SerializeField] private LeaderboardPvpCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private PlayerAddressFormatConfig formatConfig;
        [SerializeField] private string defaultString = "-";


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.SetText(defaultString);
        }

        public void UpdateView(LeaderboardPvpData data)
        {
            text.SetText(formatConfig.FormattedAddress(data.owner));
        }
    }
}
