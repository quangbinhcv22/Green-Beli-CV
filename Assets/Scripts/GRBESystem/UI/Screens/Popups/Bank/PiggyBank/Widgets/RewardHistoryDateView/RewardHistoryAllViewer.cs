using System.Collections.Generic;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank.PiggyBank.Widgets.RewardHistoryDateView
{
    public class RewardHistoryAllViewer : MonoBehaviour
    {
        [SerializeField, Space] private RewardHistoryDateViewer rewardHistoryDateViewerPrefab;
        [SerializeField] private Transform contentParent;

        public List<RewardHistoryDateViewer> rewardHistoryDateViewers = new List<RewardHistoryDateViewer>();

        [SerializeField]
        private List<RewardHistoryByDateResponse> rewardHistoryAllResponse = new List<RewardHistoryByDateResponse>();


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.GetRewardHistoryAll, OnGetRewardAllResponse);
            EventManager.StartListening(EventName.Server.ClaimRewardByDate, SendRequestGetDataToServer);
        }

        private void OnEnable()
        {
            OnGetRewardAllResponse();
            UpdateView();
        }

        private void UpdateView()
        {
            ShowNotClaimedRewardHistoryDateViewer();
        }

        private void ShowNotClaimedRewardHistoryDateViewer()
        {
            for (int i = rewardHistoryDateViewers.Count; i < rewardHistoryAllResponse.Count; i++)
            {
                var rewardHistoryDateViewer = Instantiate(rewardHistoryDateViewerPrefab, parent: contentParent);
                rewardHistoryDateViewers.Add(rewardHistoryDateViewer);
            }

            foreach (var rewardHistoryDateViewer in rewardHistoryDateViewers)
            {
                rewardHistoryDateViewer.gameObject.SetActive(false);
            }

            for (int i = 0; i < rewardHistoryAllResponse.Count; i++)
            {
                rewardHistoryDateViewers[i].UpdateView(rewardHistoryAllResponse[i]);
                rewardHistoryDateViewers[i].gameObject.SetActive(true);
            }
        }


        private void OnGetRewardAllResponse()
        {
            var getRewardAllResponse = NetworkService.Instance.services.getRewardHistoryAll.Response;
            if (getRewardAllResponse.IsError) return;

            rewardHistoryAllResponse = NetworkService.Instance.services.getRewardHistoryAll.Response.data;

            UpdateView();
        }

        private static void SendRequestGetDataToServer()
        {
            NetworkService.Instance.services.getRewardHistoryAll.SendRequest();
        }
    }
}