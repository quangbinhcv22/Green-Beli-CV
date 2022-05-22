using GEvent;
using GRBEGame.UI.Screen.LoginForm;
using Network.Service;
using TigerForge;
using UIFlow;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.AccountInfo.Widgets
{
    public class SetPasswordButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private AccountRole accountRole;
        
        // [SerializeField] [Space] private Sprite hasPasswordSprite;
        // [SerializeField] private Sprite hasNotPasswordSprite;

        [SerializeField] [Space] private UIRequest createAccountRequest;
        [SerializeField] private UIRequest changePasswordRequest;

        private UIRequest _targetUIRequest;


        private void Awake()
        {
            UpdateViewDefault();
            button.onClick.AddListener(GoToScreen);
        }

        private void OnEnable()
        {
            UpdateInfo();
            EventManager.StartListening(EventName.Server.SetPassword, UpdateInfo);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.SetPassword, UpdateInfo);
        }

        private void UpdateInfo()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn)
            {
                UpdateViewDefault();
                return;
            }

            var hasPassword = accountRole switch
            {
                AccountRole.Master => NetworkService.Instance.services.login.LoginResponse.hasMasterPassword,
                AccountRole.Slave => NetworkService.Instance.services.login.LoginResponse.hasSlavePassword,
                _ => default
            };

            UpdateView(hasPassword);
        }

        private void UpdateViewDefault()
        {
            _targetUIRequest = createAccountRequest;
            //button.image.sprite = hasNotPasswordSprite;
        }

        private void UpdateView(bool hasPassword)
        {
            _targetUIRequest = hasPassword ? changePasswordRequest : createAccountRequest;
            //button.image.sprite = hasPassword ? hasPasswordSprite : hasNotPasswordSprite;
        }

        private void GoToScreen()
        {
            _targetUIRequest.SendRequest();
        }
    }
}