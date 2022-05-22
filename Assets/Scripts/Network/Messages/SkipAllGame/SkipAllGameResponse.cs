using Network.Messages.Battle;
using Newtonsoft.Json;

namespace Network.Messages.SkipAllGame
{
    [System.Serializable]
    public struct SkipAllGameResponse
    {
        public bool isWin;
        
        public int rewardExp;
        [JsonProperty("rewardBELToken")]
        public int rewardBeLiToken;
        [JsonProperty("rewardGFRToken")]
        public int rewardGFruitToken;
        [JsonProperty("rewardNumberPVPTicket")]
        public int rewardNumberPvpTicket;

        public int bossHealthInit;
        public int bossHealthLoss;
        public int totalAtkDamageScore;
        public int lastHitScore;
        public int totalAtkDamage;

        public EndGameResponse.PlayerInfo.RewardFragment rewardFragment;
    }
}
