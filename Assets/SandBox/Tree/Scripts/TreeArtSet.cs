using System;
using System.Collections.Generic;
using System.Linq;
using GRBEGame.Resources;
using QB.Collection;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(TreeArtSet), menuName = "ScriptableObject/Art/TreeArtSet")]
public class TreeArtSet : ScriptableObject
{
        
    [SerializeField] private DefaultableDictionary<Element, Sprite> artSet;

    public Sprite GetSprite(Element element)
    {
        return artSet[element];
    }


    [System.Serializable] [Obsolete]
    public struct TreeArtPair
    {
        public Element element;
        public Sprite sprite;
    }
}
