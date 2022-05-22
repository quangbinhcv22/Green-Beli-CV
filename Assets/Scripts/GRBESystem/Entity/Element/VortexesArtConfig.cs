using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GRBESystem.Entity.Element
{
    [CreateAssetMenu(fileName = "VortexesArtConfig", menuName = "ScriptableObjects/Art/VortexesArtConfig")]
    public class VortexesArtConfig : ScriptableObject
    {
        [SerializeField] private List<VortexArtPair> vortexArtPairs;

        public Sprite GetVortexesSprite(HeroElement heroElement)
        {
            foreach (var vortexArtPair in vortexArtPairs.Where(vortexArtPair => vortexArtPair.element == heroElement))
            {
                return vortexArtPair.vortex;
            }

            throw new KeyNotFoundException();
        }
            
        [System.Serializable]
        public struct VortexArtPair
        {
            public HeroElement element;
            public Sprite vortex;
        }
    }
}
