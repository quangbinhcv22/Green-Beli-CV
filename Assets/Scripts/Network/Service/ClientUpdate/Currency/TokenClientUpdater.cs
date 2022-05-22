using System.Collections.Generic;
using GEvent;
using Manager.Inventory;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace Network.Service.ClientUpdate.Currency
{
    public class TokenClientUpdater : MonoBehaviour
    {
        private static Dictionary<string, ITokenHasChangedService> _tokenHasChangedServices;
        private static ServerServiceGroup ServiceServices => NetworkService.Instance.services;

        
        private void Awake()
        {
            _tokenHasChangedServices = new Dictionary<string, ITokenHasChangedService>()
            {
                { EventName.Server.DepositToken, ServiceServices.depositToken },
                { EventName.Server.WithDrawToken, ServiceServices.withDrawToken },
                { EventName.Server.WithDrawTokenCancel, ServiceServices.withDrawTokenCancel },
                { EventName.Server.WithDrawTokenSuccess, ServiceServices.withDrawTokenSuccess },
                { EventName.Server.BreedingCancel, ServiceServices.breedingCancel },
                { EventName.Server.BreedingHero, ServiceServices.breedingHero },
                { EventName.Server.FusionSuccess, FusionSuccessServerService.Instance},
                { EventName.Server.FusionHero, ServiceServices.fusionHero },
                { EventName.Server.FusionCancel, ServiceServices.fusionCancel },
                { EventName.Server.BuyLotteryTicket, ServiceServices.buyLotteryTicket },
                { EventName.Server.OpenMysteryChest, OpenMysteryChestServerService.Instance },
            };
            
            foreach (var tokenHasChangedService in _tokenHasChangedServices)
            {
                EventManager.StartListening(tokenHasChangedService.Key,
                    () => UpdateGFruit(tokenHasChangedService.Value));
            }
        }

        private void UpdateGFruit(ITokenHasChangedService tokenHasChangedService)
        {
            NetworkService.playerInfo.inventory.SetMoney(MoneyType.GFruit, tokenHasChangedService.GetNewGFruit());
        }
    }
}