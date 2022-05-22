using GEvent;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

namespace GRBESystem.UI.Screens.Popups.Bank.Withdraw
{
    public class WithdrawCheckWidgetActivator : MonoBehaviour
    {
        [SerializeField] private UnityEvent canWithDrawEvent;
        [SerializeField] private UnityEvent cannotWithDrawEvent;
        private bool _isFirstUpdated;

        private void Awake()
        {
            EventManager.StartListening(EventName.Server.CheckCanWithdraw, OnCheckCanWithdraw);
        }

        private void OnEnable()
        {
            if (WithdrawCheckService.ResponseData != null && _isFirstUpdated is false)
                OnCheckCanWithdraw();
        }
        
        private void OnCheckCanWithdraw()
        {
            _isFirstUpdated = true;

            if (WithdrawCheckService.IsDataError is false && WithdrawCheckService.ResponseData.canWithdraw)
                canWithDrawEvent?.Invoke();
            else
                cannotWithDrawEvent?.Invoke();
        }
    }
}
