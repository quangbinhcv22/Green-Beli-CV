using System;
using GEvent;
using GNetwork;
using Manager.Inventory;
using Network.Messages;
using Newtonsoft.Json;
using TigerForge;
using UI.Widget.Toast;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(UpgradeEnergyServerService), menuName = "NetworkAPI/UpgradeEnergy")]
    public class UpgradeEnergyServerService : ScriptableObject, IServerAPI
    {
        private static UpgradeEnergyServerService Instance => NetworkApiManager.GetAPI<UpgradeEnergyServerService>();
        public static MessageResponse<string> Response => Instance._response;
        public static string Data => Response.data ?? string.Empty;
        private static MoneyType _moneyType = MoneyType.GFruit;
        [NonSerialized] private MessageResponse<string> _response;
        [SerializeField] private UpgradeEnergyToast upgradeEnergyToast;


        private static string GetUpgradeEnergyModeToString(MoneyType moneyType)
        {
            return moneyType switch
            {
                MoneyType.BeLi => nameof(UpgradeEnergyMode.BELI),
                MoneyType.GFruit => nameof(UpgradeEnergyMode.GFRUIT),
                _ => string.Empty,
            };
        }
        
        public string APIName => EventName.Server.UpgradeEnergyCapacity;

        public static void SendRequest(MoneyType moneyType = MoneyType.GFruit)
        {
            _moneyType = moneyType;
            
            Message.Instance().SetId(EventName.Server.UpgradeEnergyCapacity)
                .SetRequest(new Currency {
                    currency = GetUpgradeEnergyModeToString(moneyType)
                }).SetResponse(null).Send();
        }
        
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<string>>(message);

            if (_response.IsError) return;
            
            upgradeEnergyToast.ShowToastPanel();
            OnUpgradeEnergy();
        }

        private void OnUpgradeEnergy()
        {
            if(Response.IsError) return;
            
            NetworkService.playerInfo.energyInfo.Level ++;
            
            var cost = NetworkService.Instance.services.loadGameConfig.Response.data.energy.GetUpgradeCost(
                NetworkService.playerInfo.energyInfo.Level);
            NetworkService.playerInfo.inventory.SubMoney(_moneyType, (int) cost);
        }
    }

    [Serializable]
    public class UpgradeEnergyToast
    {
        [SerializeField] private ToastData toastData;
        
        
        public void ShowToastPanel()
        {
            EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, toastData);
        }
    }
    
    [Serializable]
    public enum UpgradeEnergyMode
    {
        BELI = 0,
        GFRUIT = 1,
    }
        
    [Serializable]
    public struct Currency
    {
        public string currency;
    }
}