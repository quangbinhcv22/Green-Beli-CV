using System;
using System.Collections;
using System.Collections.Generic;
using GEvent;
using GRBESystem.Model.HeroModel;
using GRBESystem.Model.OptimizeHeroModel.Pool;
using GRBESystem.Model.OptimizeHeroModel.Widgets.BodyParts.Hand;
using GRBESystem.Model.OptimizeHeroModel.Widgets.MeshRendererReorder;
using Spine.Unity;
using TigerForge;
using UnityEngine;

namespace GRBESystem.Model.OptimizeHeroModel
{
    public class OptimizeHeroModel : MonoBehaviour
    {
        [SerializeField, Space] private AnimationReferenceAsset idleAnimation;
        [SerializeField] private AnimationReferenceAsset attackAnimation;
        [SerializeField] private HeroAnimationState heroAnimationStateDefault = HeroAnimationState.Idle;

        [SerializeField, Space] private List<SkeletonAnimation> allBodyPartSkeletonAnimations;
        [SerializeField] private Transform bodyPartsParent;

        [HideInInspector] public HandPartModel HandPartModel { get; private set; }


        private long _heroID;

        public long HeroID
        {
            get => _heroID;
            set
            {
                _heroID = value;
                name = _heroID.ToString();

                ChangeSkin();
            }
        }


        private void OnEnable()
        {
            ChangeSkin();
            EventManager.StartListening(EventName.ScreenEvent.Battle.HERO_ATTACK, OnHeroAttack);
        }

        private void OnHeroAttack()
        {
            if (EventManager.GetData<string>(EventName.ScreenEvent.Battle.HERO_ATTACK) == name)
            {
                SetState(HeroAnimationState.Attack);
            }
        }
        

        private void OnDisable()
        {
            RecallPooledBodyPart();
        }


        private void ChangeSkin()
        {
            try
            {
                RecallPooledBodyPart();

                var heroModelPool = EventManager.GetData<OptimizeHeroModelPool>(EventName.Model.HeroModelPool);
                allBodyPartSkeletonAnimations.AddRange(heroModelPool.GetBodyPartModels(_heroID));

                foreach (var part in allBodyPartSkeletonAnimations)
                {
                    Transform partTransform = part.transform;

                    partTransform.SetParent(this.bodyPartsParent);
                    partTransform.localPosition = Vector3.zero;
                }

                HandPartModel = bodyPartsParent.GetComponentInChildren<HandPartModel>();

                SetState(HeroAnimationState.Idle);
            }
            catch (KeyNotFoundException)
            {
                // hero id is not valid
            }
        }

        private void RecallPooledBodyPart()
        {
            try
            {
                var heroModelPool = EventManager.GetData<OptimizeHeroModelPool>(EventName.Model.HeroModelPool);

                heroModelPool.RecallBodyParts(allBodyPartSkeletonAnimations);
                allBodyPartSkeletonAnimations = new List<SkeletonAnimation>();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void SetState(HeroAnimationState heroAnimationState)
        {
            switch (heroAnimationState)
            {
                case HeroAnimationState.Idle:
                    StartCoroutine(SetAnimation(this.idleAnimation));
                    break;
                case HeroAnimationState.Attack:
                    StartCoroutine(SetAnimation(this.attackAnimation, loop: false));
                    break;
            }
        }

        private IEnumerator SetAnimation(AnimationReferenceAsset animationReference, bool loop = true,
            float timeScale = 1)
        {
            foreach (var bodyPartSkeletonAnimation in this.allBodyPartSkeletonAnimations)
            {
                bodyPartSkeletonAnimation.state.SetAnimation(0, animationReference, loop).TimeScale = timeScale;
            }

            if (loop == true) yield break;

            var realDurationAnimation =
                allBodyPartSkeletonAnimations[0].state.GetCurrent(0).Animation.Duration / timeScale;
            yield return new WaitForSeconds(realDurationAnimation);
            SetState(this.heroAnimationStateDefault);
        }

        public void SetOrderPartSkeletons(int orderAdd)
        {
            foreach (var bodyPart in allBodyPartSkeletonAnimations)
            {
                bodyPart.GetComponent<MeshRendererReorder>().AddOrder(orderAdd);
            }
        }

        public void SetScalePartSkeletons(Vector3 scale)
        {
            foreach (var bodyPart in allBodyPartSkeletonAnimations)
            {
                bodyPart.transform.localScale = scale;
            }
        }
    }
}