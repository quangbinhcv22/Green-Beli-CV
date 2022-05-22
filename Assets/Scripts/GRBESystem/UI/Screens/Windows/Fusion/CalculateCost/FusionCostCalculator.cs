using System.Collections.Generic;
using System.Linq;
using GEvent;
using GRBESystem.UI.Screens.Windows.Breeding.Widgets.CostCalculator;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;

namespace GRBESystem.UI.Screens.Windows.Fusion.CalculateCost
{
    public class FusionCostCalculator : CoinCostCalculator
    {
        private static ServerServiceGroup ServerServices => NetworkService.Instance.services;

        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.FusionHeroes, OnSelectFusionHeroes);
        }

        private void OnSelectFusionHeroes()
        {
            var fusionHeroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.FusionHeroes);
            
            if(GameManager.Instance is null) return;

            var heroMainId = fusionHeroIds.First();
            
            onUpdateView?.Invoke(GetCoinCost(GetHeroStar(heroMainId)));
        }

        private int GetHeroStar(long heroId)
        {
            return ServerServices.getHeroList.HeroResponses.GetHeroInfo(heroId).star;
        }

        private CoinCost GetCoinCost(int star)
        {
           return ServerServices.loadGameConfig.ResponseData.fusion.GetCostNextStar(star);
        }
    }
}
