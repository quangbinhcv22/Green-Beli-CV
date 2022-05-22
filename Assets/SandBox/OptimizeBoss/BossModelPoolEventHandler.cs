using System.Collections.Generic;
using GEvent;
using GRBESystem.Model.BossModel;
using TigerForge;
using UI.ScriptableObject;
using UnityEngine;

namespace SandBox.OptimizeBoss
{
    public class BossModelPoolEventHandler : MonoBehaviour
    {
        [SerializeField] private BattleConfig battleConfig;
        
        private readonly List<BossModel> _bosses= new List<BossModel>();
        
        
        void Awake()
        {
            EventManager.StartListening(EventName.Model.ShowBossModel, ShowBossModel);
            EventManager.StartListening(EventName.Model.HideAllModels, HideAllModels);
        }
        
        private void InstanceBossModelIfNull(int identity = -1)
        {
            if (_bosses.Count == battleConfig.bossConfigs.Count) return;
            
            var bossConfig = 
                battleConfig.bossConfigs.Find(bossConfig => (int) bossConfig.model.identity == identity);
            if(bossConfig is null) return;
            
            var bossModel = _bosses.Find(boss => boss.identity == bossConfig.model.identity); 
            if(bossModel != null) return;
            
            _bosses.Add(Instantiate(bossConfig.model, transform));
        }
        
        private void HideAllModels()
        {
            foreach (var bossModelPrefab in battleConfig.bossConfigs)
                bossModelPrefab.model.gameObject.SetActive(false);
        }

        private void ShowBossModel()
        {
            var identity = EventManager.GetData<int>(EventName.Model.ShowBossModel);
            var bossConfig = battleConfig.bossConfigs.Find(bossConfig => (int) bossConfig.model.identity == identity);

            if (bossConfig == null) return;

            InstanceBossModelIfNull(identity);
            _bosses.ForEach(boss => {
                boss.gameObject.SetActive(boss.identity == bossConfig.model.identity);
            });
        }
    }
}
