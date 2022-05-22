using GEvent;
using TigerForge;
using TMPro;
using UnityEngine;

namespace Network.Service.ClientUpdate.HeroList
{
    public class TotalClaimedRewardText : MonoBehaviour
    {
        [SerializeField] private TMP_Text claimText;

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
            var getRewardHistoryAllService = NetworkService.Instance.services.getRewardHistoryAll;
            if (getRewardHistoryAllService.Response.IsError) return;

            var totalClaimed = getRewardHistoryAllService.TotalClaimed;
            claimText.SetText($"{totalClaimed:N0}");
        }
    }
}