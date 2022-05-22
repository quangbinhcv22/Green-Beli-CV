using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    public class LeaderboardViewer :MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller enhanceScroller;
        [SerializeField] private LeaderboardCellView leaderboardPvpPrefab;
        [SerializeField] private TMP_Text noPlayersFoundText;

        private List<LeaderboardPvpData> _dataList;


        private void Awake()
        {
            _dataList ??= new List<LeaderboardPvpData>();
            enhanceScroller.Delegate = this;
        }
        
        private void OnEnable()
        {
            OnLoadLeaderboardPVP();
            EventManager.StartListening(EventName.UI.Select<LeaderboardRankReward>(), OnLoadLeaderboardPVP);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.Select<LeaderboardRankReward>(), OnLoadLeaderboardPVP);
        }

        private void OnLoadLeaderboardPVP()
        {
            const int countBonus = 1;

            var response = NetworkService.Instance.services.getPvpContestDetail.Response;
            if (NetworkService.Instance.IsNotLogged() || response.IsError ||
                response.data.topAccounts is null) return;
            
            noPlayersFoundText.gameObject.SetActive(response.data.topAccounts.Count < countBonus);
            
            _dataList?.Clear();
            response.data.topAccounts.ForEach(item =>
            {
                _dataList.Add(new LeaderboardPvpData
                {
                    owner = item.owner,
                    numberGoldChest = item.numberPVPGoldChest,
                    rank = response.data.topAccounts.IndexOf(item) + countBonus,
                    numberSpendPvpTicket = item.numberPVPSpendTicket,
                    nation =  item.nation,
                });
            });

            enhanceScroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _dataList.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 60f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (LeaderboardCellView) scroller.GetCellView(leaderboardPvpPrefab);
            cellView.name = dataIndex.ToString();
            cellView.UpdateView(_dataList[dataIndex]);

            return cellView;
        }
    }
}
