using System;
using System.Collections.Generic;
using System.Linq;
using GNetwork;
using GRBEGame.Define;
using GRBESystem.UI.Screens.Windows.Leaderboard;
using Manager.Inventory;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(ConvertPvpKeyToRewardServerService), menuName = "NetworkAPI/ConvertPvpKeyToReward")]
    public class ConvertPvpKeyToRewardServerService : ScriptableObject, IServerAPI
    {
        private static ConvertPvpKeyToRewardServerService Instance => NetworkApiManager.GetAPI<ConvertPvpKeyToRewardServerService>();
        public static MessageResponse<List<OpenPvpChestResponse>> Response => Instance._response;
        public static List<OpenPvpChestResponse> Data => Response.data ?? new List<OpenPvpChestResponse>();
        [NonSerialized] private MessageResponse<List<OpenPvpChestResponse>> _response;
        
        
        public static void SendRequest() =>
            Message.Instance().SetId(GEvent.EventName.Server.ConvertPvpKeyToReward).Send();
        
        public static int TotalGFruit()
        {
            return Response.IsError
                ? default
                : Response.data.Where(item => (FragmentType) item.type is FragmentType.GFruit)
                    .Sum(item => item.amount);
        }

        public static int TotalFragment()
        {
            return Response.IsError
                ? default
                : Response.data.Where(item => (FragmentType) item.type != FragmentType.GFruit)
                    .Sum(item => item.amount);
        }

        public string APIName => GEvent.EventName.Server.ConvertPvpKeyToReward;

        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<List<OpenPvpChestResponse>>>(message);
            NetworkService.playerInfo.inventory.AddMoney(MoneyType.GFruit, TotalGFruit());
            NetworkService.Instance.services.login.MessageResponse.data.numberPVPKey = default;
        }
    }
}
