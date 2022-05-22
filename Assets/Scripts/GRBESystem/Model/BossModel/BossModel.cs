using System.Collections;
using GEvent;
using Spine.Unity;
using TigerForge;
using UnityEngine;

namespace GRBESystem.Model.BossModel
{
    public class BossModel : MonoBehaviour
    {
        private const BossAnimationState ANIMATION_STATE_DEFAULT = BossAnimationState.Idle;

        public BossIdentity identity;

        [SerializeField, Space] private AnimationReferenceAsset idleAnimation;
        [SerializeField] private AnimationReferenceAsset getHurtAnimation;
        [SerializeField] private AnimationReferenceAsset dieAnimation;

        private SkeletonAnimation _skeletonAnimation;

        private void Awake()
        {
            this._skeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        private void OnEnable()
        {
            SetState(ANIMATION_STATE_DEFAULT);
            EventManager.StartListening(EventName.ScreenEvent.Battle.BOSS_ANIMATION, this.gameObject, AnimationFormEventManager);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.ScreenEvent.Battle.BOSS_ANIMATION, AnimationFormEventManager);
        }

        private void AnimationFormEventManager()
        {
            SetState(EventManager.GetData<BossAnimationState>(EventName.ScreenEvent.Battle.BOSS_ANIMATION));
        }

        private void SetState(BossAnimationState animationState)
        {
            switch (animationState)
            {
                case BossAnimationState.Idle:
                    StartCoroutine(SetAnimation(this.idleAnimation));
                    break;
                case BossAnimationState.GetHurt:
                    StartCoroutine(SetAnimation(this.getHurtAnimation, loop: false,
                        timeScale: 1.85f));
                    break;
                case BossAnimationState.Die:
                    StartCoroutine(SetAnimation(this.dieAnimation, loop: false, setActiveFalseWhenDone: true));
                    break;
                case BossAnimationState.PreDie:
                    StartCoroutine(SetAnimation(this.getHurtAnimation, loop: true));
                    break;
            }
        }

        private IEnumerator SetAnimation(AnimationReferenceAsset animationReference, bool loop = true,
            float timeScale = 1, bool setActiveFalseWhenDone = false)
        {
            this._skeletonAnimation.state.SetAnimation(0, animationReference, loop).TimeScale = timeScale;

            if (loop == true) yield break;

            var realDurationAnimation = this._skeletonAnimation.state.GetCurrent(0).Animation.Duration / timeScale;
            yield return new WaitForSeconds(realDurationAnimation);
            SetState(ANIMATION_STATE_DEFAULT);

            if (setActiveFalseWhenDone == false) yield break;
            this.gameObject.SetActive(false);
        }
    }
}