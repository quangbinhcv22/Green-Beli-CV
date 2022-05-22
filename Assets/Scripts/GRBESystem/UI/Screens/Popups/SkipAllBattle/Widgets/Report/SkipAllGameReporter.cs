using System.Collections.Generic;
using GEvent;
using GRBEGame.UI.Screen.Inventory;
using Manager.Inventory;
using Network.Messages.SkipAllGame;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.SkipAllBattle.Widgets.Report
{
    public class SkipAllGameReporter : MonoBehaviour
    {
        [SerializeField] private TMP_Text totalBattlesText;
        [SerializeField] private TMP_Text totalVictoryText;
        [SerializeField] private TMP_Text totalLoseText;
        [SerializeField] [Space] private TMP_Text totalBeLiText;
        [SerializeField] private TMP_Text totalTicketText;
        [SerializeField] private TMP_Text totalExpText;

        [SerializeField] private SkipAllItemFragmentRewardPanel itemFragmentRewardPanel;


        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.SkipAllGame, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.SkipAllGame, UpdateView);
        }

        private void UpdateView()
        {
            var responseService = SkipAllGameServerService.Response.data;
            if (responseService == null) return;

            var totalResponse = TotalSkipAllGameResult(responseService);
            totalBattlesText.SetText(totalResponse.totalBattle.ToString());
            totalVictoryText.SetText(totalResponse.totalVictory.ToString());
            totalLoseText.SetText(totalResponse.totalLose.ToString());
            
            totalBeLiText.SetText(totalResponse.totalBeLi.ToString("N0"));
            totalTicketText.SetText(totalResponse.rewardNumberPvpTicket.ToString("N0"));
            totalExpText.SetText(totalResponse.totalExp.ToString());

            itemFragmentRewardPanel.LoadData(totalResponse.totalFragmentItems);
            NetworkService.Instance.services.login.AddPvpTicket(totalResponse.rewardNumberPvpTicket);
        }

        private ReportResult TotalSkipAllGameResult(List<SkipAllGameResponse> responseService)
        {
            var totalResponse = new ReportResult
            {
                totalBattle = responseService.Count,
                totalFragmentItems = new List<FragmentItemInfo>(),
                rewardNumberPvpTicket = default,
            };
            
            totalResponse.totalGFruit =
                SkipAllGameServerService.GetTotalTokenReward(MoneyType.GFruit);
            totalResponse.totalBeLi =
                SkipAllGameServerService.GetTotalTokenReward(MoneyType.BeLi);
            totalResponse.rewardNumberPvpTicket =
                SkipAllGameServerService.GetTotalTokenReward(MoneyType.PvpTicket);

            foreach (var response in responseService)
            {
                if (response.isWin) totalResponse.totalVictory++;
                else totalResponse.totalLose++;
                
                totalResponse.totalExp += response.rewardExp;

                var rewardFragment = response.rewardFragment;
                if (rewardFragment is null) continue;

                var fragmentType = totalResponse.totalFragmentItems.Find(item => item.type == rewardFragment.type);

                if (fragmentType is null)
                    totalResponse.totalFragmentItems.Add(new FragmentItemInfo(rewardFragment));
                else
                    fragmentType.count += rewardFragment.number;
            }

            return totalResponse;
        }
    }

    [System.Serializable]
    public struct ReportResult
    {
        public int totalBattle;
        public int totalVictory;
        public int totalLose;
        public int totalBeLi;
        public int totalGFruit;
        public int totalExp;
        public int rewardNumberPvpTicket;

        public List<FragmentItemInfo> totalFragmentItems;
    }
}