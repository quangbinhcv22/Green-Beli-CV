using GEvent;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.LoginForm
{
    public class LoginFormPopupController : AGrbeScreenController
    {
        [SerializeField] private Button connectWallet;

        protected override void OtherActionOnAwake()
        {
            base.OtherActionOnAwake();

            connectWallet.interactable = Application.isMobilePlatform == false;
        }


        protected override void OtherActionOnEnable()
        {
        }

        protected override void OtherActionOnDisable()
        {
            EventManager.StopListening(EventName.Server.LoginByMetamask, CloseIfSuccess);
        }

        protected override void HandleDataOpenScreenRequest(object data)
        {
            EventManager.StartListening(EventName.Server.LoginByMetamask, CloseIfSuccess);
        }

        private void CloseIfSuccess()
        {
            if (NetworkService.Instance.services.login.MessageResponse.IsError) return;
            Close();
        }
    }
}