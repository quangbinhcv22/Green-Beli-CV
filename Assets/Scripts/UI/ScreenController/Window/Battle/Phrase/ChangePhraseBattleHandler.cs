using GEvent;
using Network.Messages.StartPhase;
using TigerForge;
using UI.ScreenController.Window.Battle.Phrase.MotionText;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Phrase
{
    public class ChangePhraseBattleHandler : MonoBehaviour
    {
        [SerializeField] private MotionText.MotionText phraseTitleMotionText;
        [SerializeField] private MotionText.MotionText buffInfoMotionText;

        private bool _haveRequestPerformMotion;

        private void Awake()
        {
            EventManager.StartListening(EventName.Server.StartPhase, OnChangePhrase);
        }

        private void OnEnable()
        {
            phraseTitleMotionText.ReStartPosition();
            buffInfoMotionText.ReStartPosition();
            
            if(_haveRequestPerformMotion) PerformMotion();
        }

        private void OnChangePhrase()
        {
            if (gameObject.activeInHierarchy) PerformMotion();
            else _haveRequestPerformMotion = true;
        }

        private void PerformMotion()
        {
            _haveRequestPerformMotion = false;
            
            var startPhaseResponse = EventManager.GetData<StartPhaseResponse>(EventName.Server.StartPhase);
            var phraseBattleData = PhraseBattleData.ConvertFromResponse(startPhaseResponse);

            var titlePhrase = PhraseBattleData.GetFormattedPhraseNumber(phraseBattleData.phraseNumber);
            var buffInfo = PhraseBattleData.GetFormattedDamagePercentage(phraseBattleData.damagePercentage);

            phraseTitleMotionText.StartMotion(new MotionTextData(titlePhrase, Color.cyan));
            buffInfoMotionText.StartMotion(new MotionTextData(buffInfo, Color.cyan));
        }
    }
}