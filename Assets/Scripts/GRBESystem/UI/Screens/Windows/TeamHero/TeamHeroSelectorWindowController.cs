using System;
using System.Collections.Generic;
using Network.Service;
using Network.Service.Implement;
using Service.Client.SelectHero;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.TeamHero
{
    public sealed class TeamHeroSelectorWindowController : AGrbeScreenControllerT<TeamHeroSelectorWindowData>
    {
        private static GetHeroListServerService GetHeroListService => NetworkService.Instance.services.getHeroList;

        [SerializeField, Space] private SelectHeroClientService selectHeroClientService;
        

        protected override void OtherActionOnEnable()
        {
            // selectHeroClientService.AddListenerResponse(SendMessageSelectHero);
            // EventManager.StartListening(EventName.Server.GET_LIST_HERO, ResetSelectedHeroes);

            // SetBattleModeRequest(pvpModeButton.IsSelected ? BattleMode.PvP : BattleMode.PvE);

            // EventManager.StartListening(EventName.PlayerEvent.PLAYER_INFO_CHANGE, ResetSelectedHeroes);
            // screenData.selectedHeroes = GetHeroListService.HeroResponses.GetSelectedHeroIdDefaultValueIfNull().ToList();
            // EmitEventScreenData();
        }

        protected override void OtherActionOnDisable()
        {
            // selectHeroClientService.RemoveListenerEmitEvent(SendMessageSelectHero);
        }

        protected override void HandleDataOpenScreenRequest(object data)
        {
        }
        

        // private void SendMessageSelectHero()
        // {
        //     screenData.selectedHeroes = new List<long>();
        //     for (int i = GetHeroListService.minSelectedIndex; i <= GetHeroListService.maxSlotSelected; i++)
        //     {
        //         screenData.selectedHeroes.Add(GetHeroListService.HeroResponses.Find(hero => hero.selectedIndex == i).GetID());
        //     }
        //
        //     var selectedHeroId = selectHeroClientService.GetEventEmitData().GetID();
        //
        //     var placeIndex = screenData.selectedHeroes.FindIndex(heroId => heroId == 0);
        //
        //     const int notFindIndex = -1;
        //     if (placeIndex == notFindIndex) return;
        //
        //     screenData.selectedHeroes[placeIndex] = selectedHeroId;
        //
        //     // EventManager.EmitEventData(EventName.PlayerEvent.BATTLE_HEROES, screenData.selectedHeroes);
        //     
        //     EmitEventScreenData();
        //
        //     var filteredSelectedHeroes = new List<long>();
        //     foreach (var heroId in screenData.selectedHeroes)
        //     {
        //         if (heroId == 0) continue;
        //         filteredSelectedHeroes.Add(heroId);
        //     }
        //
        //
        //     Message.Instance().SetId(EventName.Server.SELECT_HERO).SetRequest(
        //         new Network.Messages.SelectHero.SelectHeroRequest()
        //             { heroIds = filteredSelectedHeroes.ToArray() }).SetResponse(null).Send();
        // }

        // private void ResetSelectedHeroes()
        // {
        //     screenData.selectedHeroes = GetHeroListService.HeroResponses.GetSelectedHeroIdDefaultValueIfNull().ToList();
        //     EmitEventScreenData();
        // }

        protected override void RegisterEventsOnAwake()
        {
        }
    }

    [System.Serializable]
    public struct TeamHeroSelectorWindowData
    {
        public List<long> selectedHeroes;
        public int selectedHeroListIndex;

        public bool IsCanChangeHeroSlot(int heroSlotIndex)
        {
            return true;
        }


        public void AddSelectHeroInSlotAfter(long heroId)
        {
            for (int i = 0; i < selectedHeroes.Count; i++)
            {
                if (selectedHeroes[i] > 0) continue;

                selectedHeroes[i] = heroId;
                //selectedHeroListIndex = i;
                return;
            }
        }


        public void SetSelectedHero(long heroId)
        {
            long heroToSwapId = 0;
            foreach (var selectedHero in selectedHeroes)
            {
                if (selectedHero == heroId)
                {
                    heroToSwapId = selectedHero;
                    break;
                }
            }


            if (heroToSwapId != 0)
            {
                for (int i = 0; i < selectedHeroes.Count; i++)
                {
                    if (selectedHeroes[i] == heroId)
                    {
                        selectedHeroes[i] = selectedHeroes[selectedHeroListIndex - 1];
                        break;
                    }
                }

                selectedHeroes[selectedHeroListIndex - 1] = heroToSwapId;
            }
            else
            {
                // index list start from 0 but index select slot start 1
                selectedHeroes[selectedHeroListIndex - 1] = heroId;
            }
        }


        public long GetHeroIdAtIndex(int slotIndex)
        {
            try
            {
                return selectedHeroes[selectedHeroListIndex - 1];
            }
            catch (ArgumentException)
            {
                return 0;
            }
        }

        public HeroRole GetHeroRoleOfHero(long heroId)
        {
            for (int i = 0; i < selectedHeroes.Count; i++)
            {
                if (selectedHeroes[i] == heroId)
                {
                    return (HeroRole)(i + 1);
                }
            }

            return HeroRole.None;
        }

        public enum HeroRole
        {
            None = 0,
            Main = 1,
            Support1 = 2,
            Support2 = 3,
        }
    }
}