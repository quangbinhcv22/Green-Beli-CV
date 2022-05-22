using System.Collections.Generic;
using System.Linq;
using GEvent;
using Manager.Game;
using Network.Messages.GetHeroList;
using TigerForge;

namespace GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter.Condition
{
    public class InteractableBasedBreedingCondition : IHeroSelectSlotInteractableCondition
    {
        private const int NoneBreedingTime = 0;
        
        public bool CanInteractable(HeroResponse heroResponse)
        {
            if (GameManager.Instance is null) return false;
            
            var breedingConfig = GameManager.Instance.breedingConfig;
            var breedingHeroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.BreedingHeroes);

            var isPassedMinimumLevelRequire = heroResponse.level >= breedingConfig.minimumLevelRequire;
            var haveBreedingTime = heroResponse.breeding > NoneBreedingTime;
            var isNotBreedingHero = breedingHeroIds.Contains(heroResponse.GetID()) == false;
            var canChooseMoreBreedingHero = breedingHeroIds.Count(heroId => heroId != breedingConfig.noneHeroId) < breedingConfig.heroesNumberRequire;
            
            return  isPassedMinimumLevelRequire && haveBreedingTime && isNotBreedingHero && canChooseMoreBreedingHero;
        }
    }
}