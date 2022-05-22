namespace Manager.UseFeaturesPermission
{
    public enum FeatureId
    {
        #region Normal

        Battle = 10,
        Pve = 20,
        Pvp = 30,
        SelectHero = 40,
        LockReward = 50,
        SetNation = 60,

        #endregion

        #region Affect currency

        Bridge = 1000,
        Deposit = 1010,
        Withdraw = 1020,
        UpdateEnergy = 1030,
        Breeding = 1040,
        Fusion = 1050,
        ChangePassword = 1060,
        ClaimReward = 1070,
        LuckyGreenbie = 1080,
        RestoreLevel = 1090,
        Inventory = 1100,
        BuyPvpTicket = 1110,
        PvpSeasonReward = 1120,
        FightPvp = 1130,
        OpenPvpBox = 1140,
        
        MysteryChest = 1150,

        #endregion

        // Farming,
    }
}