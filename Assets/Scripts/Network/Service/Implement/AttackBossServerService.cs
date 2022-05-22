using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using GNetwork;
using GRBESystem.Definitions;
using Network.Messages;
using Network.Messages.AttackBoss;
using Newtonsoft.Json;
using TigerForge;
using UIFlow;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(AttackBossServerService), menuName = "NetworkAPI/AttackBoss")]
    public partial class AttackBossServerService : ScriptableObject, IServerAPI
    {
        private static AttackBossServerService Instance => NetworkApiManager.GetAPI<AttackBossServerService>();

        public static MessageResponse<AttackBossResponse> Response => Instance._response;
        [NonSerialized] private MessageResponse<AttackBossResponse> _response;


        [SerializeField] private AttackBossScreenHandler screenHandler;

        private readonly AttackBossStatistical _damageStatistical = new AttackBossStatistical();


        public string APIName => EventName.Server.AttackBoss;

        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<AttackBossResponse>>(message);
            if (_response.IsError) return;

            _damageStatistical.Add(_response.data.playerAttackBoss, _response.data.attackDamage);

            screenHandler.OnSuccess();
            CalculateUserLoseStreak();
        }


        public static void ResetDamageStatistical() => Instance._damageStatistical.Reset();
        public static int GetTotalDamage(Owner owner) => Instance._damageStatistical.GetDamage(owner);


        public static void ReCalculateUserLoseStreak()
        {
            Instance._playerLoseStreakInfo.Address = string.Empty;
            Instance._playerLoseStreakInfo.LoseStreak = default;
        }

        private readonly PlayerLoseStreakInfo _playerLoseStreakInfo = new PlayerLoseStreakInfo();

        private void CalculateUserLoseStreak()
        {
            if (_response.IsError || string.IsNullOrEmpty(_response.data.playerAttackBoss))
            {
                ReCalculateUserLoseStreak();
                EventManager.EmitEventData(EventName.PlayerEvent.LoseTurnBattle, _playerLoseStreakInfo);
                return;
            }

            if (string.IsNullOrEmpty(_playerLoseStreakInfo.Address) ||
                _playerLoseStreakInfo.Address == _response.data.playerAttackBoss)
            {
                _playerLoseStreakInfo.LoseStreak = default;
                _playerLoseStreakInfo.Address = _response.data.players
                    .Find(player => player.playerId != _response.data.playerAttackBoss).playerId;
            }

            _playerLoseStreakInfo.LoseStreak++;
            EventManager.EmitEventData(EventName.PlayerEvent.LoseTurnBattle, _playerLoseStreakInfo);
        }
    }

    public class PlayerLoseStreakInfo
    {
        public int LoseStreak;
        public string Address;

        public PlayerLoseStreakInfo()
        {
            LoseStreak = default;
            Address = string.Empty;
        }
    }

    public partial class AttackBossServerService
    {
        public static DelayResponseCallbackConfig delayResponseCallbackConfig;

        [System.Serializable]
        public struct DelayResponseCallbackConfig
        {
            public float showCardWin;
            public float hideCardWin;
            public float heroStartAttack;
            public float showDamageText;
            public float healthBarChange;
        }
    }

    [Serializable]
    public class AttackBossStatistical
    {
        private readonly Dictionary<string, int> _totalDamage = new Dictionary<string, int>();

        public void Reset()
        {
            _totalDamage.Clear();
        }

        public void Add(string address, int damage)
        {
            if (string.IsNullOrEmpty(address)) return;

            if (_totalDamage.ContainsKey(address)) _totalDamage[address] += damage;
            else _totalDamage.Add(address, damage);
        }

        public int GetDamage(string address)
        {
            return _totalDamage.ContainsKey(address) ? _totalDamage[address] : default;
        }

        public int GetDamage(Owner owner)
        {
            if (_totalDamage.Any() is false) return default;

            var isSelf = owner is Owner.Self;
            var selfAddress = NetworkService.playerInfo.address;

            return _totalDamage.FirstOrDefault(statistical => (statistical.Key == selfAddress) == isSelf).Value;
        }
    }

    [Serializable]
    public class AttackBossScreenHandler
    {
        [SerializeField] private UIRequest screenRequest;

        public void OnSuccess()
        {
            screenRequest.SendRequest();
        }
    }
}