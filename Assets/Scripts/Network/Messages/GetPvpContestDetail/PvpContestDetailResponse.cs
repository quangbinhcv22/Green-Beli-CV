using System.Collections.Generic;
using GEvent;
using Network.Service;

namespace Network.Messages.GetPvpContestDetail
{
    [System.Serializable]
    public struct PvpContestDetailResponse
    {
        public long startedTime;
        public long finishedTime;
        public int seasonIndex;
        public int totalSpendPVPTicket;
        public int totalLastSpendPVPTicket;

        public PvpPlayerInfo myReward;
        public List<PvpPlayerInfo> lastRewards;
        public List<PvpPlayerInfo> topAccounts;


        public PvpPlayerInfo MyReward()
        {
            return myReward ?? new PvpPlayerInfo();
        }
        
        public PvpPlayerInfo MyRewardCurrentSeason()
        {
            const int defaultValue = -1;
            
            var selfAddress = NetworkService.Instance.services.login.MessageResponse.data.id;
            var loginResponse = NetworkService.Instance.services.login.MessageResponse;
            var selfAccountIndex = topAccounts?.FindIndex(account => account.owner == selfAddress) ?? defaultValue;

            var validIndex = selfAccountIndex >= (int)default;
            return validIndex ? topAccounts?[selfAccountIndex] : new PvpPlayerInfo()
            {
                numberPVPGame = loginResponse.data.numberPVPContesntGame,
                numberPVPWinGame = loginResponse.data.numberPVPContestWinGame,
            };
        }

        public PvpPlayerInfo GetPvpPlayerInfoByTopThisSeason(string address)
        {
            return topAccounts.Find(top => top.owner == address);
        }

        public int GetCurrentRank(string address)
        {
            const int outTopValue = -1;
            const int bonusCount = 1;
            
            for (var i = (int)default; i < topAccounts.Count; i++)
            {
                if (topAccounts[i].owner == address)
                    return i + bonusCount;
            }
            return outTopValue;
        }
    }


    [System.Serializable]
    public class PvpPlayerInfo
    {
        public bool isClaimed;
        public string id;
        public string owner;
        public string nation;
        public int rank;
        public int seasonIndex;
        public int numberPVPWinGame;
        public int numberPVPGame;
        public int numberPVPSpendTicket;
        public int totalPVPContestTicket;
        public int numberPVPGoldChest;
        public int numberPVPSilverChest;
        public int numberReceivedFreeMaterial;
        
        
        public float WinRate => numberPVPGame is (int)default ? default : (float)numberPVPWinGame / numberPVPGame;
    }
}