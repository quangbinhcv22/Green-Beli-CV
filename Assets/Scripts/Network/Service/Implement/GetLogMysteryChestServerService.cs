using System.Collections.Generic;
using System.Linq;
using GEvent;
using GNetwork;
using Network.Messages;
using Newtonsoft.Json;
using SandBox.MysteryChest;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(GetLogMysteryChestServerService), menuName = "NetworkAPI/GetLogMysteryChest")]
    public class GetLogMysteryChestServerService : ScriptableObject, IServerAPI
    {
        private static GetLogMysteryChestServerService Instance => NetworkApiManager.GetAPI<GetLogMysteryChestServerService>();
        public static MessageResponse<List<LogMysteryResponse>> Response => Instance._response;
        
        [System.NonSerialized] private MessageResponse<List<LogMysteryResponse>> _response;
        
        public static void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.GetLogMysteryChest).Send();
        }
        
        public string APIName => EventName.Server.GetLogMysteryChest;
        
        public void OnResponse(string message)
        {
            _response.data = new List<LogMysteryResponse>();
            
            var response = JsonConvert.DeserializeObject<MessageResponse<List<HistoryMysteryResponse>>>(message);
            if (response.IsError)
                _response.data = null;
            else
                response.data.ForEach(item =>
                {
                    item.rewards.ForEach(reward =>
                    {
                        _response.data.Add(new LogMysteryResponse()
                        {
                            time = item.time,
                            reward = new FragmentMystery()
                            {
                                type = reward.type,
                                number = reward.number,
                            }
                        });
                    });
                    if (item.numberChest > item.rewards.Count)
                    {
                        _response.data.Add(new LogMysteryResponse()
                        {
                            time = item.time,
                            reward = new FragmentMystery()
                            {
                                type = RewardMysteryType.MISS,
                                number = item.numberChest - item.rewards.Count,
                            }
                        });
                    }
                });
        }
        
        public static List<LogMysteryResponse> Sort()
        {
            return Instance._response.data.OrderByDescending(item => item.time).ToList();
        }
    }

    [System.Serializable]
    public class LogMysteryResponse
    {
        public long time;
        public FragmentMystery reward;
    }

    [System.Serializable]
    public class HistoryMysteryResponse
    {
        public int numberChest;
        public long time;
        public List<FragmentMystery> rewards;
    }

    [System.Serializable]
    public class FragmentMystery 
    { 
        public RewardMysteryType type; 
        public int number;
    }
}
