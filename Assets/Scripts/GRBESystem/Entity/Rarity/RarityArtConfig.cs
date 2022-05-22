using System.Collections.Generic;
using UnityEngine;

namespace GRBESystem.Entity.Rarity
{
    [CreateAssetMenu(fileName = "RarityArtConfig", menuName = "ScriptableObjects/Art/RarityArtConfig")]
    public class RarityArtConfig : ScriptableObject
    {
        [SerializeField] private List<RarityArtPair> rarityBackgroundArtPairs;
        
        public RarityArtPair GetRarityArtPair(int rarity)
        {
            foreach (var rarityBackgroundArtPair in rarityBackgroundArtPairs)
            {
                if(rarityBackgroundArtPair.rarity == rarity)
                    return rarityBackgroundArtPair;
            }

            throw new KeyNotFoundException();
        }

        [System.Serializable]
        public struct RarityArtPair
        {
            public int rarity;
            public Sprite mainBackground;
            public Sprite levelBackground;
        }
    }
}
