using System;
using GEvent;
using GRBEGame.UI.Screen.LoginForm;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Panels.BindingAccount
{
    public class BindingAccountPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField password1Input;
        [SerializeField] private TMP_InputField password2Input;

        [SerializeField] [Space] private TMP_Text status1Text;
        [SerializeField] private TMP_Text status2Text;

        [SerializeField] [Space] private Button bindingButton;

        [SerializeField] [Space] private int minInputLength;
        [SerializeField] private int maxInputLength;

        private bool IsValidPassword(string input)
        {
            return input.Length >= minInputLength && input.Length <= maxInputLength;
        }

        private bool IsSameInput()
        {
            return String.Compare(password1Input.text, password2Input.text, StringComparison.Ordinal) == default;
        }

        private bool IsValidInput()
        {
            var isFullInput =
                (string.IsNullOrEmpty(password1Input.text) || string.IsNullOrEmpty(password2Input.text)) is false;
            var notHaveError = string.IsNullOrEmpty(status1Text.text) && string.IsNullOrEmpty(status2Text.text);

            return isFullInput && notHaveError && IsSameInput();
        }

        private void Awake()
        {
            password1Input.onValueChanged.AddListener(OnInput1ValueChanged);
            password2Input.onValueChanged.AddListener(OnInput2ValueChanged);

            bindingButton.onClick.AddListener(BindingAccount);
        }

        private void OnEnable()
        {
            EventManager.StartListening(EventName.Server.SetPassword, OnBindingAccount);

            SetBindingButtonInteract();

            password1Input.text = string.Empty;
            password2Input.text = string.Empty;
            
            status1Text.text = string.Empty;
            status2Text.text = string.Empty;
        }

        private void OnInput1ValueChanged(string input)
        {
            status1Text.SetText(IsValidPassword(input)
                ? string.Empty
                : $"Password from {minInputLength}-{maxInputLength} characters");
            SetBindingButtonInteract();
        }

        private void OnInput2ValueChanged(string input)
        {
            status2Text.SetText(IsSameInput() ? string.Empty : $"Dissimilarity");
            SetBindingButtonInteract();
        }

        private void SetBindingButtonInteract()
        {
            bindingButton.interactable = IsValidInput();
        }

        private void BindingAccount()
        {
            // NetworkService.Instance.services.bindingAccount.SendRequest(new BindingAccountRequest()
            // {
            //     username = NetworkService.Instance.services.login.MessageResponse.data.id,
            //     password = password1Input.text,
            // });

            var data = EventManager.GetData(EventName.UI.Select<AccountRoleViewMode>());
            if(data is null) return;
            var accountRoleViewMode = (AccountRoleViewMode) data;

            switch (accountRoleViewMode.accountRole)
            {
                case AccountRole.Master:
                    NetworkService.Instance.services.setPassword.SendRequest(new BindingMasterPasswordRequest() {
                        masterPassword =  password1Input.text,
                    });
                    break;
                
                case AccountRole.Slave:
                    NetworkService.Instance.services.setPassword.SendRequest(new BindingSlavePasswordRequest() {
                        slavePassword =  password1Input.text,
                    });
                    break;
            }
        }
        
        private void OnBindingAccount()
        {
            if(NetworkService.Instance.services.setPassword.Response.IsError) return;
            
            OnSetPasswordChanged();
            gameObject.SetActive(false);
        }
        
        private void OnSetPasswordChanged()
        {
            if(NetworkService.Instance.services.setPassword.Response.IsError) return;
            
            var accountRoleViewMode = (AccountRoleViewMode)EventManager.GetData(EventName.UI.Select<AccountRoleViewMode>());
            switch (accountRoleViewMode.accountRole)
            {
                case AccountRole.Master:
                    NetworkService.Instance.services.login.SetHasMasterPassword(true);
                    break;
                
                case AccountRole.Slave:
                    NetworkService.Instance.services.login.SetHasSlavePassword(true);
                    break;
            }
        }
    }
}