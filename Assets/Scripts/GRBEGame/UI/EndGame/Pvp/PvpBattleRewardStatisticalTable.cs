using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GRBESystem.Definitions;
using Network.Messages.Battle;
using Network.Service;
using Network.Service.Implement;
using UnityEngine;

namespace GRBEGame.UI.EndGame.Pvp
{
    public class PvpBattleRewardStatisticalTable : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private PvpPlayerRewardStatisticalTable cellViewTemplate;

        [SerializeField] private List<Owner> players = new List<Owner> { Owner.Self, Owner.Opponent };


        private void OnEnable()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn) return;
            scroller.Delegate = this;
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return players.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 183.9955f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (PvpPlayerRewardStatisticalTable)scroller.GetCellView(cellViewTemplate);
            cellView.UpdateView(GetPlayerInfo(players[dataIndex]), players[dataIndex]);

            return cellView;
        }

        private EndGameResponse.PlayerInfo GetPlayerInfo(Owner owner)
        {
            var endGameResponse = EndGameServerService.Response;
            return endGameResponse.IsError
                ? new EndGameResponse.PlayerInfo()
                : endGameResponse.data.GetPlayerInfo(owner);
        }
    }
}