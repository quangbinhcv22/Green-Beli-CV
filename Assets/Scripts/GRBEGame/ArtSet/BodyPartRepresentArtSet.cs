using GRBESystem.Definitions.BodyPart.Index;
using QB.Collection;
using UnityEngine;

namespace GRBEGame.ArtSet
{
    [CreateAssetMenu(fileName = nameof(BodyPartRepresentArtSet), menuName = "ScriptableObject/ArtSet/BodyPartRepresent")]
    public class BodyPartRepresentArtSet : ScriptableObject
    {
        [SerializeField] private DefaultableDictionary<BodyPartIndex, Sprite> artConfig;

        public Sprite GetArt(BodyPartIndex bodyPartIndex)
        {
            return artConfig[bodyPartIndex];
        }
    }
}