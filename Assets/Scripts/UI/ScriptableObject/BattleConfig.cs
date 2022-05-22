using System.Collections.Generic;
using GRBESystem;
using GRBESystem.Entity;
using GRBESystem.Entity.Element;
using GRBESystem.Model.BossModel;
using Manager.Resource;
using Manager.Resource.Assets;
using UnityEngine;

namespace UI.ScriptableObject
{
    [System.Serializable]
    public class BossConfig
    {
        public BossIdentity identity;
        public HeroElement faction;

        [Space] public BossModel model;
    }


    [CreateAssetMenu(fileName = "BattleConfig", menuName = "ScriptableObjects/BattleConfig", order = 1)]
    public class BattleConfig : UnityEngine.ScriptableObject
    {
        public List<BossConfig> bossConfigs;
    }
}