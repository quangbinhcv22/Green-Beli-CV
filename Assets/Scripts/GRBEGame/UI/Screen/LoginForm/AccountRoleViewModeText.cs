using GEvent;
using QB.Collection;
using TigerForge;
using TMPro;
using UnityEngine;


namespace GRBEGame.UI.Screen.LoginForm
{
    public class AccountRoleViewModeText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private DefaultableDictionary<AccountRole, string> accountRoleStrings;
        [SerializeField] private string stringFormat = "{0} Password";

        private void Awake()
        {
            EventManager.StartListening(EventName.UI.Select<AccountRoleViewMode>(), OnAccountRoleViewModeChanged);
        }

        private void OnAccountRoleViewModeChanged()
        {
            var data = EventManager.GetData(EventName.UI.Select<AccountRoleViewMode>());
            if (data is null) return;

            var accountRole = (AccountRoleViewMode) data;
            text.SetText(string.Format(stringFormat, accountRoleStrings[accountRole.accountRole]));
        }
    }
}