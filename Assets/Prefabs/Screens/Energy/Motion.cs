using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Motion : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Ease easeType;
    [SerializeField] private float duration;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    
    
    public void OnlyOpen()
    {
        
    }

    public void Open()
    {
        _canvasGroup.DOFade(minScale, duration).SetEase(easeType);
        _canvasGroup.transform.DOScale(minScale, duration).SetEase(easeType);
    }

    public void Close()
    {
        _canvasGroup.DOFade(maxScale, duration).SetEase(easeType);
        _canvasGroup.transform.DOScale(maxScale, duration).SetEase(easeType);
    }
}
