using Network.Service;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    [DefaultExecutionOrder(1000)]
    public class OpenTimeBuyTicketActiveSetter : MonoBehaviour
    {
        [SerializeField] private bool isActiveOnOpenTime;

        private void OnEnable()
        {
            TimeManager.Instance.AddEvent(UpdateTime);
        }

        private void UpdateTime(int currentSeconds)
        {
            var isActive = IsOpenTime() == isActiveOnOpenTime;
            if (gameObject.activeInHierarchy == isActive) return;

            gameObject.SetActive(isActive);
        }

        private bool IsOpenTime()
        {
            return NetworkService.Instance.services.loadGameConfig.Response.data?.lottery?.IsOpenBuy() ?? default;
        }
    }
}