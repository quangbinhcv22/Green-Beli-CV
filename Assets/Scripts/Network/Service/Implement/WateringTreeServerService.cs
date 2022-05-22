using System;
using GEvent;
using GRBESystem.UI.Screens.Popups.ChooseBattleMode;
using Log;
using Network.Messages;
using Network.Messages.NftTree;
using Network.Service.Implement;
using Newtonsoft.Json;
using UnityEngine;

namespace GNetwork
{
    [CreateAssetMenu(menuName = "NetworkAPI/WarteringTree", fileName = nameof(WateringTreeServerService))]
    public class WateringTreeServerService : ScriptableObject, IServerAPI
    {
        private static WateringTreeServerService Instance => NetworkApiManager.GetAPI<WateringTreeServerService>();

        [NonSerialized] private MessageResponse<WateringTreeResponse> _response;

        public static MessageResponse<WateringTreeResponse> Response => Instance._response;

        public string APIName => EventName.Server.WateringTree;

        public static void SendRequest(WateringTreeRequest data)
        {
            Message.Instance().SetId(EventName.Server.WateringTree).SetRequest(data).Send();
        }

        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<WateringTreeResponse>>(message);
            GLogger.LogLog($"<color=yellow>{message}</color>");
        }
    }

    [Serializable]
    public class WateringTreeRequest
    {
        [JsonProperty("treeId")]
        public int treeId;
        [JsonProperty("currency")]
        public Currency currency;
    }
    
    [Serializable]
    public class WateringTreeResponse
    {
        [JsonProperty("SILVER")]
        public int Silver;
    }
}