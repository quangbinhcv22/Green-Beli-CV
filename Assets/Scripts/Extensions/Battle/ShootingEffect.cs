using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BezierSolution;
using GEvent;
using GRBESystem.Entity.Element;
using TigerForge;
using UnityEngine;

namespace Extensions.Battle
{
    [RequireComponent(typeof(BezierWalkerWithTime))]
    public class ShootingEffect : MonoBehaviour
    {
        [SerializeField] private float delayTimeReplace = 0.3f;

        [SerializeField, Space] private List<BattleElementTagParticle> particleSystemsShootPrefabs;
        [SerializeField] private List<BattleElementTagParticle> particleSystemGetHitPrefabs;

        [SerializeField, Space] private List<AudioClip> getHitSoundEffects;

        [SerializeField, Space] private BezierSpline bezierSplineRightToLeft;
        [SerializeField] private BezierSpline bezierSplineLeftToRight;
        
        private Vector3 _positionStart;
        private BezierWalkerWithTime _bezierWalkerWithTime;

        private readonly Dictionary<HeroElement, ParticleSystem> _particleSystemShoots =
            new Dictionary<HeroElement, ParticleSystem>();

        private readonly Dictionary<HeroElement, ParticleSystem> _particleSystemHits =
            new Dictionary<HeroElement, ParticleSystem>();

        void AddInstantiateToDictionary(Dictionary<HeroElement, ParticleSystem> dictionary, params BattleElementTagParticle[] prefabs)
        {
            foreach (var prefab in prefabs)
            {
                var particleInstance = Instantiate(prefab, this.transform);
                particleInstance.gameObject.SetActive(false);
                
                dictionary.Add(prefab.elementTag, particleInstance.GetComponent<ParticleSystem>());
            }
        }

        public ParticleSystem GetParticleSystemShoot(HeroElement element)
        {
            return this._particleSystemShoots[element];
        }
        
        public ParticleSystem GetParticleSystemGetHit(HeroElement element)
        {
            return this._particleSystemHits[element];
        }
        
        public AudioClip GetSoundEffectGetHit(HeroElement element)
        {
            return getHitSoundEffects.First();
        }

        private void Awake()
        {
            this._positionStart = transform.position;

            this._bezierWalkerWithTime = GetComponent<BezierWalkerWithTime>();
            this._bezierWalkerWithTime.spline = this.bezierSplineRightToLeft;
            this._bezierWalkerWithTime.enabled = false;

            CreateParticleList();
            // EventManager.StartListening(EventName.ScreenEvent.Battle.HERO_ATTACK, StartRandomShooting);
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.A)) 
            //     StartRandomShooting();
        }

        private void CreateParticleList()
        {
            AddInstantiateToDictionary(this._particleSystemShoots, this.particleSystemsShootPrefabs.ToArray());
            AddInstantiateToDictionary(this._particleSystemHits, this.particleSystemGetHitPrefabs.ToArray());
        }

        private void DirectionBezierTime(BezierDirection bezierDirection)
        {
            this._bezierWalkerWithTime.spline = bezierDirection == BezierDirection.RightToLeft
                ? this.bezierSplineRightToLeft
                : this.bezierSplineLeftToRight;

            this._bezierWalkerWithTime.enabled = true;
        }
        
        public void StartRandomShooting()
        {
            HeroElement heroElement = (HeroElement)Random.Range(1, 6);
            BezierDirection bezierDirection = (BezierDirection)Random.Range(0, 2);
            StartShooting(heroElement, bezierDirection);
        }

        public void StartShooting(HeroElement heroElement, BezierDirection bezierDirection)
        {
            DirectionBezierTime(bezierDirection);

            transform.position = this._bezierWalkerWithTime.spline.GetPoint(0);
            
            var particle = GetParticleSystemShoot(heroElement);
            particle.gameObject.SetActive(true);
            
            StartCoroutine(ResetPosition(heroElement, bezierDirection));
        }
        
        IEnumerator ResetPosition(HeroElement heroElement, BezierDirection bezierDirection)
        {
            yield return new WaitForSeconds(this._bezierWalkerWithTime.travelTime);
            
            GetParticleSystemShoot(heroElement).gameObject.SetActive(false);
            GetParticleSystemGetHit(heroElement).gameObject.SetActive(true);

            EmitEventGetHit(heroElement);
            
            yield return new WaitForSeconds(this.delayTimeReplace);
            
            GetParticleSystemGetHit(heroElement).gameObject.SetActive(false);

            const float DEFAULT_NORMALIZED_T_WALKER_WITH_TIME = 0;
            this._bezierWalkerWithTime.NormalizedT = DEFAULT_NORMALIZED_T_WALKER_WITH_TIME;
            this._bezierWalkerWithTime.enabled = false;

            transform.position = this._positionStart;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        private void EmitEventGetHit(HeroElement heroElement)
        {
            EventManager.EmitEventData(EventName.WidgetEvent.PLAY_SOUND_EFFECT, GetSoundEffectGetHit(heroElement));
        }
    }

    public enum BezierDirection
    {
        RightToLeft = 0,
        LeftToRight = 1
    }
}
