using GRBEGame.UI.DataView;
using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class PlantHeroIdText : MonoBehaviour, IMemberView<HeroResponse>
{
    [SerializeField] private PlantHeroCellView owner;

    [SerializeField] private TMP_Text idText;
    [SerializeField] private string idFormat = "{0}";
    [SerializeField] private string stringDefault;

    private void Awake()
    {
        Assert.IsNotNull(idText);
        //owner.AddCallbackUpdate(this);
    }

    public void UpdateDefault()
    {
        idText.SetText(stringDefault);
    }

    public void UpdateView(HeroResponse hero)
    {
        idText.SetText(string.Format(idFormat, hero.GetID()));
        
    }
}
