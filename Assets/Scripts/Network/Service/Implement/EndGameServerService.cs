using System;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using GRBESystem.Definitions;
using Manager.Inventory;
using Network.Messages;
using Network.Messages.Battle;
using Newtonsoft.Json;
using Service.Server.EndGame;
using TigerForge;
using UIFlow;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(EndGameServerService), menuName = "NetworkAPI/EndGame")]
    public class EndGameServerService : ScriptableObject, IServerAPI
    {
        private static EndGameServerService Instance => NetworkApiManager.GetAPI<EndGameServerService>();
        
        public static MessageResponse<EndGameResponse> Response => Instance._response;
        [NonSerialized] private MessageResponse<EndGameResponse> _response;

        
        public static EndGameResponse Data => Response.data;
        

        [SerializeField] private DelayResponseCallbackConfig delayResponseCallbackConfig;
        [SerializeField] private EndGameScreenHandler screenHandler;
        
        [NonSerialized] private EndGameClientData _endGameClientData;

        public static DelayResponseCallbackConfig DelayConfig => Instance.delayResponseCallbackConfig;

        
        [System.Serializable]
        public struct DelayResponseCallbackConfig
        {
            public float battleResultPopup;
        }
        
        
        public static void SetClientDataOnResponse() => Instance._endGameClientData.SetDataFromResponse(Data);

        public static EndGameClientData GetClientData()
        {
            SetClientDataOnResponse();
            return Instance._endGameClientData;
        }

        public string APIName => EventName.Server.EndGame;
        
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<EndGameResponse>>(message);
            if (_response.IsError) return;
            
            UpdateMaterial();
            screenHandler.OnSuccess();
        }

        private void UpdateMaterial()
        {
            var pvpTicket = _response.data.GetSelfInfo().rewardNumberPVPTicket;
            if (pvpTicket > (int)default) NetworkService.playerInfo.inventory.AddMoney(MoneyType.PvpTicket, pvpTicket);
            if(IsFreeRoom()) NetworkService.Instance.services.login.SubNumberReceivedFreeMaterial();

            AttackBossServerService.ReCalculateUserLoseStreak();
            NetworkService.Instance.services.getRewardHistoryAll.SendRequest();
            NetworkService.Instance.services.login.MessageResponse.data.numberPVPContestGoldChest +=
                IsGoldChest() && IsFreeRoom() is false ? 1 : default;
            NetworkService.Instance.services.login.MessageResponse.data.numberPVPContestSilverChest += IsSilverChest() ? 1 : default;
                
            if(_response.data.GetSelfInfo().rewardGFruit > (int)default)
                NetworkService.playerInfo.inventory.AddMoney(MoneyType.GFruit,
                    _response.data.GetSelfInfo().rewardGFruit);
        }
        
        private bool IsFreeRoom()
        {
            var pvpRoom = EventManager.GetData(EventName.Client.Battle.PvpRoom);
            return NetworkService.Instance.IsNotLogged() is false && (int?) pvpRoom is (int) default;
        }

        private static bool IsHasPvpTicket(Owner owner = Owner.Self)
            => Response.IsError is false &&
               Response.data.GetPlayerInfo(owner)
                   .rewardNumberPVPTicket > (int) default;
     
        public static bool IsGoldChest() => IsHasPvpTicket() is false && Response.IsError is false &&
                                            (Response.data.IsOpinionQuitPvp() || Response.data.IsLargestScoreInBattle());
    
        public static bool IsSilverChest() => IsHasPvpTicket() is false && Response.IsError is false &&
                                       Response.data.IsLargestScoreInBattle() is false;
    }

    [Serializable]
    public class EndGameScreenHandler
    {
        [SerializeField] private List<UIRequest> screenRequests;

        public void OnSuccess()
        {
            screenRequests.SendRequest();
        }
    }
}