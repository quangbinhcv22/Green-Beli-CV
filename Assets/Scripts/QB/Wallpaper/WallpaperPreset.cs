using UnityEngine;

namespace QB.Wallpaper
{
    [CreateAssetMenu(menuName = "Wallpaper/Preset", fileName = nameof(WallpaperPreset))]
    public class WallpaperPreset : ScriptableObject
    {
        public Sprite sprite;
        public Color color = new Color(0, 0, 0, 0.5f);
    }
}