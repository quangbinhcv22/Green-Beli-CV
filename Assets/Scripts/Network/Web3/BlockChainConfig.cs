public class BlockChainConfig
{
    public const string requestClaimToken = "{0}/player/with-draw-gfr-token?address={1}&quantity={2}";
    public const string requestGetWithdrawInfo = "{0}/player/get-with-draw-fee?address={1}";
    public const string requestBreeding = "{0}/breeding?parent_code_1={1}&parent_code_2={2}&address={3}";
    public const string requestFusion = "{0}/fusion?main_code={1}&support_code={2}&address={3}";
    public const string requestOpenTree = "{0}/open_tree?quantity={1}&address={2}";
    public const string requestOpenTreeInfo = "{0}/data/open-tree-info?address={1}";
    public const string requestRemainingTree = "{0}/tree/get-remaining-tree?";
    
#if UNITY_EDITOR
    public const string serverUrl = "https://api-dev.greenbeli.io";
#elif GRBE_DEV
    public const string serverUrl = "https://api-dev.greenbeli.io";
#elif GRBE_STAGING
    public const string serverUrl = "https://api-beta.greenbeli.io";
#elif GRBE_PRODUCTION
    public const string serverUrl = "https://api.greenbeli.io";
#endif

#if UNITY_EDITOR
    public const string testAddress = "0x4a424AeA347861CFbB70ba92c9eaa40e48073BF1";
    public const string signature =
        "0xe7b6f3d9d32f347cde696eee6556a04e339582d96e3860fb08b4b58d7fc42e9a69bd367e95e786460bed8557025ab1d90aae920091095927f813f5d6c8151f1f1b";
#endif

    // set chain: ethereum, moonbeam, polygon etc
    public const string chain = "ethereum";
#if GRBE_DEV
    public const string rpc = "https://data-seed-prebsc-1-s1.binance.org:8545/";
    // set network mainnet, testnet
    public const string network = "testnet";

    // address of contract
    public const string urlSwapToken =
        "https://pancake.kiemtienonline360.com/#/swap?inputCurrency={0}&outputCurrency={1}";

    public const string busdTokenContract = "0x78867BbEeF44f2326bF8DDd1941a4439382EF2A7";

    public const string grbeTokenContract = "0x569ED6bc9eaE7cBC7d1b696290b4f256EDdfF42E";
    
    public const string gmetaTokenContract = "0x3d7Bb71B3F14Aa894dB01D562D198D714c9b75E1";

    public const string gfruitTokenContract = "0x534B22a213D36eaa803BB589b43779A941854240";

    public const string rewardContract = "0xfA712941D5f7fdB8FCDB9A1D05979fE6e68428C3";

    public const string treeNFTContract = "0xeAC289C5A0403D8807AF4beF45550b82C605BEd1";
    
    public const string transporterContract = "0xEe6a2E5F001Ce86Ea0e94350bb3F82EB8622Ea55";

    public const string breedingContract = "0x557d8281d5A9E7A16e47Ee52668ba3E3dbDD7747";
    
    public const string fusionContract = "0x6F3cc54c8228B6d6DA64280f88b5AF8f5397b315";

    public const string approveContract = "0xEe6a2E5F001Ce86Ea0e94350bb3F82EB8622Ea55";

    public const string plantTreeNFT = "0x1b166F1c2E3a46d57Aa4E4b1e83F7360d96c7C04";
#elif GRBE_PRODUCTION || GRBE_STAGING
    public const string rpc = "https://bsc-dataseed.binance.org/";
    // set network mainnet, testnet
    public const string network = "mainnet";
    // address of contract
    public const string urlSwapToken =
        "https://pancakeswap.finance/swap?inputCurrency={0}&outputCurrency={1}";
    public const string busdTokenContract = "0xe9e7cea3dedca5984780bafc599bd69add087d56";
    
    public const string grbeTokenContract = "0x5ca4e7294b14ea5745ee2a688990e0cb68503219";

    public const string gmetaTokenContract = "0x569ED6bc9eaE7cBC7d1b696290b4f256EDdfF42E";
    
    public const string gfruitTokenContract = "0x86d766b5b106A11731E5f816b31aAF52bA598fC6";

    public const string rewardContract = "0xB93e97E89389a74CcDf6d9259cca398625DddB25";

    public const string treeNFTContract = "0x0D56354B247a6e1b0b0190476198E95Ad13d77a4";

    public const string transporterContract = "0x07F734330553834085e9BdfB921494B10b8b6979";

    public const string breedingContract = "0xB3EC4c629DC19f17869C9AD6B4157B5BE6A0df27";

    public const string fusionContract = "0x97290FDE9417a1E5F4C6Eb1cCFb5a8a915ca084D";

    public const string approveContract = "0x07F734330553834085e9BdfB921494B10b8b6979";
    
    public const string plantTreeNFT = "0x1b166F1c2E3a46d57Aa4E4b1e83F7360d96c7C04";
#endif
}