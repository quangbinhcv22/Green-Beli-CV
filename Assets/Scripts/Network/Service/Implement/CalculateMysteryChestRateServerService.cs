using System;
using System.Collections.Generic;
using System.Linq;
using GNetwork;
using GRBEGame.Localization;
using Network.Messages;
using Newtonsoft.Json;
using SandBox.MysteryChest;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(CalculateMysteryChestRateServerService), menuName = "NetworkAPI/CalculateMysteryChestRate")]
    public class CalculateMysteryChestRateServerService : ScriptableObject, IServerAPI
    {
        private static CalculateMysteryChestRateServerService Instance => NetworkApiManager.GetAPI<CalculateMysteryChestRateServerService>();
        public static MessageResponse<List<MysteryChestRateResponse>> Response => Instance._response;
        public static List<MysteryChestRateResponse> Data => Response.data ?? new List<MysteryChestRateResponse>();
        public string APIName => GEvent.EventName.Server.CalculateMysteryChestRate;
        [NonSerialized] private MessageResponse<List<MysteryChestRateResponse>> _response;

        
        public static void SendRequest() => Message.Instance().SetId(GEvent.EventName.Server.CalculateMysteryChestRate).Send();
        
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<List<MysteryChestRateResponse>>>(message);
            if(_response.IsError) return;
            
            Data.ForEach(item => item.RewardTypeConvert(item.type));
        }

        public static List<MysteryChestRateResponse> SortDataList()
        {
            if (Response.IsError) return new List<MysteryChestRateResponse>();
            return Response.data.OrderByDescending(item => item.quantity).ToList()
                .OrderByDescending(item => item.rewardType).ToList();
        }
    }
   
    [Serializable]
    public class MysteryChestRateResponse
    {
        public int quantity;
        public float rate;
        public string type;
        public RewardMysteryType rewardType = RewardMysteryType.MISS;

        public void RewardTypeConvert(string typeMys)
        {
            rewardType = typeMys switch
            {
                nameof(RewardMysteryType.GFRUIT) => RewardMysteryType.GFRUIT,
                nameof(RewardMysteryType.BELI) => RewardMysteryType.BELI,
                nameof(RewardMysteryType.LAND_FRAGMENT) => RewardMysteryType.LAND_FRAGMENT,
                _ => RewardMysteryType.MISS
            };
        }
    }
}