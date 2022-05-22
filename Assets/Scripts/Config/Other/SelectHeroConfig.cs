using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Config.Other
{
    [CreateAssetMenu(fileName = FileName, menuName = MenuName)]
    public class SelectHeroConfig : ScriptableObject
    {
        private const string FileName = nameof(SelectHeroConfig);
        private const string MenuName = "ScriptableObject/OtherConfig/SelectHero";

        public long nonHeroId;
        [SerializeField] private int standardBattleHeroCount;
        [SerializeField] private int standardBreedingHeroCount;
        [SerializeField] private int standardFusionHeroCount;

        public int StandardBattleHeroCount => standardBattleHeroCount;
        public int StandardBreedingHeroCount => standardBreedingHeroCount;
        public int StandardFusionHeroCount => standardFusionHeroCount;


        public bool IsNotHero(long heroId)
        {
            return heroId.Equals(nonHeroId);
        }

        public bool IsValidHeroID(long heroId)
        {
            return IsNotHero(heroId).Equals(false);
        }

        public List<long> CreateNonHeroList(int length)
        {
            var noneHeroList = new List<long>();

            for (int i = 0; i < length; i++)
            {
                noneHeroList.Add(nonHeroId);
            }

            return noneHeroList;
        }

        public List<long> ReplaceNoneHeroBy(IEnumerable<long> heroIds, long newHeroId)
        {
            var heroIdList = heroIds.ToList();
            if (heroIdList.Contains(nonHeroId) == false) return heroIdList.ToList();

            var cloneHeroIds = new List<long>(heroIdList);
            cloneHeroIds[cloneHeroIds.FindIndex(heroId => heroId.Equals(nonHeroId))] = newHeroId;

            return cloneHeroIds;
        }

        public bool IsValidSelectedHeroes(List<long> selectedEmptiableHeroIds)
        {
            return HaveMainHero(selectedEmptiableHeroIds) || IsClearAllHero(selectedEmptiableHeroIds);
        }

        public bool IsFullSelection(List<long> selectedHeroId, int fullCount)
        {
            return selectedHeroId.Count(IsValidHeroID).Equals(fullCount);
        }
        
        public bool HaveMainHero(List<long> selectedEmptiableHeroIds)
        {
            const int mainHeroIndex = 0;
            return selectedEmptiableHeroIds.Count > mainHeroIndex && IsValidHeroID(selectedEmptiableHeroIds[mainHeroIndex]);
        }
        
        public bool IsClearAllHero(List<long> selectedHeroIds)
        {
            return selectedHeroIds.All(IsNotHero);
        }
    }
}