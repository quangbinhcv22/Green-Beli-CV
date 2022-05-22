using GEvent;
using TigerForge;
using UIFlow;
using UnityEngine;

namespace SandBox.Battle
{
    public class MatchPvpScreenHandler : MonoBehaviour
    {
        [SerializeField] private float delayHideWhenOpinionDisconnect = 1f;
        [SerializeField] private float delayHideWhenStartGame = 2f;

        private bool _isStartGame;
        private readonly UIRequest _request = new UIRequest()
        {
            action = UIAction.Close,
            haveAnimation = default,
            id = UIId.MatchingPvpWindow,
        };


        private void OnEnable()
        {
            EventManager.StartListening(EventName.Server.StartGame, OnStartGame);
            EventManager.StartListening(EventName.Server.EndGame, OnEndGame);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.StartGame, OnStartGame);
            EventManager.StopListening(EventName.Server.EndGame, OnEndGame);
        }

        private void OnStartGame()
        {
            _isStartGame = true;
            Invoke(nameof(UIRequest), delayHideWhenStartGame);
        }

        private void OnEndGame()
        {
            if (_isStartGame is false)
                Invoke(nameof(UIRequest), delayHideWhenOpinionDisconnect);
        }

        private void UIRequest()
        {
            if (_isStartGame)
                _isStartGame = default;
            _request.SendRequest();
        }
    }
}
