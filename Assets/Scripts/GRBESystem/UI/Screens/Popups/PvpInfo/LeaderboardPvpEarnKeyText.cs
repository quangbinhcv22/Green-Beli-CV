using TMPro;
using UnityEngine;
using GRBEGame.UI.DataView;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    public class LeaderboardPvpEarnKeyText : MonoBehaviour, IMemberView<LeaderboardPvpData>
    {
        [SerializeField] private LeaderboardPvpCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "{0}";
        [SerializeField] private string defaultString;


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
            text.SetText(string.Format(textFormat, data.numberGoldChest));
        }
    }
}
