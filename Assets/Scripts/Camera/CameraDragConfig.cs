using DG.Tweening;
using UnityEngine;

namespace GCamera
{
    [CreateAssetMenu(menuName = "Preset/CameraDragConfig", fileName = nameof(CameraDragConfig))]
    public class CameraDragConfig : ScriptableObject
    {
        public float speed = 0.5f;
        public float boundX = 0.7f;
        public float boundY = 0.4f;
        public float duration = 1f;
        public Ease ease = Ease.InOutQuint;
    }
}