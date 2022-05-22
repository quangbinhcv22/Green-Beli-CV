using DG.Tweening;
using UnityEngine;

namespace GTween
{
    [CreateAssetMenu(menuName = "Tween/MainBuilding", fileName = nameof(MainBuildingMouseEffectConfig))]
    public class MainBuildingMouseEffectConfig : ScriptableObject
    {
        [Header("General")] public float duration;
        public Ease ease;

        [Header("Glow"), Space, Range(0, 1)] public float glowFadeNormal;
        [Range(0, 1)] public float glowFadeHover;

        [Header("Text"), Space] public Color textColorNormal;
        public Color textColorHover;
        public Vector3 textScaleNormal;
        public Vector3 textScaleHover;
    }
}