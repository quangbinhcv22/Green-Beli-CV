using UnityEngine;

namespace UI.ArtVisual
{
    [CreateAssetMenu(fileName = "ButtonArtSet", menuName = "ScriptableObjects/ArtSet/Button")]
    public class ButtonArtSet : UnityEngine.ScriptableObject
    {
        public Sprite normal;
        public Sprite cantInteract;
    }
}