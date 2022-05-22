using QB.Collection;
using UnityEngine;

namespace UI.ArtVisual
{
    [CreateAssetMenu(fileName = "ItemInventoryBackgroundArtSet", menuName = "ScriptableObjects/ArtSet/ItemInventoryBackground")]
    public class ItemInventoryBackgroundArtSet : UnityEngine.ScriptableObject
    {
        [SerializeField] private DefaultableDictionary<int, Sprite> backgroundArtConfigs;
        
        public Sprite GetBackground(int rarity)
        {
            return backgroundArtConfigs[rarity];
        }
    }
}
