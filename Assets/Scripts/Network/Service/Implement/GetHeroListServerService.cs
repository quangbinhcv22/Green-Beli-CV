using System;
using System.Collections.Generic;
using System.Linq;
using Config.Mechanism;
using GEvent;
using GRBESystem.Entity;
using Network.Messages;
using Network.Messages.GetHeroList;
using Newtonsoft.Json;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(GetHeroListServerService), menuName = "ScriptableObject/Service/Server/GetHeroList")]
    public class GetHeroListServerService : ScriptableObject
    {
        public int maxSlotSelected = 3;
        public int minSelectedIndex = 1;
        public int maxSelectedIndex = 3;
        public int mainHeroSelectedIndex = 1;
        public int noneSelectedIndex = 0;

        public HeroStateShowInClientConfig heroStateShowInClientConfig;
        private List<HeroState> HeroStateShowInClient => heroStateShowInClientConfig.heroStateShowInClient;


        [NonSerialized] private List<HeroResponse> _heroList;
        public List<HeroResponse> HeroResponses => _heroList ??= new List<HeroResponse>();

        [NonSerialized] public List<HeroResponse> HeroResponsesFull;

        public void SendRequest(GameMode mode = GameMode.PVE)
        {
            Message.Instance().SetId(EventName.Server.GetListHero)
                .SetRequest(new GetListByBattleMode() {gameMode = mode.ToString()}).SetResponse(null).Send();
        }

        public bool HasHeroes()
        {
            return HeroResponses.Count > 0;
        }

        public bool ContainHero(long id)
        {
            return HeroResponses.Any(item => item.GetID() == id);
        }

        public List<HeroResponse> DeserializeResponseData(string message)
        {
            HeroResponsesFull = JsonConvert.DeserializeObject<ResponseData<List<HeroResponse>>>(message).data ??
                                new List<HeroResponse>();

            for (int i = 0; i < HeroResponsesFull.Count; i++)
            {
                var heroResponse = HeroResponsesFull[i];
                heroResponse.breeding = heroResponse.state == (int)HeroState.Breeding
                    ? heroResponse.breeding - 1
                    : heroResponse.breeding;
            }
            
            _heroList = SortHeroList();

            return HeroResponses;
        }

        private const int PvpLevel = 8;
        private const int PvpStar = 1;
        
        public HeroResponse GetHeroPvpInfo(HeroResponse heroResponse)
        {
            var hero = (HeroResponse) heroResponse.Clone();
            hero.star = PvpStar;
            hero.bodyParts.ForEach(part => part.level = PvpStar);

            if (hero.level <= PvpLevel) return hero;
            
            hero.level = PvpLevel;
            hero.exp = hero.expToUpLevel;
            return hero;
        }

        public long GetHeroAvatarId()
        {
            return HeroResponses.Any() ? _heroList.First().GetID() : HeroID.DEFAULT_HERO_ID;
        }

        public void AddHero(HeroResponse heroResponse)
        {
            HeroResponsesFull.Add(heroResponse);
            EmitEventUpdate();
        }

        public void ModifyHero(HeroResponse modifiedHero)
        {
            var modifiedHeroIndex = HeroResponsesFull.FindIndex(hero => hero.GetID().Equals(modifiedHero.GetID()));

            if (modifiedHeroIndex > -1)
            {
                HeroResponsesFull[modifiedHeroIndex] = modifiedHero;
            }

            EmitEventUpdate();
        }

        public void ModifyHeroes(List<HeroResponse> modifiedHeroes)
        {
            foreach (var modifiedHero in modifiedHeroes)
            {
                var modifiedHeroIndex = HeroResponsesFull.FindIndex(hero => hero.GetID().Equals(modifiedHero.GetID()));

                if (modifiedHeroIndex > -1)
                {
                    HeroResponsesFull[modifiedHeroIndex] = modifiedHero;
                }
            }

            EmitEventUpdate();
        }

        private void EmitEventUpdate()
        {
            _heroList = SortHeroList();
            EventManager.EmitEventData(EventName.Server.GetListHero, _heroList);
        }

        private List<HeroResponse> SortHeroList()
        {
            if (HeroResponsesFull is null) return new List<HeroResponse>();

            return (from hero in HeroResponsesFull
                where HeroStateShowInClient.Contains(hero.GetState())
                orderby hero.rarity descending, hero.level descending, hero.GetRealHeroExp() descending, hero.GetID()
                select hero).ToList();
        }
    }

    [System.Serializable]
    public struct GetListByBattleMode
    {
        public string gameMode;
    }
}