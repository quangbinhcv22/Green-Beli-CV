using System;
using System.Collections.Generic;
using GEvent;
using GRBESystem.Model.HeroModel.Presenter;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.TeamHero
{
    [DefaultExecutionOrder(200)]
    public class BattleHeroesPresenter : MonoBehaviour
    {
        [SerializeField] private List<HeroVisual> heroVisuals;
        [SerializeField] private HeroesPresentConfig presentConfig;

        private void Awake()
        {
            heroVisuals.ForEach(heroVisual => heroVisual.UpdateDefault());
        }

        private void OnEnable()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn) return;

            EventManager.StartListening(EventName.PlayerEvent.BattleHeroes, ReShow);
            ReShow();
        }

        private void OnDisable()
        {
            try
            {
                if (NetworkService.Instance.services.login.IsNotLoggedIn) return;
            }
            catch (NullReferenceException)
            {
                //end game
            }

            EventManager.StopListening(EventName.PlayerEvent.BattleHeroes, ReShow);
            EventManager.EmitEvent(EventName.Model.HideAllModels);
        }

        private void ReShow()
        {
            EventManager.EmitEvent(EventName.Model.HideAllModels);

            var heroIdsBoxing = EventManager.GetData(EventName.PlayerEvent.BattleHeroes);
            if(heroIdsBoxing is null) return;

            var heroIds = (List<long>)heroIdsBoxing;
            
            for (int i = 0; i < heroVisuals.Count; i++)
            {
                if (heroIds.Count < i + 1)
                {
                    heroVisuals[i].UpdateDefault();
                }
                else if (GameManager.Instance.selectHeroConfig.IsNotHero(heroIds[i]))
                {
                    heroVisuals[i].UpdateDefault();
                }
                else
                {
                    heroVisuals[i].UpdateView(GetHeroInfo(heroIds[i]));

                    var showModelRequest = presentConfig.GetShowModelRequest(index: i);
                    showModelRequest.heroId = heroIds[i];

                    EventManager.EmitEventData(EventName.Model.ShowHeroModel, showModelRequest);
                }
            }
        }

        private HeroResponse GetHeroInfo(long heroId)
        {
            return NetworkService.Instance.services.getHeroList.HeroResponses.GetHeroInfo(heroId);
        }
    }
}