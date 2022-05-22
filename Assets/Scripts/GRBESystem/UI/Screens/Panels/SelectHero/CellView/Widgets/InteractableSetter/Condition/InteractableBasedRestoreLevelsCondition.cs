using Network.Messages.GetHeroList;


namespace GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter.Condition
{
    public class InteractableBasedRestoreLevelsCondition : IHeroSelectSlotInteractableCondition
    {
        public bool CanInteractable(HeroResponse heroResponse)
        {
            return heroResponse.level < heroResponse.maxLevel;
        }
    }
}