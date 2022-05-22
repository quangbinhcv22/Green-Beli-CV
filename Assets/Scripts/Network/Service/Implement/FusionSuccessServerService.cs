using System;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using Network.Messages;
using Network.Messages.GetHeroList;
using Network.Service.ClientUpdate;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(FusionSuccessServerService), menuName = "NetworkAPI/FusionSuccess")]
    public class FusionSuccessServerService : ScriptableObject, ITokenHasChangedService, IModifyHeroesServices, IServerAPI
    {
        public static FusionSuccessServerService Instance => NetworkApiManager.GetAPI<FusionSuccessServerService>();
        
        [NonSerialized] private MessageResponse<FusionSuccessResponse> _response;
        public static MessageResponse<FusionSuccessResponse> Response => Instance._response;
        
        
        public int GetNewGFruit()
        {
            return Response.data.gfrToken;
        }

        public List<HeroResponse> GetModifiedHero()
        {
            var materialHero = NetworkService.Instance.services.getHeroList.HeroResponses.GetHeroInfo(Response.data.supportHeroId);
            materialHero.state = (int) HeroState.Burned;
            
            return new List<HeroResponse>() { Response.data.mainHero, materialHero };
        }

        public string APIName => EventName.Server.FusionSuccess;
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<FusionSuccessResponse>>(message);
        }
    }

    [System.Serializable]
    public class FusionSuccessResponse
    {
        public int gfrToken;
        public List<int> bodyPartLevelUp;
        public HeroResponse mainHero;
        public long supportHeroId;
    }
}