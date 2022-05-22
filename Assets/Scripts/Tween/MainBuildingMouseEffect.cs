using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace GTween
{
    public class MainBuildingMouseEffect : MonoBehaviour
    {
        [SerializeField] SpriteRenderer glowRenderer;
        //[SerializeField] SpriteRenderer textRenderer;
        [SerializeField] private MainBuildingMouseEffectConfig config;

        private List<string> _tweenMouseEnterIds = new List<string>();

        private void OnEnable()
        {
            OnMouseExit();
        }

        private void KillTweenMouseEnter() => _tweenMouseEnterIds.ForEach(id => DOTween.Kill(id));


        private void OnMouseEnter()
        {
            var tweeners = new List<Tweener>()
            {
                glowRenderer.DOFade(config.glowFadeHover, config.duration).SetEase(config.ease),
                // textRenderer.DOColor(config.textColorHover, config.duration).SetEase(config.ease),
                // textRenderer.transform.DOScale(config.textScaleHover, config.duration).SetEase(config.ease),
            };

            tweeners.ForEach(tween => tween.id = Guid.NewGuid().ToString());
            _tweenMouseEnterIds = tweeners.Select(tween => tween.id.ToString()).ToList();
        }

        private void OnMouseExit()
        {
            KillTweenMouseEnter();

            glowRenderer.DOFade(config.glowFadeNormal, config.duration).SetEase(config.ease);
            // textRenderer.DOColor(config.textColorNormal, config.duration).SetEase(config.ease);
            // textRenderer.transform.DOScale(config.textScaleNormal, config.duration).SetEase(config.ease);
        }
    }
}