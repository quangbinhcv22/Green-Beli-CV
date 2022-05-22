using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Screen.LoginForm
{
    public class AccountRoleViewModeButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private AccountRoleViewMode accountRoleViewMode;
        [SerializeField] private Sprite spriteSelect;
        [SerializeField] private Sprite spriteDefault;

        private void Awake()
        {
            button.onClick.AddListener(EmitAccountRoleEvent);
            EventManager.StartListening(EventName.UI.Select<AccountRoleViewMode>(), OnAccountRoleViewModeChanged);
        }

        private void OnAccountRoleViewModeChanged()
        {
            var accountRole = (AccountRoleViewMode) EventManager.GetData(EventName.UI.Select<AccountRoleViewMode>());
            var isCorrectAccountRole =
                accountRole != null && accountRole.accountRole == accountRoleViewMode.accountRole;

            button.image.sprite = isCorrectAccountRole ? spriteSelect : spriteDefault;
            button.image.raycastTarget = isCorrectAccountRole is false;
        }

        private void EmitAccountRoleEvent()
        {
            EventManager.EmitEventData(EventName.UI.Select<AccountRoleViewMode>(), accountRoleViewMode);
        }
    }
}