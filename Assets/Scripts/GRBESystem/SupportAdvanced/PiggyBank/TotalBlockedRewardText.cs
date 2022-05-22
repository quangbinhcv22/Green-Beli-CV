using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.SupportAdvanced.PiggyBank
{
    public class TotalBlockedRewardText : MonoBehaviour
    {
        [SerializeField] TMP_Text claimYetText;

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

            var totalBlocked = getRewardHistoryAllService.TotalBlocked;
            claimYetText.SetText($"{totalBlocked:N0}");
        }
    }
}