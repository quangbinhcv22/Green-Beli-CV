using System;
using GEvent;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(GenNoneCodeServerService), menuName = "ScriptableObject/Service/Server/GenNoneCode")]
    public class GenNoneCodeServerService : ScriptableObject,  IDeserializeResponseMessage<GenNoneCodeResponse>
    {
        [NonSerialized] private MessageResponse<GenNoneCodeResponse> _response;
        public MessageResponse<GenNoneCodeResponse> Response => _response;


        public void SendRequest(GenNonCodeRequest genNonCodeRequest)
        {
            Message.Instance().SetId(EventName.Server.GenNoneCode).SetRequest(genNonCodeRequest).SetResponse(null).Send();
        }
        
        public MessageResponse<GenNoneCodeResponse> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<GenNoneCodeResponse>>(message);
            return Response;
        }
    }

    [System.Serializable]
    public class GenNoneCodeResponse
    {
        public string code;
    }
}