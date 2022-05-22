using System;
using Network.Messages.Battle;
using Network.Messages.GetHeroList;
using Network.Service;
using UI.ScreenController.Popup.EndGame.Widget;
using UI.ScreenController.Popup.EndGame.Widget.SelectedCardsHistory;
using UI.ScreenController.Window.Battle;
using static UI.ScreenController.Popup.EndGame.Data.PlayerBattleScoreInfo;

namespace UI.ScreenController.Popup.EndGame.Data
{
    [Serializable]
    public struct BattleResultPopupData
    {
        public enum ResultBattle
        {
            Victory = 0,
            Lose = 1,
        }

        public PlayerBattleScoreInfo selfBattleScoreInfo;
        public PlayerBattleScoreInfo opponentBattleScoreInfo;

        public RewardInfo rewardInfo;
        public ExpHeroProgressInfo expHeroProgressInfo;

        public ResultBattle resultBattle;


        public long GetWinHeroId()
        {
            var winner = GetWinner(selfBattleScoreInfo, opponentBattleScoreInfo);
            return winner.mainHeroId;
        }


        public BattleResultPopupData ChangeData(BattleClientData battleClientData, EndGameResponse endGameResponse)
        {
            var selfBattleData = battleClientData.selfData;
            var opponentBattleData = battleClientData.opponentData;

            const int firstIndex = 0, secondIndex = 1;
            var isSelfAtFirstPlayerIndex = NetworkService.Instance.IsSelf(endGameResponse.player[firstIndex].playerId);
            var selfEndGameData = endGameResponse.player[isSelfAtFirstPlayerIndex ? firstIndex : secondIndex];
            var opponentEndGameData = endGameResponse.player[isSelfAtFirstPlayerIndex ? secondIndex : firstIndex];


            selfBattleScoreInfo = new PlayerBattleScoreInfo()
            {
                mainHeroId = selfBattleData.GetMainHero().GetID(),
                ownerAddress = selfBattleData.id,
                score = selfEndGameData.totalAtkDamage,
            };

            opponentBattleScoreInfo = new PlayerBattleScoreInfo()
            {
                mainHeroId = opponentBattleData.GetMainHero().GetID(),
                ownerAddress = opponentBattleData.id,
                score = opponentEndGameData.totalAtkDamage,
            };


            rewardInfo = new RewardInfo() { coin = selfEndGameData.TotalToken };
            expHeroProgressInfo = new ExpHeroProgressInfo() { scoreAdd = selfEndGameData.rewardExp };


            resultBattle = selfBattleScoreInfo.score >= opponentBattleScoreInfo.score
                ? ResultBattle.Victory
                : ResultBattle.Lose;

            return this;
        }
    }
}