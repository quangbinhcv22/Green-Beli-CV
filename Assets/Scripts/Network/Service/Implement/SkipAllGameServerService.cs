using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using GNetwork;
using Manager.Inventory;
using Network.Messages;
using Network.Messages.SkipAllGame;
using Newtonsoft.Json;
using SandBox.DataInformation;
using SandBox.Tree.InfomartionPopup;
using UIFlow;
using UnityEngine;
using UnityEngine.Serialization;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(SkipAllGameServerService), menuName = "NetworkAPI/SkipAllGame")]
    public class SkipAllGameServerService : ScriptableObject, IServerAPI
    {
        private static SkipAllGameServerService Instance => NetworkApiManager.GetAPI<SkipAllGameServerService>();

        public static MessageResponse<List<SkipAllGameResponse>> Response => Instance._response;

        [NonSerialized] private MessageResponse<List<SkipAllGameResponse>> _response;


        public static List<SkipAllGameResponse> Data => Response.data ?? new List<SkipAllGameResponse>();


        [SerializeField] private SkipAllGameScreenHandler screenHandler;


        public static int GetTotalTokenReward(MoneyType moneyType)
        {
            if (Response.IsError) return default;

            return moneyType switch
            {
                MoneyType.GFruit => Response.data.Sum(item => item.rewardGFruitToken),
                MoneyType.BeLi => Response.data.Sum(item => item.rewardBeLiToken),
                MoneyType.PvpTicket => Response.data.Sum(item => item.rewardNumberPvpTicket),
                _ => default
            };
        }

        public string APIName => EventName.Server.SkipAllGame;

        public static void SendRequest() => Message.Instance().SetId(EventName.Server.SkipAllGame).Send();

        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<List<SkipAllGameResponse>>>(message);

            if (_response.IsError)
            {
                screenHandler.OnError(_response.error);
                return;
            }

            NetworkService.playerInfo.inventory.AddMoney(MoneyType.PvpTicket, GetTotalTokenReward(MoneyType.PvpTicket));
            NetworkService.playerInfo.inventory.AddMoney(MoneyType.BeLi, GetTotalTokenReward(MoneyType.BeLi));

            screenHandler.OnSuccess();
        }

        [Serializable]
        public class SkipAllGameScreenHandler
        {
            private static bool IsNotSelectHeroError(string error) => error.Contains("Choose heroes");
            private static bool IsNotEnoughEnergyError(string error) => error.Contains("Not enough energy");


            [Header("On Success")] [SerializeField]
            private UIRequest successScreenRequest;

            [Header("On Error")] [SerializeField] [Space]
            private UIRequest openInfoPopupRequest;
            [SerializeField] private InfoPopupPreset notEnoughEnergyPopupInfo;

            [SerializeField] private UIRequest onNotSelectHeroRequest;
            
            public void OnError(string error)
            {
                InfoPopupData2 popupData;

                if (IsNotSelectHeroError(error))
                {
                    onNotSelectHeroRequest.SendRequest();
                    return;
                }
                
                if (IsNotEnoughEnergyError(error)) popupData = notEnoughEnergyPopupInfo.data;
                else popupData = InfoPopupData2.Empty;

                popupData.content = error;
                openInfoPopupRequest.data = popupData;

                openInfoPopupRequest.SendRequest();
            }

            public void OnSuccess()
            {
                successScreenRequest.SendRequest();
            }
        }
    }
}