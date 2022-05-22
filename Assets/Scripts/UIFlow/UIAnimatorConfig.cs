using System;
using UnityEngine;

namespace UIFlow
{
    [CreateAssetMenu(menuName = "UIFlow/UIAnimatorConfig", fileName = nameof(UIAnimatorConfig))]
    public class UIAnimatorConfig : ScriptableObject
    {
        [Serializable]
        public class AnimationName
        {
            public string @default;
            public string open;
            public string close;
        }

        public AnimationName animationName;
        public float delayClose;
    }
}