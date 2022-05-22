using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace UX.Animation
{
    public class AppearingAndDisappearingScaler : MonoBehaviour, IAppearingAnimation, IDisappearingAnimation
    {
        [SerializeField] private ScaleConfig scaleConfig;
        public UnityAction onCompletedAnimation;

        [Serializable]
        private struct ScaleConfig
        {
            [Space] public Vector3 target;

            [Space] public float duration;
            public Ease ease;

            [Space] public bool isActiveOnEnable;
            public bool isActiveOnDisable;
        }

        private Vector3 _normalScale;


        private void Awake()
        {
            _normalScale = transform.localScale;
        }
        
        
        public void PerformingAppearingAnimation()
        {
            if (scaleConfig.isActiveOnEnable == false) return;
            transform.localScale = scaleConfig.target;
            transform.DOScale(_normalScale, scaleConfig.duration).SetEase(scaleConfig.ease);
            
            onCompletedAnimation?.Invoke();
        }

        public void PerformingDisappearingAnimation()
        {
            if (scaleConfig.isActiveOnDisable == false) return;
            transform.localScale = _normalScale;
            transform.DOScale(scaleConfig.target, scaleConfig.duration).SetEase(scaleConfig.ease);
            
            onCompletedAnimation?.Invoke();
        }
    }

    public interface IAppearingAnimation
    {
        public void PerformingAppearingAnimation();
    }

    public interface IDisappearingAnimation
    {
        public void PerformingDisappearingAnimation();
    }
}