using System.Collections;
using System.Collections.Generic;
using GEvent;
using Extensions.Initialization.Request;
using TigerForge;
using UnityEngine;

namespace GRBESystem.Model.OptimizeHeroModel.Pool
{
    public class HeroModelPoolEventHandler : MonoBehaviour
    {
        [SerializeField] OptimizeHeroModel heroModelPrefab;

        private static readonly List<OptimizeHeroModel> PooledHeroModels = new List<OptimizeHeroModel>();

        void Awake()
        {
            EventManager.StartListening(EventName.Model.ShowHeroModel, () => StartCoroutine(ShowHeroModel()));
            EventManager.StartListening(EventName.Model.HideAllModels, HideAllModels);
            EventManager.StartListening(EventName.Server.Disconnect, HideAllModels);

            EventManager.EmitEventData(EventName.Model.HidingAllModels, data: false);
        }

        IEnumerator ShowHeroModel()
        {
            while (EventManager.GetData<bool>(EventName.Model.HidingAllModels))
            {
                yield return new WaitForSeconds(0.25f);
            }

            var showRequest = EventManager.GetData<ShowHeroModelRequest>(EventName.Model.ShowHeroModel);

            var modelToShow = GetPooledHeroModel(showRequest.heroId);

            var modelTransform = modelToShow.transform;
            modelTransform.position = showRequest.position;
            
            modelToShow.SetOrderPartSkeletons(showRequest.addOrderInLayer);
            
            var flipValue = showRequest.isFlip ? -1 : 1;
            var newScale = new Vector3(showRequest.scale.x * flipValue, showRequest.scale.y, showRequest.scale.z);
            modelToShow.SetScalePartSkeletons(newScale);
        }

        void HideAllModels()
        {
            EventManager.EmitEventData(EventName.Model.HidingAllModels, data: true);

            foreach (var heroModel in PooledHeroModels)
            {
                heroModel.gameObject.SetActive(false);
            }

            EventManager.EmitEventData(EventName.Model.HidingAllModels, data: false);
        }

        private OptimizeHeroModel GetPooledHeroModel(long heroId)
        {
            foreach (var heroModel in PooledHeroModels)
            {
                if (heroModel.HeroID == heroId && heroModel.isActiveAndEnabled == false)
                {
                    heroModel.gameObject.SetActive(true);
                    return heroModel;
                }
            }

            CreatePooledHeroModel(heroId);
            return GetPooledHeroModel(heroId);
        }

        private void CreatePooledHeroModel(long heroId)
        {
            var pooledHeroModel = Instantiate(heroModelPrefab, parent: this.transform);
            pooledHeroModel.HeroID = heroId;
            pooledHeroModel.gameObject.SetActive(false);

            PooledHeroModels.Add(pooledHeroModel);
        }

        public static Vector3 GetHandPositionHeroModel(long heroId)
        {
            foreach (var heroModel in PooledHeroModels)
            {
                if (heroModel.HeroID == heroId)
                {
                    return heroModel.HandPartModel.handTransform.position;
                }
            }

            return Vector3.zero;
        }
    }
}