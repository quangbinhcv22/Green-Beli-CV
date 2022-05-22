using GEvent;
using GRBESystem.Definitions;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Widgets.LoseStreak
{
    public class LoseStreakBattleSetter : MonoBehaviour
    {
        [SerializeField] private Owner owner;
        
        private string _playerID;
        private bool _isFirstUpdated;
        
        
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.StartGame, OnStartGame);
            EventManager.StartListening(EventName.PlayerEvent.LoseTurnBattle, OnLoseStreak);
        }

        private void OnEnable()
        {
            if (_isFirstUpdated is false)
                OnStartGame();
        }

        private void OnStartGame()
        {
            if (_isFirstUpdated is false)
                _isFirstUpdated = true;
            
            _playerID = string.Empty;
            gameObject.SetActive(default);
        }

        private bool IsLoseStreak()
        {
            var data = EventManager.GetData(EventName.PlayerEvent.LoseTurnBattle);
            if (data is null) return false;

            var playerLoseStreakInfo = (PlayerLoseStreakInfo) data;
            return playerLoseStreakInfo.Address == _playerID && playerLoseStreakInfo.LoseStreak > (int) default;
        }
        
        private void OnLoseStreak()
        {
            if(string.IsNullOrEmpty(_playerID))
                _playerID = StartGameServerService.Response.data.GetPlayerInfo(owner).id;

            gameObject.SetActive(IsLoseStreak());
        }
    }
}
