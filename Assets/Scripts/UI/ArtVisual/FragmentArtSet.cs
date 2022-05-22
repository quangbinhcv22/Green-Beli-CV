using GRBEGame.Define;
using QB.Collection;
using UnityEngine;


namespace UI.ArtVisual
{
    [CreateAssetMenu(fileName = "FragmentArtSet", menuName = "ArtSet/Fragment")]
    public class FragmentArtSet : UnityEngine.ScriptableObject
    {
        [SerializeField] private DefaultableDictionary<FragmentType, Sprite> fragmentArtConfigs;

        public Sprite GetFragmentIcon(FragmentType type)
        {
            return fragmentArtConfigs[type];
        }
    }
}
