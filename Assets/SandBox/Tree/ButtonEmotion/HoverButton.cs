using DG.Tweening;
using GTween;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public HoverButtonConfig hoverButtonConfig;
    public Button button;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TextConfig textConfig;
    
    private readonly TweenSession _tweenSession = new TweenSession();
    private float _startPosition;

    
    private void Awake()
    {
        SetDefaultPosition();
        button.onClick.AddListener(ResetDefaultPosition);
    }

    private void ResetDefaultPosition()
    {
        _tweenSession.Cancel();

        _tweenSession.Start(button.transform.DOLocalMoveY(_startPosition, hoverButtonConfig.normal.duration)
            .SetEase(hoverButtonConfig.normal.ease));
    }

    private void SetDefaultPosition()
    {
        _startPosition = button.transform.localPosition.y;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsDisableButton) return;
        
        if (_tweenSession.IsLock()) return;

        _tweenSession.Cancel();

        _tweenSession.Start(button.transform
            .DOLocalMoveY(_startPosition + hoverButtonConfig.moveY.up, hoverButtonConfig.moveY.duration)
            .SetEase(hoverButtonConfig.moveY.ease),
            text.DOColor(textConfig.hover.color, textConfig.hover.duration).SetEase(textConfig.hover.ease)
        );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsDisableButton) return;

        _tweenSession.Cancel();

        _tweenSession.Start(
            button.transform.DOLocalMoveY(_startPosition, hoverButtonConfig.moveY.duration)
                .SetEase(hoverButtonConfig.moveY.ease),
            text.DOColor(textConfig.normal.color, textConfig.normal.duration).SetEase(textConfig.normal.ease)
        );
    }

    private bool IsDisableButton => button.interactable == false;

    private void OnValidate()
    {
        Assert.IsNotNull(hoverButtonConfig);
        Assert.IsNotNull(button);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _tweenSession.Lock();
        OnPointerExit(eventData);
    }
}

