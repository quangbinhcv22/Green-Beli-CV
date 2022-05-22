using GEvent;
using GRBEGame.UI.Screen.LoginForm;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class EmitSetPasswordRoleButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private AccountRoleViewMode accountRoleViewMode;

    private void Awake()
    {
        button.onClick.AddListener(EmitAccountRoleViewModeEvent);
    }

    private void EmitAccountRoleViewModeEvent()
    {
        EventManager.EmitEventData(EventName.UI.Select<AccountRoleViewMode>(), accountRoleViewMode);
    }
}
