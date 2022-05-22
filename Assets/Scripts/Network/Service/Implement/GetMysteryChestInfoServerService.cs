using System;
using GEvent;
using GNetwork;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(GetMysteryChestInfoServerService), menuName = "NetworkAPI/GetMysteryChestInfo")]
    public class GetMysteryChestInfoServerService : ScriptableObject, IServerAPI
    {

        private static GetMysteryChestInfoServerService Instance => NetworkApiManager.GetAPI<GetMysteryChestInfoServerService>();
        public static MessageResponse<MysteryChestInfoResponse> Response => Instance._response;
        
        [NonSerialized] private MessageResponse<MysteryChestInfoResponse> _response;
        public static MysteryChestInfoResponse Data => Response.data;


        public static void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.GetMysteryChestInfo).Send();
        }

        public string APIName => EventName.Server.GetMysteryChestInfo;
        
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<MysteryChestInfoResponse>>(message);
        }
    }

    [Serializable]
    public class MysteryChestInfoResponse
    {
        public int myLuckyPoint;
        public int numberRemainLandFragment;
    }
}