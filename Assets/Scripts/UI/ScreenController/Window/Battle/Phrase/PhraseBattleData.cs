using Network.Messages.StartPhase;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Phrase
{
    [System.Serializable]
    public class PhraseBattleData
    {
        public int phraseNumber;
        public float damagePercentage;

        public PhraseBattleData(int phraseNumber, float damagePercentage)
        {
            this.phraseNumber = phraseNumber;
            this.damagePercentage = damagePercentage;
        }

        public static string GetFormattedPhraseNumber(int phraseNumber)
        {
            return $"Phase {phraseNumber}";
        }

        public static string GetFormattedDamagePercentage(float damagePercentage)
        {
            //return $"{(damagePercentage >= 0 ? "+" : "-")} {Mathf.Abs(damagePercentage * 100):N0}% DMG";
            return $"{(damagePercentage + 1) * 100}% Damage";
        }


        public static PhraseBattleData ConvertFromResponse(StartPhaseResponse startPhaseResponse)
        {
            // client: -0.5, 0, +0.5, server: 0.5, 1, 1.5 => index -1 : convert form server to client
            return new PhraseBattleData(startPhaseResponse.phaseIndex, startPhaseResponse.damagePercent - 1);
        }
    }
}