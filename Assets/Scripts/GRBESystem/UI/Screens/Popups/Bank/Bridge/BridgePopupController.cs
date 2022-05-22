using Cysharp.Threading.Tasks;
using GRBESystem.UI.Screens.Popups.Bank.Withdraw;
using GRBESystem.UI.Screens.Popups.Bridge;
using Manager.UseFeaturesPermission;
using Network.Service;
using Network.Web3;
using TMPro;
using UI.Widget.Toast;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Bank.Bridge
{
    public class BridgePopupController : AGrbeScreenControllerT<BridgePopupData>
    {
        [Space, SerializeField] private WithdrawConfig withdrawConfig;
        [Space, SerializeField] private Button button;
        [SerializeField] private TMP_Text countdownText;

        [SerializeField] private GreenBeliToastDataSet greenBeliToastDataSet;

        protected override void RegisterEventsOnAwake()
        {
        }

        private void UpdateView()
        {
            UpdateWithDrawState().Forget();
        }

        private async UniTaskVoid UpdateWithDrawState()
        {
            var address = NetworkService.Instance.services.login.MessageResponse.data.id;

            button.interactable = false;

            withdrawConfig.info = await Web3Controller.Instance.GetWithdrawTokenInfo(address);

            if (this.enabled == false)
                return;

            if (withdrawConfig.info.fee < 1) button.interactable = PermissionUseFeature.CanUse(FeatureId.Withdraw);
        }

        private static void SetText(TMP_Text meshText, string content)
        {
            meshText.text = content;
        }

        protected override void OtherActionOnEnable()
        {
            UpdateView();
        }

        protected override void OtherActionOnDisable()
        {
        }

        protected override void HandleDataOpenScreenRequest(object data)
        {
        }
    }
}