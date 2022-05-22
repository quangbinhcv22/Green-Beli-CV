using GRBEGame.Resources;
using QB.Collection;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ElementLocallizeSet), menuName = "ScriptableObject/Art/ElementLocallizeSet")]

public class ElementLocallizeSet : ScriptableObject,ILocallizeSet
{
    [SerializeField] private DefaultableDictionary<Element, string> artSet;

    public string GetDetail(object key)
    {
        return artSet[(Element) key];
    }
}
