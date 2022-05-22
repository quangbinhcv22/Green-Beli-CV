using System;
using DG.Tweening;
using GCamera;
using GEvent;
using TigerForge;
using UIFlow;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(100)]
public class TweenCameraFirstLogin : MonoBehaviour
{
    [SerializeField] private UIObject mainHallWindow;

    [SerializeField] private float startLenSize = 6f;
    [SerializeField] private float endLenSize = 5f;
    [SerializeField] private float duration = 0.85f;
    [SerializeField] private Ease ease = Ease.InQuad;
    [SerializeField] private ConfigCamera cameraConfig;

    private Camera _camera;
    private bool _haveTween;

    private string _currentTween;
    private void ResetHaveTween() => _haveTween = false;

    
    public UnityEvent onCompleted; 


    private void Awake()
    {
        _camera = Camera.main;

        mainHallWindow.onOpening.AddListener(Tween);
        EventManager.StartListening(EventName.Server.LoginByMetamask, ResetHaveTween);
    }

    private void OnDisable()
    {
        KillTween();
    }

    private void Tween()
    {
        if (_haveTween) return;

        _haveTween = true;

        _camera.orthographicSize = startLenSize;

        var tweener = _camera.DOOrthoSize(endLenSize, duration).SetEase(ease).OnComplete(() => onCompleted?.Invoke());
        _currentTween = (tweener.id = Guid.NewGuid().ToString()).ToString();
    }

    private void KillTween()
    {
        if (string.IsNullOrEmpty(_currentTween) is false) DOTween.Kill(_currentTween);
        if (_camera) cameraConfig.ResetConfig();
    }
}