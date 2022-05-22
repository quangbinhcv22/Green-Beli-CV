using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GEvent;
using GTween;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreOpenTreePresenter : MonoBehaviour
{
    [SerializeField] private GameObject decor;
    [SerializeField] private Image decorImage;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private List<Image> images;
    [SerializeField] private List<TMP_Text> texts;
    [SerializeField] private Button open;
    [SerializeField] private FadeConfig fadeConfig;
    [SerializeField] private float showDuration;

    private readonly TweenSession _tweenSession = new TweenSession();
    private void Awake()
    {
        open.onClick.AddListener(StartShow);
        EventManager.StartListening(EventName.Server.HaveNewTree, StartShow);
    }

    private void OnEnable()=> StartShow();

    private void StartShow()
    {
        StartCoroutine(ShowDecor());
    }

    private IEnumerator ShowDecor()
    {
        decor.SetActive(true);
        infoPanel.SetActive(false);
        
        TweenCancel();

        _tweenSession.Start(
            decorImage.DOFade(fadeConfig.normal.visible, showDuration).SetEase(fadeConfig.normal.ease),
            decorImage.transform.DOScale(fadeConfig.scale.scale, fadeConfig.scale.duration).SetEase(fadeConfig.scale.ease)
        );
        FadeOut(fadeConfig.invincible);
        yield return new WaitForSeconds(showDuration);
        infoPanel.SetActive(true);
        _tweenSession.Start(
            decorImage.DOFade(fadeConfig.invincible.visible,  fadeConfig.invincible.duration).SetEase(fadeConfig.invincible.ease),
            decorImage.transform.DOScale(fadeConfig.invincible.scale, fadeConfig.invincible.duration).SetEase(fadeConfig.invincible.ease)
        );
        FadeIn(fadeConfig.normal);
        decor.SetActive(false);
    }

    private void FadeIn(FadeSetting setting)
    {
        foreach (var image in images)
            _tweenSession.Start(image.DOFade(setting.visible, setting.duration).SetEase(setting.ease));
        
        foreach (var text in texts)
            _tweenSession.Start(text.DOFade(setting.visible, setting.duration).SetEase(setting.ease));
    }
    
    private void FadeOut(FadeSetting setting)
    {
        foreach (var image in images)
            _tweenSession.Start(image.DOFade(setting.visible, setting.duration).SetEase(setting.ease));
        foreach (var text in texts)
            _tweenSession.Start(text.DOFade(setting.visible, setting.duration).SetEase(setting.ease));
    }
    
    private void TweenCancel()
    {
        _tweenSession.Cancel();
    }

    private void OnValidate()
    {
        if (decor is null)
            Debug.Log($"I'm <color=yellow>{name}</color>.<color=yellow>Decor</color> is <color=yellow>null</color>");
        if (infoPanel is null)
            Debug.Log(
                $"I'm <color=yellow>{name}</color>.<color=yellow>InfoPanel</color> is <color=yellow>null</color>");
        if (open is null)
            Debug.Log($"I'm <color=yellow>{name}</color>.<color=yellow>Open</color> is <color=yellow>null</color>");
    }
}