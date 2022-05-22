using System.Linq;
using GEvent;
using Network.Messages.GetHeroList;
using Network.Service.Implement;
using Service.Client.SelectHero;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.ViewHero.Widgets.SelectDefaultHero
{
    public class SelectDefaultHero : MonoBehaviour
    {
        [SerializeField] private SelectHeroClientService selectHeroClientService;
        [SerializeField] private GetHeroListServerService getHeroListServerService;

        private void OnEnable()
        {
            EventManager.StartListening(EventName.Server.GetListHero, AutoSelectHero);
            AutoSelectHero();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetListHero, AutoSelectHero);
        }

        private void AutoSelectHero()
        {
            if (getHeroListServerService.HeroResponses.Any() is false)
            {
                selectHeroClientService.EmitData(new HeroResponse(default));
                return;
            }

            if (EventManager.GetData(EventName.PlayerEvent.SelectHero) is null)
            {
                selectHeroClientService.EmitData(getHeroListServerService.HeroResponses.First());
                return;
            }
            
            var newHeroIdList = getHeroListServerService.HeroResponses.Select(hero => hero.GetID()).ToList();
            var oldSelectedHeroId = EventManager.GetData<HeroResponse>(EventName.PlayerEvent.SelectHero).GetID();

            if (newHeroIdList.Contains(oldSelectedHeroId)) return;
            selectHeroClientService.EmitData(getHeroListServerService.HeroResponses.First());
        }
    }
}