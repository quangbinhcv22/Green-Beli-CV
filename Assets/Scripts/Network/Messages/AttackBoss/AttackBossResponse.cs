using System.Collections.Generic;
using System.Linq;
using Network.Service;

namespace Network.Messages.AttackBoss
{
    [System.Serializable]
    public struct AttackBossResponse
    {
        [System.Serializable]
        public struct PlayerInfo
        {
            public int selectedCard;
            public string playerId;
        }

        public int roundIndex;
        public string playerAttackBoss;
        public List<PlayerInfo> players;
        public int bossHealth;
        public int attackDamage;
        public bool isCritDamage;
        
        public PlayerInfo GetSelfInfo()
        {
            if (players is null || players.Any() is false) return new PlayerInfo();

            var selfAddress = NetworkService.Instance.services.login.LoginResponse.id;
            return players.Find(player => player.playerId == selfAddress);
        }
        
        public PlayerInfo GetOpinionInfo()
        {
            if (players is null || players.Any() is false) return new PlayerInfo();

            var selfAddress = NetworkService.Instance.services.login.LoginResponse.id;
            return players.Find(player => player.playerId != selfAddress);
        }
    }

    public static class AttackBossResponseUtils
    {
        public static bool IsRoundDraw(this AttackBossResponse response)
        {
            var firstSelectedCard = response.players.FirstOrDefault().selectedCard;
            return response.players.All(player => player.selectedCard == firstSelectedCard);
        }
        
        public static bool IsRoundLose(this AttackBossResponse response)
        {
            return response.GetOpinionInfo().selectedCard > response.GetSelfInfo().selectedCard;
        }

        public static int GetCardWin(this AttackBossResponse response)
        {
            return response.players.OrderByDescending(player => player.selectedCard).First().selectedCard;
        }

        public static AttackBossResponse.PlayerInfo GetPlayerTakeWin(this AttackBossResponse response)
        {
            return response.players.OrderByDescending(player => player.selectedCard).First();
        }
    }
}