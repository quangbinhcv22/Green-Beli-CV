using System.Collections.Generic;
using System.Linq;
using GEvent;
using GRBESystem.UI.Screens.Windows.Breeding.Widgets.CostCalculator;
using Manager.Game;
using Manager.UseFeaturesPermission;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.Breeding.Widgets.BreedingButton
{
    public class BreedingButton : MonoBehaviour, ICoinCostUpdateViewer
    {
        [SerializeField] private CoinCostCalculator costCalculator;
        [SerializeField] private Button button;
        
        private CoinCost _coinCost;
        
        
        private void Awake()
        {
            button.interactable = CanBreeding();
            costCalculator.AddCallbackUpdateView(this);
        }

        private bool CanBreeding()
        {
            if (PermissionUseFeature.CanUse(FeatureId.Breeding) is false) return false; 
            
            var breedingConfig = GameManager.Instance.breedingConfig;
            return EventManager.GetData<List<long>>(EventName.PlayerEvent.BreedingHeroes).Count(heroId => heroId != breedingConfig.noneHeroId) == breedingConfig.heroesNumberRequire && _coinCost.IsEnoughCoinCost();
        }

        public void UpdateView(CoinCost coinCost)
        {
            _coinCost = coinCost;
            button.interactable = CanBreeding();
        }
    }
}