using GEvent;
using GNetwork;
using GRBESystem.UI.Screens.Windows.MatchPvp;
using Log;
using NativeWebSocket;
using Network.Messages;
using Network.Messages.Battle;
using Network.Messages.Disconnect;
using Network.Messages.ErrorCase;
using Network.Messages.GetPvpContestDetail;
using Network.Messages.ServerMaintaining;
using Network.Messages.UpdateEnergy;
using Network.Service;
using Newtonsoft.Json;
using Pattern;
using TigerForge;
using UnityEngine;

namespace Network.Controller
{
    public class WebSocketController : Singleton<WebSocketController>
    {
#if GRBE_DEV
        private string url = "wss://game-dev.greenbeli.io/socket.io";
#elif GRBE_STAGING
        private string url = "wss://game-beta.greenbeli.io/socket.io";
#elif GRBE_PRODUCTION
        private string url = "wss://game.greenbeli.io/socket.io";
#endif

        // Start is called before the first frame update
        /// <summary>
        /// Saved WebSocket instance
        /// </summary>
        WebSocket webSocket;


        [SerializeField] private ServerServiceGroup severServices;

        private void InitializationWebSocket()
        {
            // Create the WebSocket instance
            this.webSocket = new WebSocket(url);

            // Subscribe to the WS events
            this.webSocket.OnOpen += OnOpen;
            
            
            webSocket.OnMessage += bytes =>
            {
                var message = System.Text.Encoding.UTF8.GetString(bytes);
                NetworkApiManager.OnResponse(message);
            };
            
            //[Obsolete]
            this.webSocket.OnMessage += bytes =>
            {
                var message = System.Text.Encoding.UTF8.GetString(bytes);
                SendFakeReceivedMessage(message);
            };
            

            this.webSocket.OnClose += OnClosed;
            this.webSocket.OnError += OnError;
        }

        void Update()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            if (webSocket != null)
            {
                webSocket.DispatchMessageQueue();
            }
#endif
        }

        public async void Connect()
        {
            InitializationWebSocket();
            await webSocket.Connect();
        }

        /// <summary>
        /// Called when the web socket is open, and we are ready to send and receive data
        /// </summary>
        void OnOpen()
        {
            GLogger.LogLog("WebSocket Open!");

            EventManager.EmitEvent(EventName.Server.Connect);
            EventManager.EmitEventData(EventName.Server.ConnectStatus, ConnectStatus.Connect);
        }

        /// <summary>
        /// Called when we received a text message from the server
        /// </summary>
        public void SendFakeReceivedMessage(string message)
        {
            var responseId = JsonConvert.DeserializeObject<Id>(message).id;
            var error = JsonConvert.DeserializeObject<Error>(message).error;
            
            object data = null;

            switch (responseId)
            {
                case EventName.Server.SetPassword:
                    data = severServices.setPassword.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.BreedingCancel:
                    data = severServices.breedingCancel.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.BreedingHero:
                    data = severServices.breedingHero.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.BuyLotteryTicket:
                    data = severServices.buyLotteryTicket.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.BuyPvpTicket:
                    data = severServices.buyPvpTicket.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.ChatInGame:
                    data = severServices.chatInGame.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.CheckExistCurrentLotteryLuckyNumber:
                    data = severServices.checkExistLotteryLuckyNumberToday.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.ClaimRewardByDate:
                    data = severServices.claimRewardByDate.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.DepositToken:
                    data = severServices.depositToken.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.CalculateHeroTeamPower:
                    data = severServices.calculateHeroTeamPower.DeserializeResponseData(message);
                    break;
                case EventName.Server.FusionCancel:
                    data = severServices.fusionCancel.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.FusionHero:
                    data = severServices.fusionHero.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.GenNoneCode:
                    data = severServices.genNoneCode.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.GetListHero:
                    data = severServices.getHeroList.DeserializeResponseData(message);
                    break;
                case EventName.Server.GetMyCurrentLotteryTicket:
                    data = severServices.getMyLotteryTicketToday.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.GetRewardHistoryAll:
                    data = severServices.getRewardHistoryAll.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.GetRewardHistoryByDate:
                    data = severServices.getRewardHistoryByDate.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.GetTransactionHistory:
                    data = severServices.getTransactionHistory.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.GetPvpContestDetail:
                    data = severServices.getPvpContestDetail.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.LoadGameConfig:
                    data = severServices.loadGameConfig.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.LoginByMetamask:
                case EventName.Server.LoginTesting:
                case EventName.Server.LoginByPassword:
                    responseId = EventName.Server.LoginByMetamask;
                    data = severServices.login.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.PlayPve:
                    data = severServices.playPve.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.OpenPvpBoxRewardEarnKey:
                    data = severServices.openPvpBox.DeserializeResponseMessage(message);
                    break;
                case EventName.Server.RestoreHeroLevel:
                    data = severServices.restoreHeroLevel.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.SelectTeamHero:
                    data = severServices.selectHero.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.SetNation:
                    data = severServices.setNation.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.StartPhase:
                    data = severServices.startPhase.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.TokenHasChanged:
                    data = severServices.tokenHasChanged.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.UpdateEnergy:
                    data = JsonConvert.DeserializeObject<ResponseData<UpdateEnergyResponse>>(message).data;
                    break;
                case EventName.Server.WithDrawToken:
                    data = severServices.withDrawToken.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.WithDrawTokenCancel:
                    data = severServices.withDrawTokenCancel.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.WithDrawTokenSuccess:
                    data = severServices.withDrawTokenSuccess.DeserializeResponseMessage(message).data;
                    break;

                case EventName.Server.Disconnect:
                    data = JsonConvert.DeserializeObject<ResponseData<DisconnectResponse>>(message).data;
                    break;

                case EventName.Server.HeroHasChanged:
                    // data = JsonConvert.DeserializeObject<ResponseData<string>>(message).data;
                    data = string.Empty;
                    break;

                case EventName.Server.OpenPvpBoxRewardLeaderboard:
                    data = JsonConvert.DeserializeObject<MessageResponse<PvpPlayerInfo>>(message);
                    break;

                case EventName.Server.VerifySignature:
                    data = JsonConvert.DeserializeObject<ResponseData<VerifySignatureResponse>>(message).data;
                    break;

                case EventName.Server.SkipGame:
                    data = severServices.skipGame.DeserializeResponseMessage(message).data;
                    break;
                case EventName.Server.JoinArena:
                    data = JsonConvert.DeserializeObject<ResponseData<string>>(message).data;
                    break;

                case EventName.Server.LeaveArena:
                    data = JsonConvert.DeserializeObject<ResponseData<EndGameResponse>>(message).data;
                    break;
                case EventName.Server.Info:
                    data = JsonConvert.DeserializeObject<ResponseData<ServerMaintainingResponse>>(message).data;
                    break;

                default:
                    break;
            }

            if(data != null)
                EventManager.EmitEventData(responseId, data);
            
            CommandNextServerResponseQueue.ExecuteCommands(responseId);


            if (error != null)
            {
                var errorCase = ErrorCaseSignal.GetErrorCase(error);

                switch (errorCase)
                {
                    case ErrorCase.Maintaining:
                        EventManager.EmitEventData(EventName.Server.Info,
                            data: new ServerMaintainingResponse() {info = error});
                        break;
                    default:
                        GLogger.LogLog($"Error: {EventName.Server.GetErrorEventName(responseId)}");

                        EventManager.EmitEventData(EventName.Server.GetErrorEventName(responseId), error);
                        EventManager.EmitEvent(EventName.Server.GetErrorCaseEventName(errorCase));
                        break;
                }
            }
            else if (responseId == EventName.Server.Disconnect)
            {
                var errorCase = ErrorCaseSignal.GetErrorCase(((DisconnectResponse) data).info);
                EventManager.EmitEvent(EventName.Server.GetErrorCaseEventName(errorCase));
            }
        }

        /// <summary>
        /// Called when the web socket closed
        /// </summary>
        void OnClosed(WebSocketCloseCode code)
        {
            GLogger.LogLog($"WebSocket closed! Code: {code}");
            EventManager.EmitEvent(EventName.Server.Stopped);

            // EventManager.EmitEventData(EventName.Server.CONNECT_STATUS, ConnectStatus.Disconnect);
            // webSocket = null;
        }

        /// <summary>
        /// Called when an error occured on client side
        /// </summary>
        void OnError(string error)
        {
            GLogger.LogLog($"An error occured: <color=red>{error}</color>");
            // EventManager.EmitEvent(EventName.Server.STOPED);

            // webSocket = null;
        }

        public void Send(string message)
        {
            if (Application.internetReachability is NetworkReachability.NotReachable) return;
            webSocket?.SendText(message);
        }
    }
}