using Network.Messages.GetHeroList;

namespace GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter.Condition
{
    public interface IHeroSelectSlotInteractableCondition
    {
        bool CanInteractable(HeroResponse heroResponse);
    }
}