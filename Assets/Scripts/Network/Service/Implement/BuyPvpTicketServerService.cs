using System;
using GEvent;
using GRBEGame.UI.ConfirmPopup;
using Manager.Inventory;
using Network.Messages;
using Newtonsoft.Json;
using TigerForge;
using UI.Widget.Toast;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(BuyPvpTicketServerService),
        menuName = "ScriptableObject/Service/Server/BuyPvpTicket")]
    public class BuyPvpTicketServerService : ScriptableObject, IDeserializeResponseMessage<string>
    {
        [NonSerialized] private MessageResponse<string> _response;
        public MessageResponse<string> Response => _response;

        public static void SendRequest(BuyPvpTicketRequest request)
        {
            Message.Instance().SetId(EventName.Server.BuyPvpTicket).SetRequest(request).Send();
        }

        public MessageResponse<string> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<string>>(message);

            var isResponseError = _response.IsError;
            var toastData = new ToastData();

            if (isResponseError)
            {
                toastData = new ToastData {toastLevel = ToastData.ToastLevel.Danger, content = _response.error};
            }
            else
            {
                toastData = new ToastData {toastLevel = ToastData.ToastLevel.Safe, content = _response.data.ToLower()};

                Debug.Log(GetTicketBuy());
                Debug.Log(GetTicketPrice());

                NetworkService.playerInfo.inventory.SubMoney(MoneyType.GFruit, GetGFruitCost());
                NetworkService.playerInfo.inventory.AddMoney(MoneyType.PvpTicket, GetTicketBuy());
            }

            toastData.Show();

            return _response;
        }


        private static int GetGFruitCost() => GetTicketBuy() * GetTicketPrice();

        private static int GetTicketPrice()
        {
            var gameConfigResponse = NetworkService.Instance.services.loadGameConfig.Response;
            return gameConfigResponse.IsError ? default : gameConfigResponse.data.pvp.ticket_price;
        }

        private static int GetTicketBuy()
        {
            var ticketNumber = EventManager.GetData(EventName.Select.BuyPvpTicketQuantity);
            return ticketNumber as int? ?? default;
        }
    }

    [Serializable]
    public class BuyPvpTicketRequest
    {
        public int numberPVPTicket;
    }
}