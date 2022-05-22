using System;
using System.Collections.Generic;
using Config.Other;
using GEvent;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.TeamHero.SelectHero
{
    public class BattleHeroesSelectedIndexFiledModifier : MonoBehaviour
    {
        private static int NoneSelectedIndex => NetworkService.Instance.services.getHeroList.noneSelectedIndex;
        private static SelectHeroConfig SelectHeroConfig => GameManager.Instance.selectHeroConfig;


        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.BattleHeroes, OnSelectBattleHeroes);
        }

        private static void OnSelectBattleHeroes()
        {
            var selectedHeroIds = GetSelectedHeroIds();

            List<HeroResponse> heroes;

            try
            {
                heroes = NetworkService.Instance.services.getHeroList.HeroResponses;

            }
            catch (NullReferenceException)
            {
                return;
            }

            for (var i = 0; i < heroes.Count; i++)
            {
                var heroId = heroes[i].GetID();
                var newSelectedIndex = GetNewSelectedIndex(selectedHeroIds, heroId);

                if (heroes[i].selectedIndex.Equals(newSelectedIndex)) continue;

                var modifiedHero = heroes[i];
                modifiedHero.selectedIndex = newSelectedIndex;

                NetworkService.Instance.services.getHeroList.ModifyHero(modifiedHero);
            }
        }

        
        private static int GetNewSelectedIndex(List<long> selectedHeroIds, long heroId)
        {
            return selectedHeroIds.Contains(heroId)
                ? QuerySelectedIndex(selectedHeroIds, heroId)
                : NoneSelectedIndex;
        }

        private static int QuerySelectedIndex(List<long> selectedHeroIds, long heroId)
        {
            const int gap = 1; // since IEnumerable starts at 0, selected index starts at 1
            return selectedHeroIds.FindIndex(heroId.Equals) + gap;
        }

        private static List<long> GetSelectedHeroIds()
        {
            var nullableHeroIds = EventManager.GetData(EventName.PlayerEvent.BattleHeroes);
            return nullableHeroIds is null ? new List<long>() : (List<long>)nullableHeroIds;
        }
    }
}