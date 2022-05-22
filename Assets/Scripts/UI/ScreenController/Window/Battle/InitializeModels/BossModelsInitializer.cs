using GEvent;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.InitializeModels
{
    public class BossModelsInitializer : MonoBehaviour
    {
        [SerializeField] private float delayTimer;

        private bool _isEndGame;
        private bool _isFirstStartGameLoaded;
        private bool _isFirstEndGameLoaded;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.StartGame, OnStartGame);
            EventManager.StartListening(EventName.Server.EndGame, OnEndGame);
        }

        private void OnEnable()
        {
            if (StartGameServerService.Response.IsError is false && _isFirstStartGameLoaded is false)
                OnStartGame();
            if(EndGameServerService.Response.IsError is false && _isFirstEndGameLoaded is false)
                OnEndGame();
        }

        private void OnStartGame()
        {
            _isEndGame = false;
            _isFirstStartGameLoaded = true;
            if(StartGameServerService.Response.IsError)
                return;
            
            Invoke(nameof(InitializeModels), delayTimer);
        }

        private void OnEndGame()
        {
            _isEndGame = true;
            _isFirstEndGameLoaded = true;
            
            Invoke(nameof(RecallModels), delayTimer);
        }

        private void InitializeModels()
        {
            if(_isEndGame)
                return;
            
            var battleData = StartGameServerService.BattleClientData;
            EventManager.EmitEventData(EventName.Model.ShowBossModel, battleData.boss.faction);
        }

        void RecallModels()
        {
            EventManager.EmitEvent(EventName.Model.HideAllModels);
        }
    }
}