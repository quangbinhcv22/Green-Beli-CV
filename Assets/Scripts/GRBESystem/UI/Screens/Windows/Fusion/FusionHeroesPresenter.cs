using System.Collections.Generic;
using GEvent;
using Extensions.Initialization.Request;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.Fusion
{
    public class FusionHeroesPresenter : MonoBehaviour
    {
        [SerializeField] private List<HeroVisual> heroVisuals;
        [SerializeField] private List<ShowHeroModelRequest> showHeroModelRequests;

        private void Start()
        {
            heroVisuals.ForEach(heroVisual => heroVisual.UpdateDefault());
            EventManager.StartListening(EventName.PlayerEvent.FusionHeroes, ReShow);
        }

        private void OnDisable()
        {
            EventManager.EmitEvent(EventName.Model.HideAllModels);
        }

        private void ReShow()
        {
            if (GameManager.Instance is null) return;

            EventManager.EmitEvent(EventName.Model.HideAllModels);

            var heroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.FusionHeroes);

            for (int i = 0; i < heroVisuals.Count; i++)
            {
                if (heroIds.Count < i + 1)
                {
                    heroVisuals[i].UpdateDefault();
                }
                else if (heroIds[i] != GameManager.Instance.breedingConfig.noneHeroId)
                {
                    heroVisuals[i].UpdateView(NetworkService.Instance.services.getHeroList.HeroResponses.GetHeroInfo(heroIds[i]));

                    var showModelHeroRequest = showHeroModelRequests[i];
                    showModelHeroRequest.heroId = heroIds[i];
                    
                    EventManager.EmitEventData(EventName.Model.ShowHeroModel, showModelHeroRequest);
                }
                else
                {
                    heroVisuals[i].UpdateDefault();
                }
            }
        }
    }
}