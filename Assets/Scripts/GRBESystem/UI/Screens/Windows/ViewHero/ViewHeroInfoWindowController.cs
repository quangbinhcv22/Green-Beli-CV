using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.ViewHero
{
    public sealed class ViewHeroInfoWindowController : AGrbeScreenController
    {
        private const string SelectHeroEvent = EventName.PlayerEvent.SelectHero;
        private const string GetListHeroEvent = EventName.Server.GetListHero;
        
        [SerializeField] private HeroVisual heroVisual;
        
        
        protected override void OtherActionOnEnable()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn) return;
            
            EventManager.StartListening(SelectHeroEvent, OnSelectHero);
            EventManager.StartListening(GetListHeroEvent, OnUpdateHeroList);
            
            var selectedHero = EventManager.GetData(SelectHeroEvent);
            if (selectedHero is null ) return;

            var newSelectedHero = NetworkService.Instance.services.getHeroList.HeroResponses.GetHeroInfo(((HeroResponse)selectedHero).GetID());
            EventManager.EmitEventData(SelectHeroEvent, newSelectedHero);
        }

        protected override void OtherActionOnDisable()
        {
            EventManager.StopListening(SelectHeroEvent, OnSelectHero);
            EventManager.StopListening(GetListHeroEvent, OnUpdateHeroList);
            EventManager.EmitEvent(EventName.Model.HideAllModels);
        }

        protected override void HandleDataOpenScreenRequest(object data)
        {
        }
        
        private void OnUpdateHeroList()
        {
            var selectedHero = EventManager.GetData<HeroResponse>(SelectHeroEvent);
            var getHeroListService = NetworkService.Instance.services.getHeroList;
            if(getHeroListService.ContainHero(selectedHero.GetID()) is false) return;

            var heroResponse = getHeroListService.HeroResponses;
            var hero = getHeroListService.HeroResponses.Find(hero => hero.GetID() == selectedHero.GetID());
            EventManager.EmitEventData(SelectHeroEvent, hero);
        }

        private void OnSelectHero()
        {
            var selectedHero = EventManager.GetData<HeroResponse>(SelectHeroEvent);
            
            if (selectedHero.GetID() <= (long) default)
            {
                heroVisual.UpdateDefault();
                return;
            }
            heroVisual.UpdateView(selectedHero);
        }
    }
}