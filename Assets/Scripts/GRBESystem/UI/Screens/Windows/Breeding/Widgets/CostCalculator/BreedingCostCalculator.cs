using System.Collections.Generic;
using System.Linq;
using GEvent;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

namespace GRBESystem.UI.Screens.Windows.Breeding.Widgets.CostCalculator
{
    public class BreedingCostCalculator : CoinCostCalculator
    {
        private static ServerServiceGroup ServerServices => NetworkService.Instance.services;
        private CoinCost _coinCost;

        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.BreedingHeroes, OnSelectBreedingHeroes);
        }

        private void OnSelectBreedingHeroes()
        {
            var breedingHeroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.BreedingHeroes);
            
            if(GameManager.Instance is null) return;
            
            breedingHeroIds = breedingHeroIds.Where(heroId => heroId != GameManager.Instance.breedingConfig.noneHeroId ).ToList();

            _coinCost.grbe = breedingHeroIds.Sum(heroId => GetBreedingCost(GetBreedingTime(heroId)).grbe);
            _coinCost.gFruit = breedingHeroIds.Sum(heroId => GetBreedingCost(GetBreedingTime(heroId)).gFruit);

            onUpdateView?.Invoke(_coinCost);
        }

        private int GetBreedingTime(long heroId)
        {
            return ServerServices.getHeroList.HeroResponses.GetHeroInfo(heroId).GetBreedingTime();
        }

        private CoinCost GetBreedingCost(int breedingTime)
        {
            return ServerServices.loadGameConfig.ResponseData.breeding.GetBreedingCost(breedingTime);
        }
    }
}