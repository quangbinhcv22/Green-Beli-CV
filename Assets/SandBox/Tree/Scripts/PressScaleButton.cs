using System;
using DG.Tweening;
using GTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressScaleButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    [SerializeField] private Button button;
    [SerializeField] private ClickButtonConfig clickButtonConfig;
    
    private Transform buttonTransform;
    private readonly TweenSession _tweenSession = new TweenSession();

    private void Awake()
    {
        buttonTransform = button.transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsDisableButton) return;
        
        if (_tweenSession.IsLock()) return;
        
        Tweens(clickButtonConfig.press);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsDisableButton) return;
        
        Tweens(clickButtonConfig.normal);
    }
    
    private bool IsDisableButton => button.interactable == false;
   

    private void Tweens(ClickButtonSetting setting)
    {
        _tweenSession.Cancel();
    
        _tweenSession.Start(buttonTransform.DOScale(setting.scale, setting.duration)
            .SetEase(setting.ease));
    }
    
}


