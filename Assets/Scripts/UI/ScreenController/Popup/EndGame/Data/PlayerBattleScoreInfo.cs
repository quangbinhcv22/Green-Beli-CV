using System;

namespace UI.ScreenController.Popup.EndGame.Data
{
    [Serializable]
    public struct PlayerBattleScoreInfo
    {
        public string ownerAddress;
        public long mainHeroId;
        public int score;

        public static PlayerBattleScoreInfo GetWinner(PlayerBattleScoreInfo playerA, PlayerBattleScoreInfo playerB)
        {
            return playerA.score >= playerB.score ? playerA : playerB;
        }
    }
}