using UnityEngine;

namespace QB.Wallpaper
{
    [DefaultExecutionOrder(50)]
    [RequireComponent(typeof(RectTransform))]
    public class WallpaperUser : MonoBehaviour
    {
        public WallpaperPreset preset;
        public RectTransform RectTransform { get; private set; }


        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            WallpaperPool.Instance.Use(this);
        }

        private void OnDisable()
        {
            WallpaperPool.Instance.Recall(this);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (preset is null) Debug.LogError($"I'm <color=yellow>{name}</color>, <color=yellow>Wallpaper Preset</color> is <color=yellow>null</color>!", gameObject);
        }
#endif
    }
}