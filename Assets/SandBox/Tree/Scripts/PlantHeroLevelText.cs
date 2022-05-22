using System.Collections;
using System.Collections.Generic;
using GRBEGame.UI.DataView;
using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class PlantHeroLevelText : MonoBehaviour, IMemberView<HeroResponse>
{
    [SerializeField] private PlantHeroCellView owner;

    [SerializeField] private TMP_Text levelText;
    [SerializeField] private string idFormat = "{0}";
    [SerializeField] private string stringDefault;

    private void Awake()
    {
        Assert.IsNotNull(levelText);
        //owner.AddCallbackUpdate(this);
    }

    public void UpdateDefault()
    {
        levelText.SetText(stringDefault);
    }

    public void UpdateView(HeroResponse hero)
    {
        levelText.SetText(string.Format(idFormat, hero.level));
        
    }
}
