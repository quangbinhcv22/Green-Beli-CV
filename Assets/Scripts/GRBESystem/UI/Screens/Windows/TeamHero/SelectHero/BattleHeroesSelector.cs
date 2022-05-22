using System;
using System.Collections.Generic;
using System.Linq;
using Calculate;
using Config.Other;
using Cysharp.Threading.Tasks;
using GEvent;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.TeamHero.SelectHero
{
    public class BattleHeroesSelector : MonoBehaviour
    {
        private SelectHeroConfig SelectHeroConfig => GameManager.Instance.selectHeroConfig;
        private List<long> _lastValidSelectedHeroes = new List<long>();
        private bool _isLastSelectedHeroesValid;

        private void Awake()
        {
            // SelectEmpty();
            
            OnGetListHero();
            EventManager.StartListening(EventName.Server.GetListHero, OnGetListHero);
            EventManager.StartListening(EventName.PlayerEvent.BattleHeroes, OnSelectBattleHeroes);
        }

        private void SelectEmpty()
        {
            var emptyHeroes = SelectHeroConfig.CreateNonHeroList(SelectHeroConfig.StandardBattleHeroCount);
            EmitEventSelect(emptyHeroes);
        }

        private void EmitEventSelect(List<long> selectedHeroIds)
        {
            _isLastSelectedHeroesValid = SelectHeroConfig.IsValidSelectedHeroes(selectedHeroIds);
            if (_isLastSelectedHeroesValid) _lastValidSelectedHeroes = selectedHeroIds;

            EventManager.EmitEventData(EventName.PlayerEvent.BattleHeroes, selectedHeroIds.ToList());
        }


        private void OnEnable()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn) return;
            EventManager.StartListening(EventName.PlayerEvent.SelectHero, OnSelectHero);
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

            EventManager.StopListening(EventName.PlayerEvent.SelectHero, OnSelectHero);

            // if (_isLastSelectedHeroesValid == false) EmitEventSelect(_lastValidSelectedHeroes);
        }


        private void OnSelectHero()
        {
            var newSelectedHeroId = EventManager.GetData<HeroResponse>(EventName.PlayerEvent.SelectHero).GetID();
            var newSelectedHeroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.BattleHeroes);

            newSelectedHeroIds = SelectHeroConfig.ReplaceNoneHeroBy(newSelectedHeroIds, newSelectedHeroId);
            EmitEventSelect(newSelectedHeroIds);
        }

        private long _lastMainHero;

        private void OnGetListHero()
        {
            if (NetworkService.Instance.IsNotLogged() ||
                NetworkService.Instance.services.getHeroList.HeroResponsesFull is null) return;

            var selectedHeroIds = NetworkService.Instance.services.getHeroList.HeroResponses.GetStandardBattleHeroId();

            _lastMainHero = selectedHeroIds.First();
            EmitEventSelect(selectedHeroIds);
        }

        private void OnSelectBattleHeroes()
        {
            var newSelectedHeroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.BattleHeroes);

            if (SelectHeroConfig.IsClearAllHero(newSelectedHeroIds)) return;


            if (newSelectedHeroIds.First() != _lastMainHero)
            {
                _lastMainHero = newSelectedHeroIds.First();
                
                if (SelectHeroConfig.HaveMainHero(newSelectedHeroIds) is false)
                {
                    EmitEventSelect(new List<long>());
                }
                else
                {
                    var allHeroes = NetworkService.Instance.services.getHeroList.HeroResponses;
                    var canSelectToSubHeroes = allHeroes.Where(hero => hero.GetID() != newSelectedHeroIds.First()).ToList();

                    var topSelectToSubHeroes = canSelectToSubHeroes.OrderByDescending(hero => MutualCalculator.CalculatePercent(hero.GetID()));

                    const int subHeroCount = 2;
                    var newHeroSelection = new List<long>() { newSelectedHeroIds.First() };
                    newHeroSelection.AddRange( topSelectToSubHeroes.Take(subHeroCount).ToList().Select(hero => hero.GetID()));
                        
                    EmitEventSelect(newHeroSelection);
                }
            }
        }

        private bool IsModifiedComparedLastValidSelectedHeroes(List<long> newHeroIds)
        {
            var haveModify = newHeroIds.Count != _lastValidSelectedHeroes.Count;

            if (haveModify == false)
            {
                for (int i = 0; i < Mathf.Min(newHeroIds.Count, _lastValidSelectedHeroes.Count); i++)
                {
                    haveModify = newHeroIds[i] != _lastValidSelectedHeroes[i];
                    if (haveModify) break;
                }
            }

            return haveModify;
        }
    }
}