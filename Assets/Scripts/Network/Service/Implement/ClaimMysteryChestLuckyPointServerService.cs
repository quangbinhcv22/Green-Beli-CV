using System;
using GEvent;
using GNetwork;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(ClaimMysteryChestLuckyPointServerService), menuName = "NetworkAPI/ClaimMysteryChestLuckyPoint")]
    public class ClaimMysteryChestLuckyPointServerService : ScriptableObject, IServerAPI
    {
        private static ClaimMysteryChestLuckyPointServerService Instance => NetworkApiManager.GetAPI<ClaimMysteryChestLuckyPointServerService>();
        public static MessageResponse<ClaimMysteryChestLuckyPointResponse> Response => Instance._response;
        [NonSerialized] private MessageResponse<ClaimMysteryChestLuckyPointResponse> _response;
        
        
        public static void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.ClaimMysteryChestLuckyPoint).Send();
        }

        public string APIName => EventName.Server.ClaimMysteryChestLuckyPoint;
        
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<ClaimMysteryChestLuckyPointResponse>>(message);
        }
    }
    
    [Serializable]
    public class ClaimMysteryChestLuckyPointResponse
    {
        public string status;
    }
}