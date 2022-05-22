namespace Network.Messages.JoinArena
{
    public static class ErrorCaseJoinArena
    {
        private const string YET_NOT_SELECT_HERO_SIGNAL = "Choose heroes before";

        public enum ErrorCase
        {
            Unknown = 0,
            YetNotSelectHero = 10,
        }

        public static ErrorCase GetErrorCase(string message)
        {
            if (message.Contains(YET_NOT_SELECT_HERO_SIGNAL))
            {
                return ErrorCase.YetNotSelectHero;
            }

            return ErrorCase.Unknown;
        }
    }
}