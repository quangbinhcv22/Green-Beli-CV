using System;
using System.Collections.Generic;
using GEvent;
using GRBESystem.UI.Screens.Windows.Leaderboard;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(OpenPvpBoxRewardEarnKeyServerService),
        menuName = "ScriptableObject/Service/Server/OpenPvpBox")]
    public class OpenPvpBoxRewardEarnKeyServerService : ScriptableObject,
        IDeserializeResponseMessage<List<OpenPvpChestResponse>>
    {
        [NonSerialized] private MessageResponse<List<OpenPvpChestResponse>> _response;
        public MessageResponse<List<OpenPvpChestResponse>> Response => _response;


        public void SendRequest(int value)
        {
            Message.Instance().SetId(EventName.Server.OpenPvpBoxRewardEarnKey)
                .SetRequest(new NumberPvpKey {numberPVPKey = value}).Send();
        }
        
        public MessageResponse<List<OpenPvpChestResponse>> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<List<OpenPvpChestResponse>>>(message);
            return _response;
        }
    }
    
    [System.Serializable]
    public struct NumberPvpKey
    {
        public int numberPVPKey;
    }
}
