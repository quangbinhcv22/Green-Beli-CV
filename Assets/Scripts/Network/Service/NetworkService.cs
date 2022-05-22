using Cysharp.Threading.Tasks;
using GEvent;
using GRBESystem.Definitions;
using GRBESystem.Entity;
using Log;
using Network.Messages;
using Network.Messages.StartGame;
using Network.Service.Implement;
using Pattern;
using TigerForge;

namespace Network.Service
{
    public class NetworkService : Singleton<NetworkService>
    {
        public static readonly PlayerClientInfo playerInfo = new PlayerClientInfo();

        public ServerServiceGroup services;

        private LoadDataFromSeverReporter _loadDataFromSeverReporter;

        private void Start()
        {
            EventManager.StartListening(EventName.Server.Connect, OnConnected);
            EventManager.StartListening(EventName.Server.LoginByMetamask, OnLoginResponse);
            EventManager.StartListening(EventName.Server.GenNoneCode, OnGenNoneCodeResponse);
            EventManager.StartListening(EventName.Client.LoadEssentialData, OnLoadEssentialData);
        }

        private void OnLoadEssentialData()
        {
            EventManager.EmitEventData(EventName.PlayerEvent.PlayerInfoChange, NetworkService.playerInfo);
        }

        public bool HasServerConfig()
        {
            var messageResponse = services.loadGameConfig.Response;
            if (string.IsNullOrEmpty(messageResponse.error) == false) return false;

            return true;
        }

        private async void OnGenNoneCodeResponse()
        {
            string code = services.genNoneCode.Response.data.code;

            GLogger.LogLog(code);

#if UNITY_EDITOR
            await UniTask.Yield();
            
            var signature = BlockChainConfig.signature;
#elif UNITY_WEBGL
            var signature = await Web3GL.Sign(code);
#else
            var signature = "";
#endif
            Message.Instance().SetId(EventName.Server.VerifySignature).SetRequest(new VerifySignatureRequest()
            {
                address = playerInfo.address,
                signature = signature
            }).SetResponse(OnVerifySignatureResponse).Send();

        }


        private void OnVerifySignatureResponse(object obj)
        {
            string token = ((VerifySignatureResponse)obj).token;
            playerInfo.authToken = token;

            NetworkService.Instance.services.login.SendRequest(new LoginRequest() { token = playerInfo.authToken });
        }

        private void OnConnected()
        {
            GetLatestClientReleaseServerService.SendRequest();
        }

        public void LoginTesting()
        {
            // Message.Instance().SetId(EventName.Server.LOGIN_TESTING)
            //     .SetRequest(new LoginTestingRequest() { address = playerInfo.address })
            //     .SetResponse(OnLoginResponse).Send();
        }

        private void OnLoginResponse()
        {
            if(NetworkService.Instance.services.login.MessageResponse.IsError) return;

            var loginResponse = NetworkService.Instance.services.login.LoginResponse;
            playerInfo.SetDataFromLoginResponse(loginResponse);

            // LoadGameConfig();
            // LoadHeroList();
        }

        public bool IsNotLogged()
        {
            return NetworkService.Instance.services.login.IsNotLoggedIn;
        }

        public bool IsSelf(string address)
        {
            return address == playerInfo.address;
        }

        public StartGameResponse.PlayerInfo GetPlayerInfo(Owner ownerFilter,
            StartGameResponse.PlayerInfo[] playerInfosNotFilter)
        {
            for (int i = 0; i < playerInfosNotFilter.Length; i++)
            {
                if (ownerFilter == Owner.Self && Instance.IsSelf(playerInfosNotFilter[i].id) ||
                    ownerFilter == Owner.Opponent && Instance.IsSelf(playerInfosNotFilter[i].id) == false)
                {
                    return playerInfosNotFilter[i];
                }
            }

            const int defaultIndex = 0;
            return playerInfosNotFilter[defaultIndex];
        }

        // private void LoadGameConfig()
        // {
        //     _loadDataFromSeverReporter.ReportWaitLoad();
        //     services.loadGameConfig.SendRequest();
        // }

        // private void LoadHeroList()
        // {
        //     if(NetworkService.Instance.services.login.MessageResponse.IsError) return;
        //     
        //     _loadDataFromSeverReporter.ReportWaitLoad();
        //
        //     Message.Instance().SetId(EventName.Server.GET_LIST_HERO)
        //         .SetRequest(new GetListByBattleMode() {gameMode = nameof(GameMode.PVE)})
        //         .SetResponse(OnLoadHeroListResponse).Send();
        // }

        // private void OnLoadHeroListResponse(object obj)
        // {
        //     EventManager.EmitEventData(EventName.PlayerEvent.PLAYER_INFO_CHANGE, playerInfo);
        //
        //     EventManager.EmitEvent(EventName.Client.LoadEssentialData);
        // }
    }

    public struct LoadDataFromSeverReporter
    {
        private int _countDataNeedLoad;
        private int _countDataLoaded;

        public void ReportWaitLoad()
        {
            _countDataNeedLoad++;
        }

        public void ReportLoadSuccess()
        {
            _countDataLoaded++;

            if (_countDataNeedLoad == _countDataLoaded) OnLoadAllDataSuccess();
        }

        private void OnLoadAllDataSuccess()
        {
            EventManager.EmitEvent(EventName.Client.LoadEssentialData);
        }
    }
}