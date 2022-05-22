using GEvent;
using GRBEGame.UI.DataView;
using Network.Messages.GetHeroList;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;

public class SelectedPlantHeroHighLight : MonoBehaviour, IMemberView<HeroResponse>
{
    [SerializeField] private HeroVisual owner;
    [SerializeField] private GameObject highlight;

    private void Awake()
    {
        highlight.SetActive(false);
        owner.AddCallBackUpdateView(this);
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventName.Select.PlantHero, OnSelectedTree);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Select.PlantHero, OnSelectedTree);
    }

    private void OnSelectedTree()
    {
        var nullablePlantHero = EventManager.GetData(EventName.Select.PlantHero);

        if (nullablePlantHero is long heroID)
        {
            var isSelected = heroID == owner.Hero.GetID();
            highlight.SetActive(isSelected);
        }
    }

    public void UpdateDefault()
    {
    }

    public void UpdateView(HeroResponse data)
    {
        OnSelectedTree();
    }
}
