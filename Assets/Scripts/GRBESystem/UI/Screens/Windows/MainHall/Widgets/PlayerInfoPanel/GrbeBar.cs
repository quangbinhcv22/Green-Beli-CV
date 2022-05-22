using Network.Web3;

namespace GRBESystem.UI.Screens.Windows.MainHall.Widgets.PlayerInfoPanel
{
    public class GrbeBar : MoneyBar
    {
        protected override void OnClick()
        {
            Web3Controller.Instance.GrbeToken.SwapToken();
        }
    }
}
