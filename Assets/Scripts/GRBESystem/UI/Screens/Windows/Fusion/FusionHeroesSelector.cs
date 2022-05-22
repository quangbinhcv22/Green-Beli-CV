using System;
using System.Collections.Generic;
using System.Linq;
using Config.Other;
using GEvent;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.Fusion
{
    public class FusionHeroesSelector : MonoBehaviour
    {
        private static SelectHeroConfig SelectConfig => GameManager.Instance.selectHeroConfig;

        private static long _nonHeroId;
        private static List<long> _nonHeroList;

        private void Awake()
        {
            if (SelectConfig is null) return;

            _nonHeroId = SelectConfig.nonHeroId;
            _nonHeroList = SelectConfig.CreateNonHeroList(SelectConfig.StandardFusionHeroCount);
        }


        private void OnEnable()
        {
            EventManager.StartListening(EventName.PlayerEvent.SelectHero, OnSelectHero);
            EventManager.StartListening(EventName.Server.GetListHero, CheckExistSelectedHeroes);
            EventManager.StartListening(EventName.Server.FusionSuccess, OnFusionSuccess);

            ResetSelectedHeroes();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.PlayerEvent.SelectHero, OnSelectHero);
            EventManager.StopListening(EventName.Server.GetListHero, CheckExistSelectedHeroes);
            EventManager.StopListening(EventName.Server.FusionSuccess, OnFusionSuccess);

            // ResetSelectedHeroes();
        }


        private void OnSelectHero()
        {
            var heroIds = GetSelectedHeroIds();
            heroIds[heroIds.FindIndex(_nonHeroId.Equals)] =
                EventManager.GetData<HeroResponse>(EventName.PlayerEvent.SelectHero).GetID();

            SelectHeroes(heroIds);
        }

        private void CheckExistSelectedHeroes()
        {
            var selectedHeroIds = GetSelectedHeroIds();
            var currentHeroIds = GetCurrentHeroIds();

            var haveChanged = false;
            for (int i = 0; i < selectedHeroIds.Count; i++)
            {
                if (currentHeroIds.Contains(selectedHeroIds[i])) continue;

                haveChanged = true;
                selectedHeroIds[i] = _nonHeroId;
            }

            if (haveChanged is false) return;

            if (selectedHeroIds.First().Equals(_nonHeroId))
                selectedHeroIds = _nonHeroList;

            SelectHeroes(selectedHeroIds);
        }

        private void OnFusionSuccess()
        {
            ResetSelectedHeroes();
        }

        private void ResetSelectedHeroes()
        {
            SelectHeroes(_nonHeroList);
        }

        private static void SelectHeroes(List<long> selectedHeroes)
        {
            EventManager.EmitEventData(EventName.PlayerEvent.FusionHeroes, selectedHeroes);
        }


        private List<long> GetSelectedHeroIds()
        {
            var selectedHeroes = EventManager.GetData(EventName.PlayerEvent.FusionHeroes);
            return selectedHeroes is null ? new List<long>() : new List<long>((List<long>)selectedHeroes);
        }

        private List<long> GetCurrentHeroIds()
        {
            return NetworkService.Instance.services.getHeroList.HeroResponses.Select(hero => hero.GetID()).ToList();
        }
    }
}