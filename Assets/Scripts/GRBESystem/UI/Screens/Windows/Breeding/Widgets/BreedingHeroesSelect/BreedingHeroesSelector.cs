using System.Collections.Generic;
using System.Linq;
using Command.Implement;
using GEvent;
using Manager.Game;
using Network.Controller;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.Breeding.Widgets.BreedingHeroesSelect
{
    public class BreedingHeroesSelector : MonoBehaviour
    {
        private long _noneHeroId;
        private List<long> _noneHeroIds;

        private void Awake()
        {
            _noneHeroId = GameManager.Instance.breedingConfig.noneHeroId;
            _noneHeroIds = new List<long>(GameManager.Instance.breedingConfig.heroesNumberRequire);
            _noneHeroIds.ForEach(heroId => { heroId = _noneHeroId; });

            EventManager.StartListening(EventName.Server.BreedingSuccess, OnBreedingSuccess);
        }

        private void OnEnable()
        {
            EventManager.StartListening(EventName.PlayerEvent.SelectHero, OnSelectHero);
            EventManager.StartListening(EventName.Server.GetListHero, CheckExistSelectedHeroes);

            ResetSelectBreedingHero();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.PlayerEvent.SelectHero, OnSelectHero);
            EventManager.StopListening(EventName.Server.GetListHero, CheckExistSelectedHeroes);

            ResetSelectBreedingHero();
        }

        private void OnSelectHero()
        {
            var heroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.BreedingHeroes);
            heroIds[heroIds.FindIndex(heroId => heroId == _noneHeroId)] =
                (EventManager.GetData<HeroResponse>(EventName.PlayerEvent.SelectHero).GetID());

            EventManager.EmitEventData(EventName.PlayerEvent.BreedingHeroes, data: heroIds);
        }

        private void ResetSelectBreedingHero()
        {
            EventManager.EmitEventData(EventName.PlayerEvent.BreedingHeroes,
                data: new List<long>() { _noneHeroId, _noneHeroId });
        }

        private void OnBreedingSuccess()
        {
            var breedingHeroes = EventManager.GetData<List<long>>(EventName.PlayerEvent.BreedingHeroes);

            var accuracyHeroes = new List<HeroResponse>();
            foreach (var breedingHero in breedingHeroes)
            {
                var accuracyHero = NetworkService.Instance.services.getHeroList.HeroResponses.GetHeroInfo(breedingHero);
                accuracyHero.breeding--;

                accuracyHeroes.Add(accuracyHero);
            }

            EventManager.EmitEventData(EventName.PlayerEvent.LastBreedingHeroes, accuracyHeroes);
            CommandNextServerResponseQueue.AddCommand(EventName.Server.GetListHero, new AccuracyHeroBreedingFieldCommand());
            
            NetworkService.Instance.services.getHeroList.SendRequest();

            ResetSelectBreedingHero();
        }

        private void CheckExistSelectedHeroes()
        {
            var breedingHeroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.BreedingHeroes);
            var heroIds = NetworkService.Instance.services.getHeroList.HeroResponses.Select(hero => hero.GetID())
                .ToList();

            var haveChanged = false;
            for (int i = 0; i < breedingHeroIds.Count; i++)
            {
                if (heroIds.Contains(breedingHeroIds[i])) continue;

                haveChanged = true;
                breedingHeroIds[i] = _noneHeroId;
            }

            if (haveChanged == false) return;
            EventManager.EmitEventData(EventName.PlayerEvent.BreedingHeroes, data: breedingHeroIds);
        }
    }
}