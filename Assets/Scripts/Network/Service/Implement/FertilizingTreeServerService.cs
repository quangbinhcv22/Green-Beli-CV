using System;
using GEvent;
using Log;
using Network.Messages;
using Network.Messages.NftTree;
using Network.Service.Implement;
using Newtonsoft.Json;
using UnityEngine;

namespace GNetwork
{
    [CreateAssetMenu(menuName = "NetworkAPI/FertilizingTree", fileName = nameof(FertilizingTreeServerService))]
    public class FertilizingTreeServerService : ScriptableObject, IServerAPI
    {
        private static FertilizingTreeServerService Instance =>
            NetworkApiManager.GetAPI<FertilizingTreeServerService>();

        [NonSerialized] private MessageResponse<FertilizingTreeResponse> _response;

        public static MessageResponse<FertilizingTreeResponse> Response => Instance._response;

        public string APIName => EventName.Server.FertilizingTree;

        public static void SendRequest(FertilizingTreeRequest data)
        {
            Message.Instance().SetId(EventName.Server.FertilizingTree).SetRequest(data).Send();
        }

        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<FertilizingTreeResponse>>(message);
            GLogger.LogLog($"<color=yellow>{message}</color>");
        }
    }

    [Serializable]
    public class FertilizingTreeRequest
    {
        [JsonProperty("treeId")]
        public int treeId;
        [JsonProperty("currency")]
        public Currency currency;
    }
    
    [Serializable]
    public class FertilizingTreeResponse
    {
        [JsonProperty("SILVER")]
        public int silver;
        [JsonProperty("GOLD")]
        public int gold;
    }
}