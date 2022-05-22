using QB.Collection;
using UnityEngine;

namespace GRBEGame.ArtSet
{
    [CreateAssetMenu(fileName = nameof(BodyPartArtSet), menuName = "ScriptableObject/ArtSet/BodyPartArtSet")]
    public class BodyPartArtSet : ScriptableObject
    {
        [SerializeField] private DefaultableDictionary<string, Sprite> artSet;
        // public List<Sprite> drawArtSet;

        public Sprite GetArt(string partId)
        {
            return artSet[partId];
        }

        // private void OnValidate()
        // {
        //     artSet.customPairs = drawArtSet.Select(artSet => new QBStudio.Collection.KeyValuePair<string, Sprite>
        //         {key = artSet.name, value = artSet}).ToList();
        // }
    }
}