using UnityEngine;

namespace UI.ArtVisual
{
    [CreateAssetMenu(fileName = "ResultFlagArtSet", menuName = "ScriptableObjects/ArtSet/ResultFlag")]
    public class ResultFlagArtSet : UnityEngine.ScriptableObject
    {
        public Sprite victory;
        public Sprite lose;
    }
}