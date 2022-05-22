using System.Collections;
using DG.Tweening;
using GScreen;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCover : MonoBehaviour
{
    [SerializeField] private Image cover;
    [SerializeField] private float startFade = 0.45f;
    [SerializeField] private float endFade = 1f;
    [SerializeField] private Ease ease = Ease.OutQuad;

    private float _delay;
    private float _duration;


    private void Awake()
    {
        var loadingPanelReporter = LoadingPanelReporter.Instance;

        loadingPanelReporter.OnLoading += OnLoad;

        _delay = loadingPanelReporter.config.GetMainHallDelay();
        _duration = loadingPanelReporter.config.GetAnimationCoverDelay();
    }

    private void OnEnable()
    {
        cover.DOFade(default, default);
    }

    private void OnLoad(float loadingValue)
    {
        const int fullLoadingValue = 1;
        if (loadingValue is fullLoadingValue) StartCoroutine(OnLoaded());
    }

    private IEnumerator OnLoaded()
    {
        yield return new WaitForSeconds(_delay);

        cover.DOFade(startFade, default);
        cover.DOFade(endFade, _duration).SetEase(ease);
    }
}