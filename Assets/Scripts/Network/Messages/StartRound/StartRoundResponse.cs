using System.Collections.Generic;

namespace Network.Messages.StartRound
{
    [System.Serializable]
    public struct StartRoundResponse
    {
        public int roundIndex;
        public List<int> cardNumbers;
        public int roundTimeout;

        const int RatioToSecond = 1000;
        public int RoundSecondsOut => roundTimeout / RatioToSecond;
    }
}