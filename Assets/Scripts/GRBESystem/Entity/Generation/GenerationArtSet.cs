using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace GRBESystem.Entity.Generation
{
    [CreateAssetMenu(fileName = "GenerationArtConfig", menuName = "ScriptableObjects/ArtSet/GenerationArtConfig")]
    public class GenerationArtSet : ScriptableObject
    {
        [SerializeField] private List<GenerationArtPair> generationArtPairs;


        public Sprite GetGenerationSprite(int heroRarity)
        {
            foreach (var generationArtPair in generationArtPairs.Where(
                generationArtPair => generationArtPair.heroRarity == heroRarity))
            {
                return generationArtPair.backgroundGeneration;
            }
            throw new KeyNotFoundException();
        }
    }
    
    [System.Serializable]
    public struct GenerationArtPair
    {
        public int heroRarity;
        public Sprite backgroundGeneration;
    }
}