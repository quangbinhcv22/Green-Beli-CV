using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Widgets.FieldTextViewer.BattleRound
{
    public class BattleTimeLineFieldTextsViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text phraseText;
        [SerializeField] private TMP_Text turnText;

        [SerializeField] private int unreasonableValue = 0;
        [SerializeField] private string prefixFormatPhaseText = "Phase ";
        [SerializeField] private string prefixFormatTurnText = "Turn ";


        private int _clientPhase;

        private int ClientPhase
        {
            get => _clientPhase;
            set
            {
                _clientPhase = value;
                phraseText.SetText(GetFormattedPhraseText(_clientPhase));
            }
        }

        private int _clientTurn;

        private int ClientRound
        {
            get => _clientTurn;
            set
            {
                _clientTurn = value;
                turnText.SetText(GetFormattedTurnText(_clientTurn));
            }
        }


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.StartGame, OnStartGame);
            EventManager.StartListening(EventName.Server.StartPhase, OnStartPhase);
            EventManager.StartListening(EventName.Server.StartRound, OnStartRound);
        }
        
        private void OnEnable()
        {
            phraseText.SetText(string.Empty);
            turnText.SetText(string.Empty);
        }

        private void OnStartGame()
        {
            ClientRound = 0;
        }

        private void OnStartPhase()
        {
            var startPhaseResponse = NetworkService.Instance.services.startPhase.Response;
            if (startPhaseResponse.IsError) return;
            
            ClientPhase = startPhaseResponse.data.phaseIndex;
        }

        private void OnStartRound()
        {
            ClientRound++;
        }

        
        private string GetFormattedPhraseText(int phrase)
        {
            return GetFormattedFairNumber(phrase, $"{prefixFormatPhaseText}{phrase}");
        }

        private string GetFormattedTurnText(int turn)
        {
            return GetFormattedFairNumber(turn, $"{prefixFormatTurnText}{turn}");
        }

        private string GetFormattedFairNumber(int number, string outputIfReasonable)
        {
            return number == unreasonableValue ? string.Empty : outputIfReasonable;
        }
    }
}