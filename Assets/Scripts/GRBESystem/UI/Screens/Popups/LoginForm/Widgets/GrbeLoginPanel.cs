using System.Collections.Generic;
using GEvent;
using GRBEGame.UI.Screen.LoginForm;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.LoginForm.Widgets
{
    public class GrbeLoginPanel : MonoBehaviour
    {
        private const string AddressKey = "address";
        
        [SerializeField, Space] private TMP_InputField addressInput;
        [SerializeField] private TMP_InputField passwordInput;

        [SerializeField, Space] private Button loginButton;

        [SerializeField, Space] private TMP_Text errorText;
        [SerializeField] private List<QB.Collection.KeyValuePair<string, string>> loginErrorLocalizationConfig;


        private void Start()
        {
            loginButton.onClick.AddListener(LoginByGrbeAccount);
            EventManager.StartListening(EventName.Server.LoginByPassword, OnLoginResponse);
        }
        
        private void OnEnable()
        {
            addressInput.text = PlayerPrefs.GetString(AddressKey, string.Empty);
        }

        private static void SaveAddressInLocal(string address)
        {
            PlayerPrefs.SetString(AddressKey, address);
            PlayerPrefs.Save();
        }

        private void LoginByGrbeAccount()
        {
            if (IsEmptyInputText())
            {
                errorText.SetText(GetErrorEmptyText());
                return;
            }

            // NetworkService.Instance.services.loginBindingAccount.SendRequest(new LoginBindingAccountUnHashRequest(addressInput.text, passwordInput.text));
                
            var data = EventManager.GetData(EventName.UI.Select<AccountRoleViewMode>());
            if(data is null) return;
            var accountRoleViewMode = (AccountRoleViewMode) data;

            switch (accountRoleViewMode.accountRole)
            {
                case AccountRole.Master:
                    LoginByPasswordServerService.SendRequest(new LoginByHashMasterPasswordRequest()
                    {
                        address = addressInput.text,
                        masterPassword = passwordInput.text,
                    });
                    break;
                case AccountRole.Slave:
                    LoginByPasswordServerService.SendRequest(new LoginByHashSlavePasswordRequest()
                    {
                        address = addressInput.text,
                        slavePassword = passwordInput.text,
                    });
                    break;
            }
            
            SaveAddressInLocal(addressInput.text);
        }

        private void OnLoginResponse()
        {
            var isError = LoginByPasswordServerService.Response.IsError;
            errorText.SetText(isError ? GetErrorText() : string.Empty);
        }

        private bool IsEmptyInputText()
        {
            return string.IsNullOrEmpty(addressInput.text) || string.IsNullOrEmpty(passwordInput.text);
        }

        private string GetErrorEmptyText()
        {
            if (string.IsNullOrEmpty(addressInput.text) && string.IsNullOrEmpty(passwordInput.text)) 
                return "Missing param: User Name, Password";
            if (string.IsNullOrEmpty(addressInput.text)) 
                return "Missing param: User Name";
            return "Missing param: Password";
        }

        private string GetErrorText()
        {
            return IsEmptyInputText()
                ? GetErrorEmptyText()
                : LoginByPasswordServerService.Response.error;
        }

        // private string GetErrorText(string errorMessage)
        // {
        //     var errorIndex = loginErrorLocalizationConfig.FindIndex(errorSignalPair => errorMessage.ToLower().Contains(errorSignalPair.key.ToLower()));
        //     return errorIndex < (int)default ? string.Empty : loginErrorLocalizationConfig[errorIndex].value;
        // }
    }
}