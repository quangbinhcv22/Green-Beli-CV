using System.Collections.Generic;
using GEvent;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.Breeding.Config
{
    [CreateAssetMenu(fileName = "BreedingConfig", menuName = "ScriptableObjects/General/BreedingConfig")]
    public class BreedingConfig : ScriptableObject
    {
        public int heroesNumberRequire;
        public int minimumLevelRequire;
        public long noneHeroId;

        public List<long> NonHeroIds => new List<long> {noneHeroId, noneHeroId};
    }
}