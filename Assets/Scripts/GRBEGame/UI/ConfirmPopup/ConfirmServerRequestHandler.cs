using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.ConfirmPopup
{
    public class ConfirmServerRequestHandler : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.StartListening(EventName.Mechanism.Confirm, OnConfirm);
        }

        private static void OnConfirm()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn) return;

            var boxingConfirmID = EventManager.GetData(EventName.Mechanism.Confirm);
            if (boxingConfirmID is null) return;

            var confirmID = (ConfirmID) boxingConfirmID;

            switch (confirmID)
            {
                case ConfirmID.BuyPvpTicket:
                    var ticketQuantity = EventManager.GetData(EventName.Select.BuyPvpTicketQuantity);
                    if (ticketQuantity is int)
                        BuyPvpTicketServerService.SendRequest(new BuyPvpTicketRequest
                            {numberPVPTicket = (int) ticketQuantity});
                    break;
            }
        }
    }
}