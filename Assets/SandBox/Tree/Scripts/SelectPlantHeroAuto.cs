using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;

public class SelectPlantHeroAuto : MonoBehaviour
{
    private void Awake()
    {
        FirstTreeSelect();
        EventManager.StartListening(EventName.Server.GetListHero,FirstTreeSelect);
    }

    private void FirstTreeSelect()
    {
        if (NetworkService.Instance.services.getHeroList.HeroResponses is null || NetworkService.Instance.services.getHeroList.HeroResponses.Any() is false) return;
        
        var firstHero = NetworkService.Instance.services.getHeroList.HeroResponses.First();
        EventManager.EmitEventData(EventName.Select.PlantHero, firstHero.GetID());
    }
}
