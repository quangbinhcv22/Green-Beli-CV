using GEvent;
using Network.Service;
using TigerForge;
using UI.Widget.Toast;
using UnityEngine;
using Utils;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    public class BuyLotteryTicketToastHandler : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.BuyLotteryTicket, OnBuyLotteryTicket);
        }

        private void OnBuyLotteryTicket()
        {
            var response = NetworkService.Instance.services.buyLotteryTicket.Response;

            var toastData = response.IsError
                ? new ToastData()
                {
                    content = response.error.ToTitleCase(),
                    toastLevel = ToastData.ToastLevel.Danger,
                }
                : new ToastData()
                {
                    content = response.data.ToTitleCase(),
                    toastLevel = ToastData.ToastLevel.Safe,
                };

            EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, toastData);
        }
    }
}