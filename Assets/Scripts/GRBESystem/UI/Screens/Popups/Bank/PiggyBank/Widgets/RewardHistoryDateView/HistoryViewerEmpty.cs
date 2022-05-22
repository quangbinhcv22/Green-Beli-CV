using System.Linq;
using GEvent;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank.PiggyBank.Widgets.RewardHistoryDateView
{
    public class HistoryViewerEmpty : MonoBehaviour
    {
        [SerializeField] private GameObject emptyObject;

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.GetRewardHistoryAll, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetRewardHistoryAll, UpdateView);
        }

        private void UpdateView()
        {
            var getRewardAllResponse = NetworkService.Instance.services.getRewardHistoryAll.Response;
            if (getRewardAllResponse.IsError) return;

            var rewardHistoryAllResponse = getRewardAllResponse.data;
            var notHistory = rewardHistoryAllResponse.Any() is false;

            emptyObject.SetActive(notHistory);
        }
    }
}