using System;
using System.Collections.Generic;
using GEvent;
using GRBESystem.Definitions;
using GRBESystem.Entity;
using Network.Messages.GetHeroList;
using Network.Messages.StartGame;
using Network.Service;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;

namespace UI.ScreenController.Window.Battle
{
    [System.Serializable]
    public struct BattleClientData
    {
        const string ID_BOSS_SIGNAL = "BOT";

        [Serializable]
        public struct BossInfo
        {
            public int faction;
            public int maxHealth;
        }

        [Serializable]
        public struct PlayerInfo
        {
            public string id;
            public List<HeroResponse> selectedHeroes;

            public HeroResponse GetMainHero()
            {
                const int mainHeroIndex = 0;
                return selectedHeroes[mainHeroIndex];
            }
        }

        public BossInfo boss;
        public PlayerInfo selfData;
        public PlayerInfo opponentData;
        public BattleMode battleMode;


        public static BattleClientData ConvertFormStartGameResponse(StartGameResponse startGameResponse)
        {
            var selfInfoUnConverted = NetworkService.Instance.GetPlayerInfo(Owner.Self, startGameResponse.players.ToArray());
            var opponentInfoUnConverted = NetworkService.Instance.GetPlayerInfo(Owner.Opponent, startGameResponse.players.ToArray());


            var result = new BattleClientData()
            {
                selfData = new PlayerInfo()
                {
                    id = selfInfoUnConverted.id,
                    selectedHeroes = selfInfoUnConverted.selectedHeros
                },

                opponentData = new PlayerInfo()
                {
                    id = opponentInfoUnConverted.id,
                    selectedHeroes = opponentInfoUnConverted.selectedHeros
                },

                boss = new BossInfo()
                {
                    maxHealth = startGameResponse.boss.healthInit,
                    faction = startGameResponse.boss.faction,
                },
            };

            result.battleMode = result.opponentData.id.Contains(ID_BOSS_SIGNAL) ? BattleMode.PvE : BattleMode.PvP;

            return result;
        }
    }


}