using QB.Collection;
using QB.ViewData;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(TreeDeactivateButtonArtSet), menuName = "ScriptableObject/Art/TreeDeactivateArtSet")]
public class TreeDeactivateButtonArtSet : ScriptableObject,IArtSet
{
    [SerializeField] private DefaultableDictionary<TreeStatus, Sprite> artSet;

    public Sprite GetSprite(object key)
    {
        return artSet[(TreeStatus) key];
    }
}
