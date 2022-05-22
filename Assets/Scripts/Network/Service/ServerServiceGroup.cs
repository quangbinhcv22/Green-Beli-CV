using Network.Service.Implement;
using UnityEngine;

namespace Network.Service
{
    [CreateAssetMenu(fileName = nameof(ServerServiceGroup), menuName = "ScriptableObject/Service/ServerServiceGroup")]
    public class ServerServiceGroup : ScriptableObject
    {
        public BreedingCancelServerService breedingCancel;
        public BreedingHeroServerService breedingHero;
        public BuyLotteryTicketServerService buyLotteryTicket;
        public BuyPvpTicketServerService buyPvpTicket;
        public CalculateHeroTeamPowerServerService calculateHeroTeamPower;
        public ChatInGameServerService chatInGame;
        public CheckExistLotteryLuckyNumberTodayServerService checkExistLotteryLuckyNumberToday;
        public ClaimRewardByDateServerService claimRewardByDate;
        public DepositTokenSererService depositToken;
        public FusionCancelServerService fusionCancel;
        public FusionHeroServerService fusionHero;
        public GenNoneCodeServerService genNoneCode;
        public GetHeroListServerService getHeroList;
        public GetLatestClientReleaseServerService getLatestClientRelease;
        public GetMyLotteryTicketTodayServerService getMyLotteryTicketToday;
        public GetRewardHistoryAllServerService getRewardHistoryAll;
        public GetRewardHistoryByDateServerService getRewardHistoryByDate;
        public GetTransactionHistoryServerService getTransactionHistory;
        public GetPvpContestDetailServerService getPvpContestDetail;
        public LoadGameConfigServerService loadGameConfig;
        public LoginServerService login;
        public OpenPvpBoxRewardEarnKeyServerService openPvpBox;
        public PlayPveServerService playPve;
        public RestoreHeroLevelServerService restoreHeroLevel;
        public SelectCardServerService selectCard;
        public SelectHeroServerService selectHero;
        public SkipGameService skipGame;
        public StartPhaseServerService startPhase;
        public StartRoundServerService startRound;
        public SetNationServerService setNation;
        public SetPasswordServerService setPassword;
        public TokenHasChangedServerService tokenHasChanged;
        public WithDrawTokenSererService withDrawToken;
        public WithDrawTokenCancelSererService withDrawTokenCancel;
        public WithDrawTokenSuccessSererService withDrawTokenSuccess;
    }
}