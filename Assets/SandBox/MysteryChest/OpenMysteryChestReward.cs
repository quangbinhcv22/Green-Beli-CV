using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GTween;
using UnityEngine;
using UnityEngine.UI;

public class OpenMysteryChestReward : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fade;
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;

    private readonly TweenSession _tweenSession = new TweenSession();
    private float _startPosition;

    private void OnEnable()
    {
        StartCoroutine(OpenReward());
    }

    private IEnumerator OpenReward()
    {
        _tweenSession.Start(
            image.DOFade(fade, 0.01f).SetEase(ease)
        );
        yield return new WaitForSeconds(duration);
        _tweenSession.Cancel();
        
        _tweenSession.Start(
            image.DOFade(0, fadeDuration).SetEase(ease)
        );
    }
}
