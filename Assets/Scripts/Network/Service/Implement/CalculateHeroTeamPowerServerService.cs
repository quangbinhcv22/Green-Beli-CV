using System.Collections.Generic;
using GEvent;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(CalculateHeroTeamPowerServerService), menuName = "ScriptableObject/Service/Server/CalculateHeroTeamPower")]
    public class CalculateHeroTeamPowerServerService : ScriptableObject
    {
        public int Result { get; private set; }

        
        public int DeserializeResponseData(string message)
        {
            Result = JsonConvert.DeserializeObject<ResponseData<int>>(message).data;
            return Result;
        }

        public void SendRequest(List<long> heroIds, GameMode gameMode = GameMode.PVE)
        {
            Message.Instance().SetId(EventName.Server.CalculateHeroTeamPower)
                .SetRequest(new CalculateHeroTeamPowerRequest
                {
                    heroIds = heroIds,
                    gameMode = GetGameModeToString(gameMode),
                }).Send();
        }

        private string GetGameModeToString(GameMode gameMode)
        {
            return gameMode switch
            {
                GameMode.PVP => nameof(GameMode.PVP),
                _ => nameof(GameMode.PVE)
            };
        }
    }

    [System.Serializable]
    internal struct CalculateHeroTeamPowerRequest
    {
        public List<long> heroIds;
        public string gameMode;
    }

    [System.Serializable]
    public enum GameMode
    {
        PVE = 0,
        PVP = 1,
    }
}