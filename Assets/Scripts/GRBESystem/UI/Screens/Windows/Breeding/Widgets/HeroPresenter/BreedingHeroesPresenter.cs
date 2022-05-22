using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using Extensions.Initialization.Request;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.Breeding.Widgets.HeroPresenter
{
    public class BreedingHeroesPresenter : MonoBehaviour
    {
        [SerializeField] private List<Vector3> heroPositions;
        [SerializeField] private Vector3 heroScale = Vector3.one * 1.22f;

        [SerializeField] private List<HeroVisual> heroVisuals;
        [SerializeField] private List<ShowHeroModelRequest> showHeroModelRequests;


        private void Start()
        {
            heroVisuals.ForEach(heroVisual => heroVisual.UpdateDefault());
        }

        private void OnEnable()
        {
            EventManager.StartListening(EventName.PlayerEvent.BreedingHeroes, ReShow);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.PlayerEvent.BreedingHeroes, ReShow);
            EventManager.EmitEvent(EventName.Model.HideAllModels);
        }


        private void ReShow()
        {
            if(GameManager.Instance is null) return;

            EventManager.EmitEvent(EventName.Model.HideAllModels);

            var heroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.BreedingHeroes);

            for (int i = 0; i < heroVisuals.Count; i++)
            {
                if (heroIds.Count < i + 1)
                {
                    heroVisuals[i].UpdateDefault();
                }
                else if (heroIds[i] != GameManager.Instance.breedingConfig.noneHeroId)
                {
                    heroVisuals[i]
                        .UpdateView(NetworkService.Instance.services.getHeroList.HeroResponses.GetHeroInfo(heroIds[i]));
                    EventManager.EmitEventData(EventName.Model.ShowHeroModel,
                        data: new ShowHeroModelRequest()
                        {
                            heroId = heroIds[i], position = heroPositions[i],
                            isFlip = i != 0,
                            scale = heroScale,
                            addOrderInLayer = 10,
                        });
                }
                else
                {
                    heroVisuals[i].UpdateDefault();
                }
            }
        }
    }
}