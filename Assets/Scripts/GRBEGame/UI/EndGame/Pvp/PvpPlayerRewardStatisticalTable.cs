using EnhancedUI.EnhancedScroller;
using GRBEGame.UI.Resource.PvpKey;
using GRBESystem.Definitions;
using Network.Messages.Battle;
using Network.Service;
using Network.Service.Implement;
using QB.Collection;
using TMPro;
using UI.Widget.HeroCard;
using UnityEngine;

namespace GRBEGame.UI.EndGame.Pvp
{
    public class PvpPlayerRewardStatisticalTable : EnhancedScrollerCellView
    {
        [SerializeField] [Space] private TMP_Text damageText;
        [SerializeField] private TMP_Text lastHitText;
        [SerializeField] private TMP_Text totalText;
        [SerializeField] private TMP_Text teamPowerText;
        [SerializeField] private string scoreFormat = "{0:N0}";

        [SerializeField] [Space] private TMP_Text titleOfOwner;
        [SerializeField] private DefaultableDictionary<Owner, string> titleOfOwners;

        [SerializeField] [Space] private HeroVisual heroVisual;
        [SerializeField] private PvpKeyCoreView pvpKeyCoreView;


        public void UpdateView(EndGameResponse.PlayerInfo playerInfo, Owner owner)
        {
            SetScoreText(damageText, playerInfo.totalAtkDamageScore);
            SetScoreText(lastHitText, playerInfo.lastHitScore);
            SetScoreText(totalText, playerInfo.TotalScore);

            var startGameResponse = StartGameServerService.Response;
            if (startGameResponse.IsError is false)
            {
                var teamPower = startGameResponse.data.GetPlayerInfo(owner).heroTeamPower;
                SetScoreText(teamPowerText, teamPower);
            }


            void SetScoreText(TMP_Text text, float score) => text.SetText(string.Format(scoreFormat, score));

            titleOfOwner.SetText(titleOfOwners[owner]);

            heroVisual.UpdateView(playerInfo.MainHero());

            // pvpKeyCoreView.gameObject.SetActive(playerInfo.HavePvpKey);
            var response = EndGameServerService.Response;
            if (response.IsError || response.data.PvpChest(playerInfo) is QualityChest.None)
            {
                pvpKeyCoreView.UpdateView(new PvpChest() {qualityChest = QualityChest.None});
                return;
            }
            pvpKeyCoreView.UpdateView(new PvpChest() {qualityChest = response.data.PvpChest(playerInfo)});
        }
    }
}