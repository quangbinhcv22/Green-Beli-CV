using GEvent;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.LoginForm
{
    public class AccountRoleViewModeEmitDefault : MonoBehaviour
    {
        [SerializeField] private AccountRoleViewMode accountRoleViewMode;
        [SerializeField] private bool isEmitWhenOnEnable;

        private void OnEnable()
        {
            if (isEmitWhenOnEnable) EmitEvent();
        }

        public void EmitEvent()
        {
            EventManager.EmitEventData(EventName.UI.Select<AccountRoleViewMode>(), accountRoleViewMode);
        }
    }
}