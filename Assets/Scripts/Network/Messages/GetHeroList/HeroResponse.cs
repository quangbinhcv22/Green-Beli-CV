using System;
using System.Collections.Generic;
using System.Linq;
using GRBESystem.Definitions.BodyPart.Index;
using GRBESystem.Entity;
using GRBESystem.Entity.Element;
using Manager.Game;
using Network.Service;
using Network.Service.Implement;

namespace Network.Messages.GetHeroList
{
    [System.Serializable]
    public class HeroResponse : ICloneable
    {
        public int level;
        public int maxLevel;

        public int exp;
        public int expToUpLevel;

        public int breeding;
        public int breedingLimit;

        public int star;
        public float genes;
        public int faction;
        public int rarity;
        public int state;

        public int baseDamage;
        public float critDamageBoot;
        public float critRate;
        public float intel;
        public float farming;
        public int growth;

        public int selectedIndex;
        public bool isSupportMainHero;

        public int generation;

        public List<BodyPart> bodyParts;


        public HeroResponse(long id)
        {
            if (id is (long)default) return;

            const int partNumber = 6;

            bodyParts = new List<BodyPart>();
            var idString = id.ToString();

            for (int i = 0; i < partNumber; i++)
            {
                const int partIdLength = 2;

                var startPartIdIndex = i * partIdLength;
                var partIdString = idString.Substring(startPartIdIndex, partIdLength);

                bodyParts.Add(new BodyPart().SetPartId(partIdString));
            }
        }


        [System.Serializable]
        public class BodyPart
        {
            public int level;
            public int faction;
            public int skinId;
            public int id;
            public int rarity;

            public BodyPart SetPartId(string partId)
            {
                const int factionLocate = 0;
                const int skinIdLocate = 1;

                faction = int.Parse(partId[factionLocate].ToString());
                skinId = int.Parse(partId[skinIdLocate].ToString());

                return this;
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }


    public static class HeroResponseUtils
    {
        private static ServerServiceGroup ServerServices => NetworkService.Instance.services;
        private static GetHeroListServerService GetHeroListService => NetworkService.Instance.services.getHeroList;

        private const int BodyElementIndex = 4;
        private const int MainElementIndexLength = 1;


        public static HeroElement GetElement(long heroId)
        {
            HeroElement mainElement;

            try
            {
                mainElement = (HeroElement)int.Parse($"{heroId}".Substring(BodyElementIndex, MainElementIndexLength));
            }
            catch (ArgumentOutOfRangeException)
            {
                return HeroElement.None;
            }

            return mainElement;
        }


        public static long GetID(this HeroResponse heroResponse)
        {
            if (heroResponse?.bodyParts is null)
                return default;
            if (heroResponse.bodyParts.Count < 6)
                return default;

            return HeroID.ConvertFromBodyPartsResponse(heroResponse.bodyParts.ToArray());
        }

        public static HeroElement GetElement(this HeroResponse heroResponse)
        {
            return GetElement(heroResponse.GetID());
        }

        public static HeroState GetState(this HeroResponse heroResponse)
        {
            return (HeroState)heroResponse.state;
        }

        public static int GetRealHeroExp(this HeroResponse hero)
        {
            var expPerLevel = hero.expToUpLevel;

            var currentExp = hero.exp;
            var currentLevel = hero.level;

            const int firstLevel = 1;

            var realExp = expPerLevel is (int)default ? default : currentExp % expPerLevel;
            if (realExp is (int)default && currentExp > firstLevel) realExp = expPerLevel;

            return realExp;
        }

        public static float GetStatValue(this HeroResponse hero, BodyPartIndex bodyPartIndex)
        {
            return bodyPartIndex switch
            {
                BodyPartIndex.Face => hero.intel,
                BodyPartIndex.Hair => hero.growth,
                BodyPartIndex.Body => hero.baseDamage,
                BodyPartIndex.FrontHand => hero.farming,
                BodyPartIndex.Leg => hero.critDamageBoot + 1,
                BodyPartIndex.Deco => hero.critRate,
                _ => default
            };
        }

        public static bool IsSelectedIndex(this HeroResponse heroResponse)
        {
            var minSelectedIndex = GetHeroListService.minSelectedIndex;
            var maxSlotSelected = GetHeroListService.maxSlotSelected;
            var selectedIndex = heroResponse.selectedIndex;

            return selectedIndex >= minSelectedIndex && selectedIndex <= maxSlotSelected;
        }

        public static int GetBreedingTime(this HeroResponse heroResponse)
        {
            //breeding time start from 1 => +1
            return heroResponse.breedingLimit - heroResponse.breeding + 1;
        }

        public static HeroResponse AddFakeExp(this HeroResponse heroResponse, int expAdd)
        {
            var newExp = heroResponse.GetRealHeroExp() + expAdd;
            var isLevelUp = newExp >= heroResponse.expToUpLevel;

            if (isLevelUp)
            {
                var levelCanUp = newExp / heroResponse.expToUpLevel;
                var residualExp = newExp % heroResponse.expToUpLevel;

                var maxLevelCanUp = int.Parse(ServerServices.loadGameConfig.ResponseData.levelCapacityStar
                    .levelCapacityStar.Find(cap => Equals(int.Parse(cap.star), heroResponse.star)).level);
                var isLevelUpEqualMaxLevelCanUp = heroResponse.level + levelCanUp >= maxLevelCanUp;


                if (isLevelUpEqualMaxLevelCanUp)
                {
                    var totalExpCurrent = (heroResponse.level - 1) * heroResponse.expToUpLevel +
                                          heroResponse.GetRealHeroExp();
                    var expToMax = maxLevelCanUp * heroResponse.expToUpLevel - totalExpCurrent;

                    heroResponse.level = maxLevelCanUp;
                    heroResponse.exp = expAdd >= expToMax ? heroResponse.expToUpLevel : residualExp;
                }
                else
                {
                    heroResponse.level = heroResponse.level + levelCanUp;
                    heroResponse.exp = residualExp;
                }
            }
            else
            {
                heroResponse.exp = newExp;
            }

            return heroResponse;
        }


        public static HeroResponse GetHeroInfo(this List<HeroResponse> heroList, long heroId)
        {
            var queryHeroIndex = heroList.FindIndex(hero => hero.GetID() == heroId);
            var result = queryHeroIndex >= (int)default ? heroList[queryHeroIndex] : new HeroResponse(heroId);

            return result;
        }

        public static HeroResponse GetMainHero(this List<HeroResponse> heroList)
        {
            return heroList.Find(hero => hero.selectedIndex == GetHeroListService.mainHeroSelectedIndex);
        }

        public static List<HeroResponse> GetSubHeroes(this List<HeroResponse> heroList)
        {
            return heroList.FindAll(hero => hero.selectedIndex > GetHeroListService.mainHeroSelectedIndex);
        }

        public static List<HeroResponse> GetSelectedHeroes(this List<HeroResponse> heroList)
        {
            return heroList.Where(hero => hero.IsSelectedIndex()).OrderBy(hero => hero.selectedIndex).ToList();
        }

        public static long[] GetSelectedHeroIdDefaultValueIfNull(this List<HeroResponse> heroList)
        {
            var selectedHeroIds = heroList.GetSelectedHeroes().Select(hero => hero.GetID()).ToList();
            for (int i = selectedHeroIds.Count; i < GetHeroListService.maxSlotSelected; i++)
            {
                const int defaultValue = 0;
                selectedHeroIds.Add(defaultValue);
            }

            return selectedHeroIds.ToArray();
        }

        public static List<long> GetStandardBattleHeroId(this List<HeroResponse> heroList)
        {
            var selectedHeroIds = new List<long>();

            for (int i = 1; i <= GameManager.Instance.selectHeroConfig.StandardBattleHeroCount; i++)
            {
                var hero = heroList.Find(hero => hero.selectedIndex.Equals(i));
                selectedHeroIds.Add((hero ?? new HeroResponse(default)).GetID());
            }

            return selectedHeroIds;
        }
    }
}