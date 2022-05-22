using DG.Tweening;
using GTween;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private TextConfig textConfig;

    private readonly TweenSession _tweenSession = new TweenSession();

    private void Awake()
    {
        text.raycastTarget = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tweenSession.Cancel();

        _tweenSession.Start(
            text.DOColor(textConfig.hover.color, textConfig.hover.duration).SetEase(textConfig.hover.ease)
        );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tweenSession.Cancel();

        _tweenSession.Start(
            text.DOColor(textConfig.normal.color, textConfig.normal.duration).SetEase(textConfig.normal.ease)
        );
    }

    private void OnValidate()
    {
        Assert.IsNotNull(text);
    }
}