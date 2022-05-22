using System;
using GEvent;
using Network.Messages;
using Network.Messages.GetPvpContestDetail;
using Newtonsoft.Json;
using UnityEngine;


namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(GetPvpContestDetailServerService), menuName = "ScriptableObject/Service/Server/GetPvpContestDetail")]
    public class GetPvpContestDetailServerService : ScriptableObject,
        IDeserializeResponseMessage<PvpContestDetailResponse>
    {
        [NonSerialized] private MessageResponse<PvpContestDetailResponse> _response;
        public MessageResponse<PvpContestDetailResponse> Response => _response;


        public void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.GetPvpContestDetail).Send();
        }

        private int _seasonIndex;
        private bool _firstUpdate = true;
        public MessageResponse<PvpContestDetailResponse> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<PvpContestDetailResponse>>(message);
            if (_response.IsError || _seasonIndex == _response.data.seasonIndex) return _response;

            _seasonIndex = _response.data.seasonIndex;
            if (_firstUpdate)
            {
                _firstUpdate = default;
                return _response;
            }
            
            NetworkService.Instance.services.login.NewSeasonSetUp();
            return _response;
        }

        private const float MoneyPoolPercent = 0.05f;
        public double GetTotalSeasonPvpMoneyReward(PvpSeasonType type)
        {
            return  GetTotalSpendPvpTicket(type) * MoneyPoolPercent;
        }

        private int GetTotalSpendPvpTicket(PvpSeasonType type)
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (loadGameResponse.IsError) return default;

            return loadGameResponse.data.pvp.ticket_price * type switch
            {
                PvpSeasonType.Last => Response.data.totalLastSpendPVPTicket,
                PvpSeasonType.Current => Response.data.totalSpendPVPTicket,
                PvpSeasonType.Next => default,
                _ => default
            };
        }

        public int GetCurrentSeasonSpendPvpTicket(string address)
        {
            var item = Response.data.topAccounts.Find(item => item.owner == address);
            return item?.numberPVPSpendTicket ?? default;
        }
        
        
        [System.Serializable]
        public enum PvpSeasonType
        {
            Last = 0,
            Current = 1,
            Next = 2,
        }
    }
}