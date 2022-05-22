using GRBEGame.UI.Screen.LoginForm;
using Network.Service;
using TMPro;
using UnityEngine;

public class SetPasswordText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private AccountRole accountRole;
    [SerializeField] private string hasPasswordString;
    [SerializeField] private string hasNotPasswordString;

    private void OnEnable()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        if (NetworkService.Instance.services.login.IsNotLoggedIn) text.SetText(hasNotPasswordString);
        else
        {
            var hasPassword = false;
            switch (accountRole)
            {
                case AccountRole.Master:
                    hasPassword = NetworkService.Instance.services.login.LoginResponse.hasMasterPassword;
                    break;
                
                case AccountRole.Slave:
                    hasPassword = NetworkService.Instance.services.login.LoginResponse.hasSlavePassword;
                    break;
            }

            var content = hasPassword ? hasPasswordString : hasNotPasswordString;
            text.SetText(content);
        }
    }
}
