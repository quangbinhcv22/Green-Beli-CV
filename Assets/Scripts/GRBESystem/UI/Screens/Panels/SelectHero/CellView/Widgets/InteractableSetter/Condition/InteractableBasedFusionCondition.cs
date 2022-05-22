using System.Collections.Generic;
using System.Linq;
using Config.Other;
using GEvent;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;

namespace GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter.Condition
{
    public class InteractableBasedFusionCondition : IHeroSelectSlotInteractableCondition
    {
        private static SelectHeroConfig SelectConfig => GameManager.Instance.selectHeroConfig;

        public bool CanInteractable(HeroResponse heroResponse)
        {
            if (GameManager.Instance is null) return false;

            var boxedSelectedHeroes = EventManager.GetData(EventName.PlayerEvent.FusionHeroes);
            if (boxedSelectedHeroes is null) return false;

            var selectedHeroes = (List<long>)boxedSelectedHeroes;


            var isFullSelection = SelectConfig.IsFullSelection(selectedHeroes, SelectConfig.StandardFusionHeroCount);
            var isSelectedSelf = selectedHeroes.Contains(heroResponse.GetID());
            var isNotFullExp = heroResponse.GetRealHeroExp() < heroResponse.expToUpLevel;
            var isNotFullLevelAtStar = IsNotFullLevelAtStar(heroResponse);

            if (isFullSelection || isSelectedSelf || isNotFullExp || isNotFullLevelAtStar) return false;


            var haveMainHero = SelectConfig.HaveMainHero(selectedHeroes);

            if (haveMainHero)
            {
                var mainHeroId = selectedHeroes.First();
                var mainHero = NetworkService.Instance.services.getHeroList.HeroResponses.GetHeroInfo(mainHeroId);

                var isSameMainHeroStar = heroResponse.star.Equals(mainHero.star);
                var isSameMainHeroElement = heroResponse.GetElement().Equals(mainHero.GetElement());

                return isSameMainHeroStar && isSameMainHeroElement;
            }
            else
            {
                var isLessMaxStar = heroResponse.star < 6;
                return isLessMaxStar;
            }
        }

        private bool IsNotFullLevelAtStar(HeroResponse heroResponse)
        {
            var gameConfig = NetworkService.Instance.services.loadGameConfig.ResponseData;
            var maxLevelAtStar = gameConfig.levelCapacityStar.GetMaxLevelAtStar(heroResponse.star);

            return heroResponse.level.Equals(maxLevelAtStar) is false;
        }
    }
}