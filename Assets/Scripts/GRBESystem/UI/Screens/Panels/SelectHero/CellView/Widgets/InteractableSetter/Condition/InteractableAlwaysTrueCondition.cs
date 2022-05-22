using Network.Messages.GetHeroList;

namespace GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter.Condition
{
    public class InteractableAlwaysTrueCondition : IHeroSelectSlotInteractableCondition
    {
        public bool CanInteractable(HeroResponse heroResponse)
        {
            return true;
        }
    }
}