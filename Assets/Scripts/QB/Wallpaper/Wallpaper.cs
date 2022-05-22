using UnityEngine;
using UnityEngine.UI;

namespace QB.Wallpaper
{
    [DefaultExecutionOrder(200)]
    public class Wallpaper : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private RectTransform rectTransform;


        public void SetSkin(WallpaperPreset preset)
        {
            image.sprite = preset.sprite;
            image.color = preset.color;
        }

        public void SetParent(RectTransform parent)
        {
            rectTransform.SetParent(parent);
            rectTransform.SetSiblingIndex(default);

            rectTransform.localPosition = Vector3.zero;

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            transform.localScale = Vector2.one;
        }

        private void OnEnable()
        {
            image.enabled = true;
        }
    }
}