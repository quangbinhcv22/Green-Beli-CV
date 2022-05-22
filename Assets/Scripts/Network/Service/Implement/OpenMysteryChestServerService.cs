using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using GNetwork;
using Manager.Inventory;
using Network.Messages;
using Network.Messages.LoadGame;
using Newtonsoft.Json;
using SandBox.MysteryChest;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(OpenMysteryChestServerService),
        menuName = "NetworkAPI/OpenMysteryChest")]
    public class OpenMysteryChestServerService : ScriptableObject, IServerAPI, ITokenHasChangedService
    {
        public static OpenMysteryChestServerService Instance =>
            NetworkApiManager.GetAPI<OpenMysteryChestServerService>();

        public static MessageResponse<List<OpenMysteryChestResponse>> Response => Instance._response;
        public static List<OpenMysteryChestResponse> Data => Response.data ?? new List<OpenMysteryChestResponse>();
        [NonSerialized] private MessageResponse<List<OpenMysteryChestResponse>> _response;


        public static int GetNumberReward() => Response.IsError ? default : Response.data.Count;

        public static LoadGameConfigResponse.MysteryChest.Price GetPrice()
        {
            var gameConfigResponse = NetworkService.Instance.services.loadGameConfig.Response;
            var openChestCost = gameConfigResponse.IsError
                ? new LoadGameConfigResponse.MysteryChest.Price()
                : gameConfigResponse.data.mysteryChest.price;

            var numberReward = GetNumberReward();

            openChestCost.energy *= numberReward;
            openChestCost.gfruit *= numberReward;

            return openChestCost;
        }

        public string APIName => EventName.Server.OpenMysteryChest;

        public static void SendRequest(OpenMysteryChestRequest request)
        {
            Message.Instance().SetId(EventName.Server.OpenMysteryChest).SetRequest(request).Send();
        }

        public int GetNewGFruit()
        {
            return NetworkService.playerInfo.inventory.GetMoney(MoneyType.GFruit) - GetPrice().gfruit;
        }

        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<List<OpenMysteryChestResponse>>>(message);

            if (_response.IsError) return;
            NetworkService.playerInfo.inventory.AddMoney(MoneyType.BeLi, GetBeLiReward());
            NetworkService.playerInfo.inventory.AddMoney(MoneyType.GFruit, GetGFruitReward());
        }

        private int GetGFruitReward()
        {
            if (_response.IsError) return default;
            var list = _response.data.Where(item => item.type is RewardMysteryType.GFRUIT).ToList();
            return list.ToList().Sum(item => item.number);
        }

        private int GetBeLiReward()
        {
            if (_response.IsError) return default;
            var list = _response.data.Where(item => item.type is RewardMysteryType.BELI).ToList();
            return list.ToList().Sum(item => item.number);
        }
    }

    [Serializable]
    public class OpenMysteryChestRequest
    {
        public int numberChest;
        public List<long> heroIds;
    }

    [Serializable]
    public class OpenMysteryChestResponse
    {
        public int number;
        public RewardMysteryType type = RewardMysteryType.MISS;
    }
}