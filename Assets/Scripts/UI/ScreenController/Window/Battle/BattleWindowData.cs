using System.Collections.Generic;
using System.Linq;
using GRBESystem.Definitions;
using GRBESystem.Entity;
using GRBESystem.Model.BossModel;
using Network.Messages.AttackBoss;
using Network.Messages.GetHeroList;
using Network.Messages.StartGame;
using Network.Messages.StartPhase;
using Network.Messages.StartRound;
using Network.Service;
using UI.ScreenController.Window.Battle.Mode;

namespace UI.ScreenController.Window.Battle
{
    [System.Serializable]
    public struct BattleWindowData
    {
        // public BattleMode battleMode;
        // public TimeLine timeLine;
        // public BossInfo bossInfo;
        //
        // public PlayerInfo selfInfo;
        // public PlayerInfo opponentInfo;
        //
        // public AttackBossTime attackBossTime;
        //
        // public long GetMainHeroId(string playerAddress)
        // {
        //     return selfInfo.address == playerAddress ? selfInfo.GetMainHeroId() : opponentInfo.GetMainHeroId();
        // }
        //
        //
        // public void SetDataFromStartGameResponse(StartGameResponse response)
        // {
        //     bossInfo = new BossInfo()
        //     {
        //         identity = (BossIdentity)response.boss.faction,
        //         health = new BossInfo.Health(response.boss.healthInit),
        //     };
        //
        //     var playersResponse = response.players.ToArray();
        //
        //     var selfInfoUnConverted = NetworkService.Instance.GetPlayerInfo(Owner.Self, playersResponse);
        //     var selfHeroList = selfInfoUnConverted.selectedHeros;
        //
        //     var opponentInfoUnConverted = NetworkService.Instance.GetPlayerInfo(Owner.Opponent, playersResponse);
        //     var opponentHeroList =
        //         opponentInfoUnConverted.selectedHeros;
        //
        //     selfInfo = new PlayerInfo()
        //     {
        //         address = selfInfoUnConverted.id,
        //         selectedHero = selfHeroList.Select(hero => hero.GetID()).ToList(),
        //         totalDamage = 0,
        //     };
        //
        //     opponentInfo = new PlayerInfo()
        //     {
        //         address = opponentInfoUnConverted.id,
        //         selectedHero = opponentHeroList.Select(hero => hero.GetID()).ToList(),
        //         totalDamage = 0,
        //     };
        //
        //     attackBossTime = new AttackBossTime()
        //     {
        //         attackerAddress = string.Empty,
        //         selectedCard = 0,
        //         attackDamage = 0,
        //         isCriticalDamage = false,
        //     };
        // }
        //
        // public void SetDataFromAttackBossResponse(AttackBossResponse attackBossResponse)
        // {
        //     attackBossTime = new AttackBossTime()
        //     {
        //         attackerAddress = attackBossResponse.GetPlayerTakeWin().playerId,
        //         selectedCard = attackBossResponse.GetPlayerTakeWin().selectedCard,
        //         attackDamage = attackBossResponse.attackDamage,
        //         isCriticalDamage = attackBossResponse.isCritDamage,
        //     };
        //
        //     selfInfo.totalDamage += selfInfo.IsMe(attackBossTime.attackerAddress) ? attackBossTime.attackDamage : 0;
        //     opponentInfo.totalDamage +=
        //         opponentInfo.IsMe(attackBossTime.attackerAddress) ? attackBossTime.attackDamage : 0;
        // }
        //
        // public void SetDataFromStartPhaseResponse(StartPhaseResponse startPhaseResponse)
        // {
        //     timeLine.phase.number = startPhaseResponse.phaseIndex;
        //     timeLine.phase.damageFactor = startPhaseResponse.damagePercent;
        // }
        //
        // public void SetDataFromStartRoundResponse(StartRoundResponse startRoundResponse)
        // {
        //     timeLine.turn = startRoundResponse.roundIndex;
        //     timeLine.timeSelectThisTurn = startRoundResponse.roundTimeout;
        // }
        //
        //
        // public PlayerInfo GetPlayerInfo(Owner owner)
        // {
        //     return owner == Owner.Self ? selfInfo : opponentInfo;
        // }
        //
        //
        // [System.Serializable]
        // public struct TimeLine
        // {
        //     public Phrase phase;
        //     public int turn;
        //     public int timeSelectThisTurn;
        //
        //     [System.Serializable]
        //     public struct Phrase
        //     {
        //         public int number;
        //         public float damageFactor;
        //     }
        // }
        //
        // [System.Serializable]
        // public struct BossInfo
        // {
        //     public BossIdentity identity;
        //     public Health health;
        //
        //     [System.Serializable]
        //     public struct Health
        //     {
        //         public int current;
        //         public int max;
        //
        //         public Health(int health)
        //         {
        //             current = health;
        //             max = health;
        //         }
        //     }
        // }
        //
        // [System.Serializable]
        // public struct PlayerInfo
        // {
        //     public string address;
        //     public List<long> selectedHero;
        //     public int totalDamage;
        //     public int selectedCard;
        //
        //
        //     public long GetMainHeroId()
        //     {
        //         return selectedHero[0];
        //     }
        //
        //     public List<long> GetSubHeroIds()
        //     {
        //         var result = new List<long>();
        //
        //         for (int i = 1; i < selectedHero.Count; i++)
        //         {
        //             result.Add(selectedHero[i]);
        //         }
        //
        //         return result;
        //     }
        //
        //     public bool IsMe(string playerAddress)
        //     {
        //         return playerAddress == address;
        //     }
        // }
        //
        // [System.Serializable]
        // public struct AttackBossTime
        // {
        //     public string attackerAddress;
        //     public int selectedCard;
        //     public int attackDamage;
        //     public bool isCriticalDamage;
        // }
    }
}