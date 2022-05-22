using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace UIFlow
{
    [RequireComponent(typeof(Animator))]
    public class UIAnimator : MonoBehaviour
    {
        [SerializeField] private UIAnimatorConfig config;
        public UIAnimatorConfig Config => config;

        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnlyOpen()
        {
            _animator.SetTrigger(config.animationName.@default);
        }

        public void Open()
        {
            _animator.SetTrigger(config.animationName.open);
        }

        public void Close()
        {
            _animator.SetTrigger(config.animationName.close);
        }

        private void OnValidate()
        {
            Assert.IsNotNull(config);
        }
    }
}