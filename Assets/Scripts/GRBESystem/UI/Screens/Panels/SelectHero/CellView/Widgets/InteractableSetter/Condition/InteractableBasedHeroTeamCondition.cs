using System.Collections.Generic;
using System.Linq;
using GEvent;
using Manager.Game;
using Network.Messages.GetHeroList;
using TigerForge;

namespace GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter.Condition
{
    public class InteractableBasedHeroTeamCondition : IHeroSelectSlotInteractableCondition
    {
        public bool CanInteractable(HeroResponse heroResponse)
        {
            return IsCanSelectHeroMore() && heroResponse.selectedIndex.Equals(0);
        }

        private bool IsCanSelectHeroMore()
        {
            var nullableHeroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.BattleHeroes);
            var heroIds = nullableHeroIds.Where(GameManager.Instance.selectHeroConfig.IsValidHeroID);

            var maxSelectedHeroSlot = GameManager.Instance.selectHeroConfig.StandardBattleHeroCount;
            return heroIds.Count() < maxSelectedHeroSlot;
        }
    }
}