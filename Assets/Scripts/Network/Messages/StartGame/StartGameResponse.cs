using System;
using System.Collections.Generic;
using System.Linq;
using GRBESystem.Definitions;
using Network.Messages.GetHeroList;
using Network.Service;

namespace Network.Messages.StartGame
{
    [Serializable]
    public class StartGameResponse
    {
        [Serializable]
        public struct BossInfo
        {
            public int faction;
            public int healthInit;
        }

        [Serializable]
        public struct PlayerInfo
        {
            public string id;
            public string nation;
            public List<HeroResponse> selectedHeros;
            public int heroTeamPower;
        }

        public BossInfo boss;
        public List<PlayerInfo> players;

        
        public PlayerInfo GetSelfInfo()
        {
            return players.Find(player => player.id.Equals(NetworkService.Instance.services.login.LoginResponse.id));
        }
        public PlayerInfo GetOpponentInfo()
        {
            return players.Find(player => player.id.Equals(NetworkService.Instance.services.login.LoginResponse.id) == false);
        }

        public PlayerInfo GetPlayerInfo(Owner owner)
        {
            return owner.Equals(Owner.Self) ? GetSelfInfo() : GetOpponentInfo();
        }
        
        public PlayerInfo GetPlayerInfo(string address)
        {
            return players.Find(player => player.id == address);
        }

        public HeroResponse GetMainHero(string playerAddress)
        {
            return players.Find(player => player.id.Equals(playerAddress)).selectedHeros.GetMainHero();
        }
    }
}