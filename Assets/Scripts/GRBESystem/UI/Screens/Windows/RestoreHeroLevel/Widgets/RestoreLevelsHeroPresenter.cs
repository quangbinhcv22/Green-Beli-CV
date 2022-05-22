using System;
using Extensions.Initialization.Request;
using GEvent;
using GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;


namespace GRBESystem.UI.Screens.Windows.RestoreHeroLevel
{
    public class RestoreLevelsHeroPresenter : MonoBehaviour
    {
        [SerializeField] private HeroVisual hero;
        [SerializeField] private ShowHeroModelRequest showHeroModelRequest;
        [SerializeField, Space] private RestoreLevelsResponseHandler restoreLevelsResponseHandler;


        private void OnEnable()
        {
            hero.UpdateDefault();

            EventManager.StartListening(EventName.PlayerEvent.SelectHero, OnSelectHero);
            EventManager.StartListening(EventName.Server.RestoreHeroLevel, OnRestoreLevelsEventReceive);
            EventManager.StartListening(EventName.Server.GetListHero, OnHeroHasChanged);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.PlayerEvent.SelectHero, OnSelectHero);
            EventManager.StopListening(EventName.Server.RestoreHeroLevel, OnRestoreLevelsEventReceive);
            EventManager.StopListening(EventName.Server.GetListHero, OnHeroHasChanged);
        }

        
        private void OnHeroHasChanged()
        {
            hero.UpdateDefault();
            EventManager.EmitEvent(EventName.Model.HideAllModels);
        }

        private void OnRestoreLevelsEventReceive()
        {
            if (NetworkService.Instance.services.restoreHeroLevel.Response.IsError) return;
            ReshowHero(restoreLevelsResponseHandler.GetRestoreLevelsHeroID());
        }

        private void OnSelectHero()
        {
            if (EventManager.GetData<SelectHeroMode>(EventName.PlayerEvent.SelectHeroMode) !=
                SelectHeroMode.RestoreLevels) return;
            ReshowHero(EventManager.GetData<HeroResponse>(EventName.PlayerEvent.SelectHero).GetID());
        }

        private void ReshowHero(long heroId)
        {
            hero.UpdateView(NetworkService.Instance.services.getHeroList.HeroResponses.GetHeroInfo(heroId));
            showHeroModelRequest.heroId = heroId;

            EventManager.EmitEvent(EventName.Model.HideAllModels);
            EventManager.EmitEventData(EventName.Model.ShowHeroModel, showHeroModelRequest);
        }
    }
}