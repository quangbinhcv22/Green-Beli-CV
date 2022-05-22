using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

namespace GRBEGame.UI.ConfirmPopup
{
    public class OnConfirmEventInvoker : MonoBehaviour
    {
        [SerializeField] private UnityEvent @event;

        private void OnEnable()
        {
            EventManager.StartListening(EventName.Mechanism.Confirm, OnConfirm);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Mechanism.Confirm, OnConfirm);
        }

        private void OnConfirm() => @event?.Invoke();
    }
}