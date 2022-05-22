using System.Collections.Generic;
using Network.Service.Implement;

namespace Service.Server.GetRewardHistoryByDate
{
    [System.Serializable]
    public struct GetRewardHistoryByDateClientData : IConvertedResponseClientData<List<RewardHistorySourceResponse>>
    {
        public List<RewardHistorySourceClientData> rewardHistorySources;

        public void SetDataFromResponse(List<RewardHistorySourceResponse> response)
        {
            rewardHistorySources = RewardHistorySourceClientData.CovertFromSourceResponses(response);
        }

        [System.Serializable]
        public struct RewardHistorySourceClientData
        {
            public string date;
            public double amount;
            public bool isClaimed;
            public string type;

            public static List<RewardHistorySourceClientData> CovertFromSourceResponses(List<RewardHistorySourceResponse> responses)
            {
                var result = new List<RewardHistorySourceClientData>();
                
                foreach (var response in responses)
                {
                    result.Add(new RewardHistorySourceClientData()
                    {
                        date = response.date,
                        amount = response.amount,
                        isClaimed = response.isClaimed,
                        type = response.type,
                    });
                }

                return result;
            }
        }
    }
}