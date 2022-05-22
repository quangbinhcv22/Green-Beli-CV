using GRBEGame.UI.Screen.Inventory;
using QB.Collection;
using UnityEngine;

namespace UI.ArtVisual
{
    [CreateAssetMenu(fileName = "BoxItemTypeArtSet", menuName = "ScriptableObjects/ArtSet/BoxItemType")]
    public class BoxItemTypeArtSet : UnityEngine.ScriptableObject
    {
        [SerializeField] private DefaultableDictionary<BoxItemType, Sprite> boxItemTypeArtConfigs;

        public Sprite GetIcon(BoxItemType type)
        {
            return boxItemTypeArtConfigs[type];
        }
    }
}
