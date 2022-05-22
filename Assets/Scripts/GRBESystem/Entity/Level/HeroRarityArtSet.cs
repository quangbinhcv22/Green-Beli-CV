using System.Collections.Generic;
using UnityEngine;

namespace Config.ArtSet
{
    [CreateAssetMenu(fileName = "HeroRarityArtSet", menuName = "ScriptableObjects/ArtSet/HeroRarity")]
    public class HeroRarityArtSet : ScriptableObject
    {
        [SerializeField] private List<HeroRarityArtPair> levelBackgroundArtPairs;
        
        public Sprite GetRaritySprite(int rarity)
        {
            return levelBackgroundArtPairs.Find(pair => pair.rarity == rarity).sprite;
        }
        
        [System.Serializable]
        public struct HeroRarityArtPair
        {
            public int rarity;
            public Sprite sprite;
        }
    }
}
