using UnityEngine;

namespace UI.Widget.Toast
{
    [CreateAssetMenu(fileName = "ToastBackgroundArtSet", menuName = "ScriptableObjects/Screen/Toast/BackgroundArtSet")]
    public class ToastBackgroundArtSet : UnityEngine.ScriptableObject
    {
        public Sprite safe;
        public Sprite neutral;
        public Sprite danger;
    }
}