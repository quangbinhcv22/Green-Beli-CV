using System.Collections.Generic;
using GEvent;
using Network.Messages.Battle;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine.Assertions;

namespace Service.Server.EndGame
{
    [System.Serializable]
    public struct EndGameClientData : IConvertedResponseClientData<EndGameResponse>
    {
        private static readonly string NoOneAddress = string.Empty;
        public List<EndGameResponse.PlayerInfo> players;

        public void SetDataFromResponse(EndGameResponse response)
        {
            players = response.player;
        }

        public ResultBattle GetResultBattle()
        {
            var endGameResponse = EndGameServerService.Response;
            if (endGameResponse.IsError) return ResultBattle.Victory;

            var battleMode = GetBattleMode();
            var addressSelf = NetworkService.Instance.services.login.LoginResponse.id;
            var battleResult = addressSelf == GetVictoryPlayerInfo().playerId ? ResultBattle.Victory : ResultBattle.Lose;

            
            if (battleMode is BattleMode.PvP)
            {
                return battleResult;
            }

            
            if (endGameResponse.data.IsBossDie() is false) return ResultBattle.Lose;

            if (players[0].totalAtkDamage == players[1].totalAtkDamage)
            {
                return ResultBattle.Draw;
            }

            return battleResult;
        }

        public enum ResultBattle
        {
            Victory = 0,
            Lose = 1,
            Draw = 2,
        }

        public EndGameResponse.PlayerInfo GetVictoryPlayerInfo()
        {
            const int standardPlayerNumber = 2;
            Assert.IsTrue(players.Count == standardPlayerNumber);

            if (players[0].totalAtkDamage == players[1].totalAtkDamage)
            {
                return new EndGameResponse.PlayerInfo() { playerId = NoOneAddress };
            }

            return GetBattleMode() switch
            {
                BattleMode.PvP => players[0].TotalScore > players[1].TotalScore ? players[0] : players[1],
                _ => players[0].totalAtkDamage > players[1].totalAtkDamage ? players[0] : players[1],
            };
        }

        public BattleMode GetBattleMode()
        {
            var boxedBattleMode = EventManager.GetData(EventName.Client.Battle.BattleMode);
            return boxedBattleMode is null ? BattleMode.PvE : (BattleMode)boxedBattleMode;
        }

        public EndGameResponse.PlayerInfo GetSelfInfo()
        {
            foreach (var player in players)
            {
                if (player.playerId == NetworkService.Instance.services.login.LoginResponse.id)
                {
                    return player;
                }
            }

            return players[0];
        }

        public EndGameResponse.PlayerInfo GetOpponentInfo()
        {
            foreach (var player in players)
            {
                if (player.playerId != NetworkService.Instance.services.login.LoginResponse.id)
                {
                    return player;
                }
            }

            throw new KeyNotFoundException("Opponent not found in result battle (Client)");
        }
    }
}