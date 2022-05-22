using System.Collections.Generic;
using GEvent;
using Network.Messages.GetHeroList;
using Network.Service.Implement;
using TigerForge;
using UI.Widget.Toast;
using UnityEngine;

namespace Network.Service.ClientUpdate.HeroList
{
    public class HeroListClientUpdater : MonoBehaviour
    {
        private static Dictionary<string, IModifyHeroesServices> ModifyHeroesServices;

        [SerializeField] private GreenBeliToastDataSet toastDataSet;


        public void Awake()
        {
            EventManager.StartListening(EventName.Server.HeroHasChanged, OnHeroHasChangedResponse);
            EventManager.StartListening(EventName.Server.EndGame, SendRequestUpdateHeroList);
            EventManager.StartListening(EventName.Server.RestoreHeroLevel, OnRestoreLevelsHeroEventReceive);
            // EventManager.StartListening(EventName.Server.BREEDING_SUCCESS, SendRequestUpdateHeroList);

            
            ModifyHeroesServices = new Dictionary<string, IModifyHeroesServices>()
            {
                { EventName.Server.FusionSuccess, FusionSuccessServerService.Instance },
            };
            
            foreach (var modifyHeroesService in ModifyHeroesServices)
            {
                EventManager.StartListening(modifyHeroesService.Key,
                    () => ModifyHeroes(modifyHeroesService.Value));
            }
        }

        private void OnHeroHasChangedResponse()
        {
            EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, toastDataSet.heroHasChanged);
            SendRequestUpdateHeroList();
        }

        private void OnRestoreLevelsHeroEventReceive()
        {
            if(NetworkService.Instance.services.restoreHeroLevel.Response.IsError) return;
            SendRequestUpdateHeroList();
        }

        private void SendRequestUpdateHeroList()
        {
            NetworkService.Instance.services.getHeroList.SendRequest();
        }

        private void ModifyHeroes(IModifyHeroesServices modifyHeroesServices)
        {
            NetworkService.Instance.services.getHeroList.ModifyHeroes(modifyHeroesServices.GetModifiedHero());
        }
    }
}