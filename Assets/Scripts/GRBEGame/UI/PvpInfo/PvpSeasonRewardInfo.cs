using System.Collections.Generic;
using GEvent;
using GRBESystem.UI.Screens.Popups.PvpInfo;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.PvpInfo
{
    public class PvpSeasonRewardInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text totalSpendTicketText;
        [SerializeField] private List<TMP_Text> rewardTexts;


        private void OnEnable()
        {
            UpdateReward();
            EventManager.StartListening(EventName.UI.Select<LeaderboardRankReward>(), UpdateReward);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.Select<LeaderboardRankReward>(), UpdateReward);
        }
        
        private void UpdateReward()
        {
            if(NetworkService.Instance.IsNotLogged()) return;

            var totalSpendPvpTicket = NetworkService.Instance.services.getPvpContestDetail.Response.data.totalSpendPVPTicket;
            totalSpendTicketText.SetText($"{totalSpendPvpTicket:N0}");
            
            for (var i = (int) default; i < rewardTexts.Count; i++)
                rewardTexts[i].SetText(GetLeaderboardRankReward().GetRanks()[i].gFruit.ToString("N0"));
        }
        
        private LeaderboardRankReward GetLeaderboardRankReward()
        {
            var rankData = EventManager.GetData(EventName.UI.Select<LeaderboardRankReward>());
            return (LeaderboardRankReward)rankData;
        }
    }
}
