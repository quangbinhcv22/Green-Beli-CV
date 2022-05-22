using System.Collections.Generic;
using GEvent;
using Network.Messages.LoadGame;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;


namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    public class PvpContestDetailHandler : MonoBehaviour
    {
        private LeaderboardRankReward _leaderboardRankReward;

        
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.EndGame, UpdatePlayerEarnKey);
            EventManager.StartListening(EventName.Server.GetPvpContestDetail, OnGetPvpContestDetail);
        }

        private void OnEnable()
        {
            if (NetworkService.Instance.IsNotLogged()) return;
            NetworkService.Instance.services.getPvpContestDetail.SendRequest();
        }

        private void OnGetPvpContestDetail()
        {
            var response = NetworkService.Instance.services.loadGameConfig.Response;
            if(response.IsError) return;
            
            _leaderboardRankReward = new LeaderboardRankReward(response.data.pvp);
            EventManager.EmitEventData(EventName.UI.Select<LeaderboardRankReward>(), _leaderboardRankReward);
        }

        private void UpdatePlayerEarnKey()
        {
            var response = EndGameServerService.Response;
            if (NetworkService.Instance.IsNotLogged() || response.IsError)
                return;

            if (GetBattleMode() is BattleMode.PvP)
                NetworkService.Instance.services.login.AddPvpWinGame();
        }

        private BattleMode GetBattleMode()
        {
            var data = EventManager.GetData(EventName.Client.Battle.BattleMode);
            return data is null ? BattleMode.None : (BattleMode) data;
        }
    }

    [System.Serializable]
    public class LeaderboardRankReward
    {
        private List<Reward> _leaderboardRewards;

        public LeaderboardRankReward(LoadGameConfigResponse.Pvp pvp)
        {
            const int nextIndex = 1;
            const float moneyPoolPercent = 0.05f;

            var pvpContestResponse = NetworkService.Instance.services.getPvpContestDetail.Response;
            var totalSpendPvpTicket =
                pvpContestResponse.IsError ? default : pvpContestResponse.data.totalSpendPVPTicket;

            _leaderboardRewards = new List<Reward>();
            pvp.leaderboard_reward.ForEach(reward =>
            {
                var newString = reward.top.Split('-');

                var top = int.Parse(newString[default]);
                _leaderboardRewards.Add(new Reward
                {
                    topMin = top,
                    topMax = newString.Length > nextIndex ? int.Parse(newString[nextIndex]) : top,
                    gFruit = (int)(reward.rate_gfruit_reward_per_prize * totalSpendPvpTicket * pvp.ticket_price * moneyPoolPercent),
                    rateReward = reward.rate_gfruit_reward_per_prize,
                });
            });
        }

        public List<Reward> GetRanks()
        {
            return _leaderboardRewards;
        }
        
        public int GetMyGFruitRewardLastSeason()
        {
            var getPvpDetailService = NetworkService.Instance.services.getPvpContestDetail;
            if (getPvpDetailService.Response.IsError || getPvpDetailService.Response.data.myReward is null)
                return default;

            return (int) (getPvpDetailService.GetTotalSeasonPvpMoneyReward(
                              GetPvpContestDetailServerService.PvpSeasonType.Last) *
                          _leaderboardRewards.Find(item =>
                              getPvpDetailService.Response.data.myReward.rank >= item.topMin &&
                              getPvpDetailService.Response.data.myReward.rank <= item.topMax).rateReward);
        }
    }

    

    [System.Serializable]
    public struct Reward
    {
        public int topMin;
        public int topMax;
        public int gFruit;
        public float rateReward;
    }
}
