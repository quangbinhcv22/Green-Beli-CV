using GEvent;
using GRBEGame.UI.DataView;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    [RequireComponent(typeof(TMP_Text))]
    public class LeaderboardPvpGFruitRewardText : MonoBehaviour, IMemberView<LeaderboardPvpData>
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
            var rankData = EventManager.GetData(EventName.UI.Select<LeaderboardRankReward>());
            if (rankData is null)
            {
                UpdateDefault();
                return;
            }

            var rankReward = (LeaderboardRankReward) rankData;
            rankReward.GetRanks().ForEach(rank =>
            {
                if (data.rank >= rank.topMin && data.rank <= rank.topMax)
                {
                    text.SetText(rank.gFruit.ToString("N0"));
                }
            });
        }
    }
}
