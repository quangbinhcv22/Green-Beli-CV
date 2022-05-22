using System;
using GEvent;
using TigerForge;
using UIFlow;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.ConfirmPopup
{
    [RequireComponent(typeof(Button))]
    public class OpenConfirmPopupButton : MonoBehaviour
    {
        [SerializeField] private ConfirmID confirmID;

        private UIRequest _openPopupRequest;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OpenConfirmPopup);
            _openPopupRequest = new UIRequest {action = UIAction.Open, id = UIId.ConfirmPopup, haveAnimation = true};
        }

        private void OpenConfirmPopup()
        {
            _openPopupRequest.data = new ConfirmPopupData
                {id = confirmID, title = "Buy PvP Tickets", content = GetContentData()};
            _openPopupRequest.SendRequest();
        }

        private string GetContentData()
        {
            switch (confirmID)
            {
                case ConfirmID.BuyPvpTicket:
                    var ticketQuantity = EventManager.GetData(EventName.Select.BuyPvpTicketQuantity);
                    return ticketQuantity is int ? $"Are you sure you want to buy <color=#A3F409>{ticketQuantity}</color> PvP ticket(s) ?" : string.Empty;
                default:
                    return string.Empty;
            }
        }
    }
}