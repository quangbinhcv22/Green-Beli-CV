namespace GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter.Condition
{
    public interface ICondition<T>
    {
        bool IsTrue(T data);
    }
}