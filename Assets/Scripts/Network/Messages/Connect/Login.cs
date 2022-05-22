using System;
using Network.Service;
using UnityEngine.Serialization;
using Utils;

namespace Network.Messages
{
    public struct LoginRequest
    {
        public string token;
    }

    [System.Serializable]
    public class LoginResponse
    {
        // user info
        public string id;
        public string username;
        public string nation;
        public string role;
        public bool hasMasterPassword;
        public bool hasSlavePassword;
        public string updatedNationTime;

        // token info
        public int gfrToken;
        public int belToken;
        public int energy;
        public int energyCapacityLevel;

        //pvp info
        public int numberLimitReceivedFreeMaterial;
        public int numberPVPKey;
        public int numberPVPTicket;
        public int numberPVPContestSpendTicket;
        public int numberPVPContestWinGame;
        public int numberPVPContesntGame;
        public int numberPVPContestGoldChest;
        public int numberPVPContestSilverChest;
        
        
        public DateTime UpdatedNationTime =>
            string.IsNullOrEmpty(updatedNationTime)
                ? new DateTime()
                : updatedNationTime.ToDateTime(DateTimeUtils.GreenBeliFullDateFormat);

        public DateTime UnLockUpdatedNationTime
        {
            get
            {
                var gameConfigResponse = NetworkService.Instance.services.loadGameConfig.Response;
                return gameConfigResponse.IsError
                    ? UpdatedNationTime
                    : UpdatedNationTime.AddDays(gameConfigResponse.data.nation.limitDayUpdateNation);
            }
        }

        public int LockDayChangeNation
        {
            get
            {
                if (UpdatedNationTime == new DateTime()) return default;
                return (int)(UnLockUpdatedNationTime.Date - DateTime.UtcNow.Date).TotalDays;
            }
        }
    }
}