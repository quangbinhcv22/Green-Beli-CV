using GRBEGame.Define;
using QB.Collection;
using UnityEngine;

namespace UI.ArtVisual
{
    [CreateAssetMenu(fileName = "MaterialArtSet", menuName = "ArtSet/Material")]
    public class MaterialArtSet : UnityEngine.ScriptableObject
    {
        [SerializeField] private DefaultableDictionary<MaterialType, Sprite> fragmentArtConfigs;

        public Sprite GetFragmentIcon(MaterialType type)
        {
            return fragmentArtConfigs[type];
        }
    }
}
