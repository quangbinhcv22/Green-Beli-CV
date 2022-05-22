using System;
using GEvent;
using GNetwork;
using Network.Messages;
using Network.Messages.BreedingHero;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(BreedingSuccessServerService), menuName = "NetworkAPI/BreedingSuccess")]
    public class BreedingSuccessServerService : ScriptableObject, IServerAPI
    {
        private static BreedingSuccessServerService Instance =>
            NetworkApiManager.GetAPI<BreedingSuccessServerService>();
        [NonSerialized] private MessageResponse<BreedingHeroResponse> _response;
        public static MessageResponse<BreedingHeroResponse> Response => Instance._response;
        

        public string APIName => EventName.Server.BreedingSuccess;
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<BreedingHeroResponse>>(message);
            NetworkService.Instance.services.getHeroList.AddHero(_response.data.hero);
        }
    }
}