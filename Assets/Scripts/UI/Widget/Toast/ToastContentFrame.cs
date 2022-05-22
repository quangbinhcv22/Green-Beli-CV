using System;
using DG.Tweening;
using GEvent;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace UI.Widget.Toast
{
    public class ToastContentFrame : MonoBehaviour
    {
        private static ToastData ToastData => EventManager.GetData<ToastData>(EventName.ScreenEvent.ShowToastPanel);


        [SerializeField] private Image background;
        [SerializeField] private TMP_Text contentText;

        [SerializeField] private ToastBackgroundArtSet toastBackgroundArtSet;

        [SerializeField, Space] private CanvasGroup canvasGroup;
        [SerializeField] private Ease ease = Ease.Linear;

        [SerializeField, Space] private Transform hidePoint;
        [SerializeField] private Transform showPoint;

        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            transform.position = _startPosition;

            SetBackgroundLevel(ToastData.toastLevel);
            SetContentText(ToastData.content);
        }

        public void MoveFade(ToastFade fade, float duration)
        {
            transform.DOMove(
                fade == ToastFade.FadeIn ? showPoint.position : hidePoint.position, duration).SetEase(ease);
            canvasGroup.DOFade(fade == ToastFade.FadeIn ? 1f : 0f, duration).SetEase(ease);
        }

        private void SetBackgroundLevel(ToastData.ToastLevel toastLevel)
        {
            var newBackgroundSprite = toastLevel switch
            {
                ToastData.ToastLevel.Safe => toastBackgroundArtSet.safe,
                ToastData.ToastLevel.Neutral => toastBackgroundArtSet.neutral,
                ToastData.ToastLevel.Danger => toastBackgroundArtSet.danger,
                _ => toastBackgroundArtSet.neutral
            };

            background.sprite = newBackgroundSprite;
        }

        private void SetContentText(string content)
        {
            contentText.SetText(content);
        }
    }

    [Serializable]
    public enum ToastFade
    {
        FadeIn = 0,
        FadeOut = 1,
    };
}
