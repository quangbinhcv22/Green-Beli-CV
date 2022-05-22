using System.Collections.Generic;
using Service.Server.EndGame;
using UnityEngine;

namespace UI.ScreenController.Popup.EndGame.Widget.SoundEffect
{
    [CreateAssetMenu(fileName = "BattleResultSoundEffectSet", menuName = "ScriptableObjects/SoundSet/BattleResultSound")]
    public class BattleResultSoundEffectSet : UnityEngine.ScriptableObject
    {
        [System.Serializable]
        private struct ResultBattleSoundPair
        {
            public EndGameClientData.ResultBattle result;
            public AudioClip sound;
        }

        [SerializeField] private List<ResultBattleSoundPair> resultBattleSoundPairs;

        public AudioClip GetSound(EndGameClientData.ResultBattle result)
        {
            return resultBattleSoundPairs.Find(pair => pair.result == result).sound;
        }
    }
}