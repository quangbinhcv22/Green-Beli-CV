using System.Collections;
using System.Collections.Generic;
using GRBEGame.Resources;
using GRBEGame.UI.DataView;
using Network.Messages.GetHeroList;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlantHeroIcon : MonoBehaviour, IMemberView<HeroResponse>
{
    [SerializeField] private PlantHeroCellView owner;
    [SerializeField] private Image icon;

    private void Awake()
    {
        Assert.IsNotNull(icon);
        //owner.AddCallbackUpdate(this);
    }

    public void UpdateDefault()
    {
        
    }

    public void UpdateView(HeroResponse hero)
    {
        icon.sprite = GrbeGameResources.Instance.HeroIcon.GetIcon(hero.GetID().ToString());
    }
}
