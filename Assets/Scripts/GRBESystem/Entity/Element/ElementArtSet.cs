using QB.Collection;
using QB.ViewData;
using UnityEngine;

namespace GRBESystem.Entity.Element
{
    [CreateAssetMenu(fileName = nameof(ElementArtSet), menuName = "ScriptableObject/Art/ElementArtSet")]
    public class ElementArtSet : ScriptableObject, IArtSet
    {
        [SerializeField] private DefaultableDictionary<HeroElement, Sprite> artSet;

        public Sprite GetSprite(object key)
        {
            return artSet[(HeroElement) key];
        }
    }
}