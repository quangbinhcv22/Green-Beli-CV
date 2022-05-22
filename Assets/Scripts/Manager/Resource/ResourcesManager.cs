using System.Collections.Generic;
using System.Linq;
using GEvent;
using Manager.Resource.Assets;
using Pattern;
using TigerForge;
using UnityEngine;

namespace Manager.Resource
{
    public class ResourcesManager : Singleton<ResourcesManager>
    {
        private static readonly List<DecoGameObject> s_decoPrefabsInstantiate = new List<DecoGameObject>();

        [SerializeField, Space] private List<DecoGameObject> decoPrefabs;


        private void Awake()
        {
            EventManager.StartListening(EventName.WidgetEvent.CHANGE_THEME, OnChaneTheme);
            InitializationFirstTime();
        }


        private void InitializationFirstTime()
        {
            foreach (var decoPrefabInstantiate in this.decoPrefabs.Select(decoPrefab =>
                Instantiate(decoPrefab, Instance.transform)))
            {
                decoPrefabInstantiate.gameObject.SetActive(false);
        
                s_decoPrefabsInstantiate.Add(decoPrefabInstantiate);
            }
        }
        

        private static void OnChaneTheme()
        {
            var theme = EventManager.GetData<Theme>(EventName.WidgetEvent.CHANGE_THEME);
            var decoIds = theme?.decoIds;

            foreach (var decoPrefabInstantiate in s_decoPrefabsInstantiate)
            {
                decoPrefabInstantiate.gameObject.SetActive(false);
            }

            if (decoIds == null) return;

            foreach (var decoPrefabInstantiate in from decoId in decoIds
                from decoPrefabInstantiate in ResourcesManager.s_decoPrefabsInstantiate
                where decoPrefabInstantiate.decoGameObjectId == decoId
                select decoPrefabInstantiate)
            {
                decoPrefabInstantiate.gameObject.SetActive(true);
            }
        }
    }
}