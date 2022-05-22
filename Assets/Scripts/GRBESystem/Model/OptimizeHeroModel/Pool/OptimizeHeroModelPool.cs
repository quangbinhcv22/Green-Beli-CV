using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using GRBESystem.Definitions.BodyPart.Index;
using GRBESystem.Model.OptimizeHeroModel.Widgets.MeshRendererReorder;
using GRBESystem.Model.OptimizeHeroModel.Widgets.Rescale;
using Spine.Unity;
using TigerForge;
using UnityEngine;

namespace GRBESystem.Model.OptimizeHeroModel.Pool
{
    public class OptimizeHeroModelPool : MonoBehaviour
    {
        [SerializeField] private List<SkeletonAnimation> bodyPartPrefabs;
        [SerializeField] private List<SkeletonAnimation> pooledBodyParts;

        void Awake()
        {
            EventManager.EmitEventData(EventName.Model.HeroModelPool, this);
        }

        public List<SkeletonAnimation> GetBodyPartModels(long heroId)
        {
            try
            {
                var heroIdString = heroId.ToString();
                var result = new List<SkeletonAnimation>();

                for (int i = 1; i <= 6; i++)
                {
                    var partName = $"{i}{heroIdString.Substring(2 * (i - 1), 2)}";

                    if (i == (int)BodyPartIndex.FrontHand)
                    {
                        var frontHandPartName = $"{partName}_0";
                        var backHandPartName = $"{partName}_1";

                        result.Add(GetPooledBodyPart(frontHandPartName));
                        result.Add(GetPooledBodyPart(backHandPartName));
                    }
                    else
                    {
                        result.Add(GetPooledBodyPart(partName));
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw new KeyNotFoundException();
            }
        }

        public void RecallBodyParts(List<SkeletonAnimation> bodyParts)
        {
            foreach (var bodyPart in bodyParts)
            {
                //bodyPart.transform.SetParent(this.transform);
                bodyPart.gameObject.SetActive(false);
            }
        }

        private SkeletonAnimation GetPooledBodyPart(string partName)
        {
            foreach (var pooledBodyPart in pooledBodyParts.Where(pooledBodyPart =>
                pooledBodyPart.name == partName && pooledBodyPart.gameObject.activeInHierarchy == false))
            {
                pooledBodyPart.gameObject.SetActive(true);
                return pooledBodyPart;
            }

            CreatePooledBodyPart(partName);

            return GetPooledBodyPart(partName);
        }

        private void CreatePooledBodyPart(string partName)
        {
            var result = GameObject.Instantiate(GetBodyPartPrefabs(partName), parent: transform);
            result.name = partName;
            result.gameObject.SetActive(false);
            result.gameObject.AddComponent<MeshRendererReorder>();

            pooledBodyParts.Add(result);
        }

        private SkeletonAnimation GetBodyPartPrefabs(string partName)
        {
            foreach (var bodyPart in bodyPartPrefabs.Where(bodyPart => bodyPart.name == partName))
            {
                return bodyPart;
            }

            throw new KeyNotFoundException();
        }
    }
}