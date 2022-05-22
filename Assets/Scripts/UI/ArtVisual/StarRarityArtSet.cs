using QB.Collection;
using UnityEngine;


namespace UI.ArtVisual
{
    [CreateAssetMenu(fileName = "StarRarityArtSet", menuName = "ScriptableObjects/ArtSet/StarRarity")]
    public class StarRarityArtSet : UnityEngine.ScriptableObject
    {
        [SerializeField] private DefaultableDictionary<int, Sprite> artConfigs;

        public Sprite GetIcon(int rarity)
        {
            return artConfigs[rarity];
        }
    }
}
