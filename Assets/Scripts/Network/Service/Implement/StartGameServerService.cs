using System;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using Manager.Inventory;
using Network.Messages;
using Network.Messages.StartGame;
using Newtonsoft.Json;
using TigerForge;
using UI.ScreenController.Window.Battle;
using UI.ScreenController.Window.Battle.Mode;
using UI.ServerResponseHandler;
using UIFlow;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(StartGameServerService), menuName = "NetworkAPI/StartGame")]
    public class StartGameServerService : ScriptableObject, IServerAPI
    {
        private static StartGameServerService Instance => NetworkApiManager.GetAPI<StartGameServerService>();

        public static MessageResponse<StartGameResponse> Response => Instance._response;
        [NonSerialized] private MessageResponse<StartGameResponse> _response;

        [SerializeField] private StartBattleScreenHandler screenHandler;


        public static StartGameResponse Data => Response.data ?? new StartGameResponse();
        public static BattleClientData BattleClientData => Instance._battleClientData;

        private BattleClientData _battleClientData;


        private BattleMode BattleModeCurrent()
        {
            var battleMode = EventManager.GetData(EventName.Client.Battle.BattleMode);
            return battleMode is null ? BattleMode.PvE : (BattleMode) battleMode;
        }

        public string APIName => EventName.Server.StartGame;

        public void OnResponse(string message)
        {
            HideDelayPanel();

            _response = JsonConvert.DeserializeObject<MessageResponse<StartGameResponse>>(message);

            if (_response.IsError) return;


            AttackBossServerService.ResetDamageStatistical();


            ChangeSelectIndex();
            CalculateMaterial();
            SetUpBattleClientData();

            screenHandler.OnSuccess();
        }

        private void CalculateMaterial()
        {
            if (_response.IsError || BattleModeCurrent() != BattleMode.PvP) return;

            var ticketRequire = EventManager.GetData<int>(EventName.Client.Battle.PvpRoom);
            if (ticketRequire > (int) default)
                NetworkService.playerInfo.inventory.SubMoney(MoneyType.PvpTicket, ticketRequire);

            NetworkService.Instance.services.login.AddPvpGame();
            NetworkService.Instance.services.login.AddPvpContestSpendTicket(ticketRequire);
        }

        private void ChangeSelectIndex()
        {
            if (Response.IsError) return;

            foreach (var player in Response.data.players)
            {
                for (int i = 0; i < player.selectedHeros.Count; i++)
                {
                    // list index start form 0, but select index from 1
                    var modifiedHero = player.selectedHeros[i];
                    modifiedHero.selectedIndex = i + 1;
                    player.selectedHeros[i] = modifiedHero;
                }
            }
        }

        private void SetUpBattleClientData()
        {
            _battleClientData = BattleClientData.ConvertFormStartGameResponse(Response.data);
        }

        private void HideDelayPanel() => UIRequest.HideDelayPanel.SendRequest();
    }

    [Serializable]
    public class StartBattleScreenHandler
    {
        [SerializeField] private List<UIRequest> screenRequests;

        public void OnSuccess()
        {
            screenRequests.SendRequest();
        }
    }
}