using Network.Web3;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.MainHall.Widgets.PlayerInfoPanel
{
    public class GfruitBar : MonoBehaviour
    {
        [SerializeField] private Button swapTokenButton;

        private void Awake()    
        {
            if (swapTokenButton) swapTokenButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Web3Controller.Instance.GfruitToken.SwapToken();
        }
    }
}