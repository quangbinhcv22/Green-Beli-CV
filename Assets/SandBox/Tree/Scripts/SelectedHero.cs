using System.Collections;
using System.Collections.Generic;
using GEvent;
using Network.Messages.GetHeroList;
using TigerForge;
using UnityEngine;

public class SelectedHero : MonoBehaviour
{
    [SerializeField] private PlantHeroCellView plantHeroCellView;

    private void OnEnable()
    {
        EventManager.StartListening(EventName.Select.PlantHero, OnSelectTree);
        OnSelectTree();
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Select.PlantHero, OnSelectTree);
    }

    private void OnSelectTree()
    {
        var nullableHeroId = EventManager.GetData(EventName.Select.PlantHero);
        
        if (nullableHeroId is long heroId) plantHeroCellView.Setup(heroId);
    }
}
