using System;
using System.Collections.Generic;
using System.Linq;
using GRBEGame.UI.Resource.PvpKey;
using GRBESystem.Definitions;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using Newtonsoft.Json;

namespace Network.Messages.Battle
{
    [Serializable]
    public struct EndGameResponse
    {
        public string skipPlayerId;
        public string skipGameMode;
        public List<PlayerInfo> player;

        public PlayerInfo GetSelfInfo()
        {
            if (player is null || player.Any() is false) return new PlayerInfo();

            var selfAddress = NetworkService.Instance.services.login.LoginResponse.id;
            return player.Find(player => player.playerId == selfAddress);
        }

        private PlayerInfo GetOpponentInfo()
        {
            if (player is null || player.Any() is false) return new PlayerInfo();

            var selfAddress = NetworkService.Instance.services.login.LoginResponse.id;
            return player.Find(player => player.playerId != selfAddress);
        }

        public PlayerInfo GetPlayerInfo(Owner owner)
        {
            return owner.Equals(Owner.Self) ? GetSelfInfo() : GetOpponentInfo();
        }

        public bool IsOpinionQuitPvp()
        {
            return GetSelfInfo().playerId != skipPlayerId && skipGameMode.Equals(nameof(SkipGameMode.PLAYER_DISCONNECT));
        }
        
        public QualityChest PvpChest(PlayerInfo playerInfo)
        {
            return IsOpinionQuitPvp() && playerInfo.playerId.Equals(skipPlayerId) ? QualityChest.None :
                playerInfo.rewardGFruit > (int) default ? QualityChest.Gold : QualityChest.Silver;
        }

        public bool HaveDropGFruit(Owner owner = Owner.Self)
        {
            return GetPlayerInfo(owner).rewardGFruit > (int) default;
        }

        public bool HaveDropItemFragment(Owner owner = Owner.Self)
        {
            return GetPlayerInfo(owner).rewardFragment != null;
        }

        public bool HaveDropPvPTicket(Owner owner = Owner.Self)
        {
            return GetPlayerInfo(owner).rewardNumberPVPTicket > (int)default;
        }

        public bool IsLargestScoreInBattle()
        {
            return GetSelfInfo().TotalScore > GetOpponentInfo().TotalScore;
        }

        public bool IsBossDie()
        {
            var bossHealth = StartGameServerService.Response.data.boss.healthInit;
            var allDamage = player.Sum(player => player.totalAtkDamage);

            var isBossDie = allDamage >= bossHealth;

            return isBossDie;
        }

        [Serializable]
        public struct PlayerInfo
        {
            [JsonProperty("rewardGFRToken")]
            public int rewardGFruit;
            
            public List<RewardFragment> rewardMaterials;
            
            [JsonProperty("rewardMaterial")]
            public RewardFragment rewardFragment;


            public string playerId;
            public int totalAtkDamage;
            
            public int rewardExp;
            public int rewardGFRTokenOnDamage;
            public int rewardGFRTokenOnLastHit;
            public int rewardNumberPVPTicket;
            public float lastHitScore;
            public float totalAtkDamageScore;
            public List<int> historySelectedCards;

            [Serializable]
            public class RewardFragment
            {
                public int number;
                public int type;
            }
            
            public float TotalScore => totalAtkDamageScore + lastHitScore;

            public int TotalToken => rewardGFRTokenOnDamage + rewardGFRTokenOnLastHit;
            
            public HeroResponse MainHero()
            {
                var startGameResponse = StartGameServerService.Response;
                return startGameResponse.IsError ? new HeroResponse(default) : startGameResponse.data.GetMainHero(playerId);
            }
        }
    }
    
    [System.Serializable]
    public enum SkipGameMode
    {
        PLAYER_DISCONNECT = 1,
    }
}