using GEvent;
using GRBESystem.UI.Screens.Popups.PvpInfo;
using Network.Messages;
using Network.Messages.GetPvpContestDetail;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.PvpInfo
{
    public class MyRewardLastPvpSeason : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "{0:N0}";
        [SerializeField] private string textDefault = "-----";


        private void OnEnable()
        {
            if(NetworkService.Instance.IsNotLogged()) return;
            
            UpdateView();
            EventManager.StartListening(EventName.Server.OpenPvpBoxRewardLeaderboard,
                OnOpenPvpBoxRewardLeaderboard);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.OpenPvpBoxRewardLeaderboard,
                OnOpenPvpBoxRewardLeaderboard);
        }

        private void OnOpenPvpBoxRewardLeaderboard()
        {
            UpdateView();
            NetworkService.Instance.services.getRewardHistoryAll.SendRequest();
        }

        private const float PercentRewardPool = 0.05f;
        
        private void UpdateView()
        {
            var seasonRewardResponse =
                EventManager.GetData<MessageResponse<PvpPlayerInfo>>(EventName.Server.OpenPvpBoxRewardLeaderboard);
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            var rate = GetRatePvpSeasonReward(seasonRewardResponse.data.rank);
            
            if (seasonRewardResponse.IsError || loadGameResponse.IsError || rate is (float)default)
            {
                text.SetText(textDefault);
                return;
            }

            var rewardGFruit = rate * seasonRewardResponse.data.totalPVPContestTicket *
                               PercentRewardPool * loadGameResponse.data.pvp.ticket_price;
            text.SetText(string.Format(textFormat, (int) rewardGFruit));
        }

        private float GetRatePvpSeasonReward(int rank)
        {
            var leaderboardData = EventManager.GetData(EventName.UI.Select<LeaderboardRankReward>());
            if (leaderboardData is null) return default;
            
            var leaderboard = (LeaderboardRankReward) leaderboardData;
            return leaderboard.GetRanks().Find(item => item.topMin <= rank && item.topMax >= rank).rateReward;
        }
    }
}
