using Network.Messages.ErrorCase;

namespace GEvent
{
    public partial class EventName
    {
        public static class Server
        {
            #region Connect

            public const string Ping = "P";
            public const string Info = "INFO";

            public const string Connect = "CONNECT";
            public const string ConnectStatus = "CONNECT_STATUS";
            public const string Disconnect = "DISCONNECT";
            public const string Stopped = "STOPED";

            #endregion


            #region Login

            public const string GenNoneCode = "GEN_NONE_CODE";
            public const string VerifySignature = "VERIFY_SIGNATURE";
            public const string LoginByMetamask = "LOGIN_BY_METAMASK";
            public const string LoginByPassword = "LOGIN_BY_PASSWORD";
            public const string LoginTesting = "LOGIN_TESTING";

            #endregion

            #region Config

            public const string LoadGameConfig = "LOAD_GAME_CONFIG";
            public const string GetLatestClientRelease = "GET_LATEST_CLIENT_RELEASE";

            #endregion


            #region Setting

            public const string SetPassword = "SET_PASSWORD";
            public const string SetNation = "SET_NATION";

            #endregion


            #region Inventory

            public const string LoadInventory = "LOAD_INVENTORY";
            public const string TokenHasChanged = "TOKEN_HAS_CHANGED";

            #endregion

            #region Energy

            public const string UpdateEnergy = "UPDATE_ENERGY";
            public const string UpgradeEnergyCapacity = "UPGRADE_ENERGY_CAPACITY";

            #endregion


            #region Hero

            public const string GetListHero = "GET_LIST_HERO";
            public const string SelectTeamHero = "SELECT_TEAM_HERO";
            public const string HeroHasChanged = "HERO_HAS_CHANGED";
            public const string CalculateHeroTeamPower = "CALCULATE_HERO_TEAM_POWER";
            public const string RestoreHeroLevel = "RESTORE_HERO_LEVEL";

            #endregion

            #region Breeding

            public const string BreedingHero = "BREEDING_HERO";
            public const string BreedingCancel = "BREEDING_CANCEL";
            public const string BreedingSuccess = "BREEDING_SUCCESS";

            #endregion

            #region Fusion

            public const string FusionHero = "FUSION_HERO";
            public const string FusionCancel = "FUSION_CANCEL";
            public const string FusionSuccess = "FUSION_SUCCESS";

            #endregion


            #region Battle

            public const string JoinArena = "JOIN_ARENA";
            public const string PlayPve = "PLAY_PVE";
            public const string PlayPvp = "PLAY_PVP";
            public const string LeaveArena = "LEAVE_ARENA";
            public const string CancelPlayPvp = "CANCEL_PLAY_PVP";
            public const string StartGame = "START_GAME";
            public const string CancelGame = "CANCEL_GAME";
            public const string EndGame = "END_GAME";
            public const string StartPhase = "START_PHASE";
            public const string StartRound = "START_ROUND";
            public const string SelectCard = "SELECT_CARD";
            public const string AttackBoss = "ATTACK_BOSS";
            public const string ChatInGame = "CHAT_IN_GAME";

            #endregion

            #region PVE

            public const string SkipGame = "SKIP_GAME";
            public const string SkipAllGame = "SKIP_ALL_GAME";

            #endregion

            #region PVP

            public const string GetPvpContestDetail = "GET_PVP_CONTEST_DETAIL";
            public const string ConvertPvpKeyToReward = "CONVERT_PVP_KEY_TO_REWARD";
            public const string OpenPvpBoxRewardEarnKey = "OPEN_PVP_BOX_REWARD_EARNKEY";
            public const string OpenPvpBoxRewardLeaderboard = "OPEN_PVP_BOX_REWARD_LEADERBOARD";
            public const string GetLogPvp = "GET_LOG_PVP";
            public const string GetLogMysteryChest = "GET_LOG_MYSTERY_CHEST";

            #endregion


            #region LockReward

            public const string GetRewardHistoryAll = "GET_REWARD_HISTORY_ALL";
            public const string GetRewardHistoryByDate = "GET_REWARD_HISTORY_BY_DATE";
            public const string ClaimRewardByDate = "CLAIM_REWARD_BY_DATE";

            #endregion


            #region Bridge

            public const string GetTransactionHistory = "GET_TRANSACTION_HISTORY";
            public const string WithdrawSuccess = "WITH_DRAW_TOKEN_SUCCESS";
            public const string DepositToken = "DEPOSIT_TOKEN";
            public const string WithDrawToken = "WITH_DRAW_TOKEN";
            public const string WithDrawTokenCancel = "WITH_DRAW_TOKEN_CANCEL";
            public const string WithDrawTokenSuccess = "WITH_DRAW_TOKEN_SUCCESS";

            #endregion


            #region Lottery

            public const string BuyLotteryTicket = "BUY_LOTTERY_TICKET";
            public const string WinLottery = "WIN_LOTTERY";
            public const string GetLotteryResult = "GET_LOTTERY_RESULT";
            public const string GetMyCurrentLotteryTicket = "GET_MY_CURRENT_LOTTERY_TICKET";
            public const string CheckExistCurrentLotteryLuckyNumber = "CHECK_EXIST_CURRENT_LOTTERY_LUCKY_NUMBER";
            public const string BuyPvpTicket = "BUY_PVP_TICKET";
            public const string GetLotteryDetail = "GET_LOTTERY_DETAIL";

            #endregion


            #region NftTree

            public const string GetListTree = "GET_LIST_TREE";
            public const string ActiveTree = "ACTIVE_TREE";
            public const string InactiveTree = "INACTIVE_TREE";
            public const string WateringTree = "WATERING_TREE";
            public const string FertilizingTree = "FERTILIZING_TREE";
            public const string RestoreDyingTree= "RESTORE_DYING_TREE";
            public const string HarvestTreeFruit= "HARVEST_TREE_FRUIT";
            public const string TreeHasChanged= "TREE_HAS_CHANGED";
            public const string BuyTreeCancel= "BUY_TREE_CANCEL";
            public const string HaveNewTree= "HaveNewTree";

            public const string CheckWhiteListBuyTree = "CheckWhiteListBuyTree";
            public const string RemainingTree = "RemainingTree";
            public const string SetEventTime = "SetEventTime";
            public const string BuyTreeStage = "CheckEventInfo";
            
            public const string OpenTreeSuccess = "OPEN_TREE_SUCCESS";

            #endregion

            public const string CheckCanWithdraw = "CheckCanWithdraw";
            
            
            #region Obsolete

            public const string GetMysteryChestInfo = "GET_MYSTERY_CHEST_INFO";
            public const string CalculateMysteryChestRate = "CALCULATE_MYSTERY_CHEST_RATE";
            public const string OpenMysteryChest = "OPEN_MYSTERY_CHEST";
            public const string ClaimMysteryChestLuckyPoint = "CLAIM_MYSTERY_CHEST_LUCKY_POINT";
            public const string MysteryChestDecor = "MysteryChestDecor";
            public const string SoundEffect = "SoundEffect";
            public const string PressMysteryChest = "PressMysteryChes";
            public const string RenewMysteryChest = "RenewMysteryChest";
            
            public static string GetErrorEventName(string serverEventName)
            {
                return $"{serverEventName}_ERROR";
            }

            public static string GetErrorCaseEventName(ErrorCase errorCase)
            {
                const string prefix = "ERROR";
                return $"{prefix}{errorCase}";
            }

            #endregion
        }
    }
}