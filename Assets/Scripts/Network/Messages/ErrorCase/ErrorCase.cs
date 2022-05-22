using System;
using System.Collections.Generic;

namespace Network.Messages.ErrorCase
{
    public static class ErrorCaseSignal
    {
        private const string SomeOneLoginSameAddress = "Someone is login with same address";
        private const string YourAddressIsNotInWhitelist = "Your address is not in whitelist";
        private const string YetNotSelectHero = "Choose heroes before";
        private const string NotExistBot = "Not exist bot";
        private const string NotEnoughGfrTokenToUpgrade = "Not enough gfrToken to upgrade";

        private const string Maintenance = "maintenance";
        private const string Maintained = "maintained";
        private const string Maintaining = "maintaining";
        
        private const string NotEnoughEnergy = "not enough energy";
        private const string SelectedHeroStateNotAllowPlay = "Selected Hero State is not allow to play";

        private const string HeroListEmpty = "Your heroes are empty";

        private const string NoMatchingCompetitor = "Timeout matching player";
        private const string HeroHasChanged = "Hero has changed";

        
        private static readonly Dictionary<string, ErrorCase> ErrorCases = new Dictionary<string, ErrorCase>()
        {
            { SomeOneLoginSameAddress, ErrorCase.SomeOneLoginSameAddress },
            { YourAddressIsNotInWhitelist, ErrorCase.YourAddressIsNotInWhitelist },
            { YetNotSelectHero, ErrorCase.YetNotSelectHero },
            { NotExistBot, ErrorCase.NotExistBot },
            { NotEnoughGfrTokenToUpgrade, ErrorCase.NotEnoughGfrTokenToUpgrade },
            { SelectedHeroStateNotAllowPlay, ErrorCase.SelectedHeroStateNotAllowPlay },
            
            { Maintenance, ErrorCase.Maintaining },
            { Maintained, ErrorCase.Maintaining },
            { Maintaining, ErrorCase.Maintaining },
            
            { NotEnoughEnergy, ErrorCase.NotEnoughEnergy },
            { HeroListEmpty, ErrorCase.HeroListEmpty},
            { HeroHasChanged, ErrorCase.HeroHasChanged},
            
            { NoMatchingCompetitor, ErrorCase.NoMatchingCompetitor},
        };

        public static ErrorCase GetErrorCase(string errorMessage)
        {
            if(string.IsNullOrEmpty(errorMessage)) errorMessage = string.Empty;
            
            foreach (var errorCase in ErrorCases)
            {
                if (errorMessage.ToLower().Contains(errorCase.Key.ToLower()))
                {
                    return errorCase.Value;
                }
            }

            return ErrorCase.Unknown;
        }
    }

    public enum ErrorCase
    {
        //unspecified
        Unknown = 0,

        //form client
        YetNotAgreeTerm = -10,
        NoMatchingCompetitor = -9,
        HeroHasChanged = -8,

        //form server
        SomeOneLoginSameAddress = 10,
        YourAddressIsNotInWhitelist = 15,
        YetNotSelectHero = 20,
        NotExistBot = 30,
        NotEnoughGfrTokenToUpgrade = 40,
        Maintaining = 50,
        NotEnoughEnergy = 60,
        SelectedHeroStateNotAllowPlay = 70,
        HeroListEmpty = 80,
    }
}