using System.Collections;
using System.Collections.Generic;
using GRBEGame.Resources;
using GRBEGame.UI.DataView;
using GRBESystem.Entity.Element;
using Network.Messages.GetHeroList;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlantHeroElement : MonoBehaviour, IMemberView<HeroResponse>
{
    [SerializeField] private PlantHeroCellView owner;
    [SerializeField] private ElementArtSet elementArtSet;
    [SerializeField] private Image element;
    

    private void Awake()
    {
        Assert.IsNotNull(element);
        //owner.AddCallbackUpdate(this);
    }

    public void UpdateDefault()
    {
        element.sprite = elementArtSet.GetSprite(Element.Metal);
    }

    public void UpdateView(HeroResponse hero)
    {
        element.sprite = elementArtSet.GetSprite(hero.GetElement());
    }
}
