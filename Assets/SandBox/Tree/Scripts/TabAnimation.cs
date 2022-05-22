using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabAnimation : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;

    [SerializeField] private TabTweenSetting normalConfig;
    [SerializeField] private TabTweenSetting hoverConfig;
    [SerializeField] private TabTweenSetting selectConfig;

    private List<string> currentTweenIds = new List<string>();

    public void OnNormal() => Tween(normalConfig);

    public void OnHover() => Tween(hoverConfig);

    public void OnSelect() => Tween(selectConfig);

    private void Tween(TabTweenSetting config)
    {
        KillLastTweens();

        var duration = config.duration;

        AddNewTween(button.transform.DOLocalMove(config.position, duration).SetEase(config.ease));
        AddNewTween(text.transform.DOScale(config.textSize, duration).SetEase(config.ease));
        AddNewTween(text.DOColor(config.textColor, duration).SetEase(config.ease));
    }

    private void AddNewTween(Tweener tweener)
    {
        tweener.id = Guid.NewGuid().ToString();
        currentTweenIds.Add(tweener.id.ToString());
    }

    private void KillLastTweens()
    {
        foreach (var currentTweenId in currentTweenIds)
        {
            if (string.IsNullOrEmpty(currentTweenId)) continue;

            DOTween.Kill(currentTweenId);
        }

        currentTweenIds.Clear();
    }
}