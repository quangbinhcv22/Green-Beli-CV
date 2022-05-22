using System.Collections.Generic;
using System.Linq;
using GEvent;
using GNetwork;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(GetLogPvpServerService), menuName = "NetworkAPI/GetLogPvpServerService")]
    public class GetLogPvpServerService : ScriptableObject, IServerAPI
    {
        private static GetLogPvpServerService Instance => NetworkApiManager.GetAPI<GetLogPvpServerService>();
        public static MessageResponse<List<LogPvpResponse>> Response => Instance._response;
        
        [System.NonSerialized] private MessageResponse<List<LogPvpResponse>> _response;


        public static void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.GetLogPvp).Send();
        }
        
        public string APIName => EventName.Server.GetLogPvp;
        
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<List<LogPvpResponse>>>(message);
        }
        
        public static List<LogPvpResponse> Sort()
        {
            return Instance._response.data.OrderByDescending(item => item.time).ToList();
        }
    }

    [System.Serializable]
    public class LogPvpResponse
    {
        public MaterialReward material;
        public int gfrToken;
        public int numberTicket;
        public long time;
        public bool isWin;
        
        
        [System.Serializable]
        public class MaterialReward
        {
            public int number;
            public int type;
        }
    }
}
