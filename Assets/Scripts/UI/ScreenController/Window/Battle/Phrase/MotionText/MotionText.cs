using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GEvent;
using TigerForge;
using TMPro;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Phrase.MotionText
{
    public class MotionText : MonoBehaviour
    {
        [SerializeField] private TMP_Text motionText;
        [SerializeField] private List<MotionTextConfig> motionConfigs;

        public void StartMotion(MotionTextData motionTextData)
        {
            ChangeTextInterface(motionTextData);
            StartCoroutine(MoveMotion());
            StartCoroutine(PlaySoundEffect());
        }

        public void ReStartPosition()
        {
            if (motionConfigs[0] == null) return;
            motionText.transform.position = this.motionConfigs[0].motionPositionTransformStart.position;
        }

        private void ChangeTextInterface(MotionTextData motionTextData)
        {
            this.motionText.text = motionTextData.titleContent;
            this.motionText.color = motionTextData.colorText;
        }

        private IEnumerator MoveMotion()
        {
            foreach (var motionConfig in motionConfigs)
            {
                yield return new WaitForSeconds(motionConfig.delayMotion);

                this.motionText.transform.position = motionConfig.motionPositionTransformStart.position;
                var targetPosition = motionConfig.motionPositionTransformTarget.position;

                motionText.transform.DOMove(targetPosition, motionConfig.durationMotion)
                    .SetEase(motionConfig.easeMotion);
            }
        }

        private IEnumerator PlaySoundEffect()
        {
            foreach (var motionConfig in motionConfigs)
            {
                yield return new WaitForSeconds(motionConfig.delayMotion);

                if (motionConfig.soundEffectWhenMotion == null) continue;

                yield return new WaitForSeconds(motionConfig.delayPlaySoundEffect);
                EventManager.EmitEventData(EventName.WidgetEvent.PLAY_SOUND_EFFECT, data: motionConfig.soundEffectWhenMotion);
            }
        }
    }
}