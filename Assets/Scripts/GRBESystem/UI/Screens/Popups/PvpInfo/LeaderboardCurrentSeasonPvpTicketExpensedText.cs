using GRBEGame.UI.DataView;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    [RequireComponent(typeof(TMP_Text))]
    public class LeaderboardCurrentSeasonPvpTicketExpensedText : MonoBehaviour, IMemberView<LeaderboardPvpData>
    {
        [SerializeField] private LeaderboardPvpCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textDefault = "-----";


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
            text ??= GetComponent<TMP_Text>();
        }

        public void UpdateDefault()
        {
            text.SetText(textDefault);
        }

        public void UpdateView(LeaderboardPvpData data)
        {
            text.SetText(data.numberSpendPvpTicket.ToString("N0"));
        }
    }
}
