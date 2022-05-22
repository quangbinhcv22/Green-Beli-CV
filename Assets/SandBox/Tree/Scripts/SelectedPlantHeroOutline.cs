using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using GNetwork;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;

public class SelectedPlantHeroOutline : MonoBehaviour
{
    [SerializeField] private HeroVisual coreView;
    
    //public static List<long> HeroList = new List<long>();
    private List<HeroResponse> SelectedHeroes = new List<HeroResponse>();
    //public static List<HeroResponse> localHeroList = new List<HeroResponse>();
    
    [SerializeField] private int maxCount = 3;
    
    private void OnEnable()
    {
        EventManager.StartListening(EventName.Select.PlantHero, OnSelectedHero);
        OnSelectedHero();
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Select.PlantHero, OnSelectedHero);
    }

    private void OnSelectedHero()
    {
        var nullableSelectedHero = EventManager.GetData(EventName.Select.PlantHero);

        if (nullableSelectedHero is HeroResponse selectedHero)
        {
            //plantHeroCellView.Setup(selectedHero);
            // if (SelectedHeroes.Count < maxCount)
            // {
            //     SelectedHeroes.Add(selectedHero);
            //     
            //     EventManager.EmitEventData(EventName.Select.PlantHeroes,SelectedHeroes);
            // }
        }
    }
}