using Network.Web3;

namespace GRBESystem.UI.Screens.Windows.MainHall.Widgets.PlayerInfoPanel
{
    public class GmetaBar : MoneyBar
    {
        protected override void OnClick()
        {
            Web3Controller.Instance.GmetaToken.SwapToken();
        }
    }
}
