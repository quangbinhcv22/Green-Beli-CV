using DG.Tweening;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Phrase.MotionText
{
    [System.Serializable]
    public class MotionTextConfig
    {
        [Space] public Transform motionPositionTransformStart;
        public Transform motionPositionTransformTarget;

        [Space] public float durationMotion;
        public float delayMotion;
        public Ease easeMotion;

        [Space] public AudioClip soundEffectWhenMotion;
        public float delayPlaySoundEffect;
    }
}