using System;
using System.Collections.Generic;
using GEvent;
using Manager.Inventory;
using Network.Messages;
using Newtonsoft.Json;
using TigerForge;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(BuyLotteryTicketServerService), menuName = "ScriptableObject/Service/Server/BuyLotteryTicket")]
    public class BuyLotteryTicketServerService : ScriptableObject, IDeserializeResponseMessage<string>, ITokenHasChangedService
    {
        [SerializeField] private MessageResponse<string> _response;
        public MessageResponse<string> Response => _response;

        private int _lastBoughtTicketCount;


        public void SendRequest(BuyLotteryTicketRequest request)
        {
            Message.Instance().SetId(EventName.Server.BuyLotteryTicket).SetRequest(request).SetResponse(null).Send();
        }

        public MessageResponse<string> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<string>>(message);

            _lastBoughtTicketCount = _response.IsError ? default : EventManager.GetData<List<long>>(EventName.PlayerEvent.SelectedTickets).Count;
            
            return _response;
        }

        public int GetNewGFruit()
        {
            var ticketPrice = NetworkService.Instance.services.loadGameConfig.Response.data.lottery.price;
            return NetworkService.playerInfo.inventory.GetMoney(MoneyType.GFruit) - _lastBoughtTicketCount * ticketPrice;
        }
    }

    [Serializable]
    public class BuyLotteryTicketRequest
    {
        public List<long> heroIds;
    }
}