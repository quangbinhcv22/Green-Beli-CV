using System.Collections.Generic;
using System.Linq;
using Config.Other;
using GEvent;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Messages.LoadGame;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace Calculate
{
    public static class MutualCalculator
    {
        private const float MaxTeamBuffRate = 0.2f;
        // private const float MaxPersonalBuffRate = 0.15f;

        private static ServerServiceGroup ServerServices => NetworkService.Instance.services;

        private static LoadGameConfigResponse.SubHeroMutual SubHeroConfig =>
            ServerServices.loadGameConfig.ResponseData.supportHero ?? new LoadGameConfigResponse.SubHeroMutual();

        private static GetHeroListServerService GetHeroListService => ServerServices.getHeroList;
        private static SelectHeroConfig SelectHeroConfig => GameManager.Instance.selectHeroConfig;


        public static float CalculatePercent(long heroId)
        {
            var battleHeroes = EventManager.GetData<List<long>>(EventName.PlayerEvent.BattleHeroes);
            var mainHeroId = battleHeroes.First();

            var subHero = GetHeroListService.HeroResponses.GetHeroInfo(heroId);
            var mainHero = GetHeroListService.HeroResponses.GetHeroInfo(mainHeroId);

            var mutualBuffBase = SubHeroConfig.GetMutualBuff(subHero.star, subHero.rarity);
            var mutualFactor = SubHeroConfig.GetSubHeroMutualFactor(mainHero.GetElement(), subHero.GetElement());

            return mutualBuffBase * mutualFactor;
        }

        public static float CalculatePercent(params long[] heroIds)
        {
            return FinalTeamRateBuff(heroIds.Sum(CalculatePercent));
        }

        public static float CalculatePercentSubHeroTotal()
        {
            return FinalTeamRateBuff(CalculatePercent(GetSudHeroIds().ToArray()));
        }


        private static float CalculatePercent(HeroResponse mainHero, HeroResponse subHero)
        {
            var mutualBuffBase = SubHeroConfig.GetMutualBuff(subHero.star, subHero.rarity);
            var mutualFactor = SubHeroConfig.GetSubHeroMutualFactor(mainHero.GetElement(), subHero.GetElement());

            return FinalTeamRateBuff(mutualBuffBase * mutualFactor);
        }

        public static float CalculatePercentSubHeroTotal(List<HeroResponse> standardHeroes)
        {
            var mainHero = standardHeroes.GetMainHero();
            var subHeroes = standardHeroes.GetSubHeroes();

            return FinalTeamRateBuff(subHeroes.Sum(subHero => CalculatePercent(mainHero, subHero)));
        }


        public static IEnumerable<long> GetSudHeroIds()
        {
            var battleHeroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.BattleHeroes);
            return battleHeroIds.Where(IsSubHero);
        }

        private static bool IsSubHero(long heroId, int index)
        {
            const int mainHeroIndex = 0;
            return index.Equals(mainHeroIndex) == false && SelectHeroConfig.IsNotHero(heroId) == false;
        }

        
        private static float FinalTeamRateBuff(float rateBuff) => Mathf.Min(rateBuff, MaxTeamBuffRate);
        // private static float FinalPersonalRateBuff(float rateBuff) => Mathf.Min(rateBuff, MaxPersonalBuffRate);
    }
}