using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace GTween
{
    [SerializeField]
    public class TweenSession
    {
        private readonly List<string> _currentTweens = new List<string>();

        private float _lockSeconds;
        private DateTime _lastLock;

        public bool IsLock() => (DateTime.Now - _lastLock).Seconds < _lockSeconds;

        public void Start(params Tweener[] tweeners)
        {
            foreach (var tweener in tweeners)
            {
                _currentTweens.Add((tweener.id = Guid.NewGuid().ToString()).ToString());
            }
        }

        public void Cancel()
        {
            _currentTweens.ForEach(tween => DOTween.Kill(tween));
            _currentTweens.Clear();
        }

        public void Lock(float seconds = 0.5f)
        {
            _lockSeconds = seconds;
            _lastLock = DateTime.Now;
        }
    } 
}

