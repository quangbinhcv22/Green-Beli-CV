using System.Collections.Generic;
using GEvent;
using Extensions.Battle;
using Extensions.Initialization;
using GRBESystem.Entity;
using GRBESystem.Model.OptimizeHeroModel.Pool;
using Manager.Resource;
using Network.Messages.GetHeroList;
using TigerForge;
using UnityEngine;

namespace UI.Effect.Battle
{
    public class EffectBattleHandler : MonoBehaviour
    {
        private static readonly List<string> s_setActiveTrueEventNames;
        private static readonly List<string> s_setActiveFalseEventNames;


        [SerializeField] private float delayShooting;

        [SerializeField, Space] private ShootingEffect shooter;

        [SerializeField, Space] private Transform effectPointFromLeft;
        [SerializeField] private Transform effectPointFromRight;


        static EffectBattleHandler()
        {
            s_setActiveTrueEventNames = new List<string>() { EventName.ScreenEvent.Battle.ENTER_SCREEN, };
            s_setActiveFalseEventNames = new List<string>() { EventName.ScreenEvent.Battle.EXIT_SCREEN, };
        }


        private void Awake()
        {
            RegisterEventSetActive(s_setActiveTrueEventNames, s_setActiveFalseEventNames);
            EventManager.StartListening(EventName.ScreenEvent.Battle.PLAY_EFFECT_ATTACK, Shooting);
        }

        private void Shooting()
        {
            var heroAttackId =long.Parse(EventManager.GetData<string>(EventName.ScreenEvent.Battle.PLAY_EFFECT_ATTACK));
            var handPosition = HeroModelPoolEventHandler.GetHandPositionHeroModel(heroAttackId);

            FindNearestStartingEffectPoint(handPosition).position = handPosition;
            shooter.StartShooting(HeroResponseUtils.GetElement(heroAttackId), GetBezierDirectionShooting(handPosition));
        }

        private Transform FindNearestStartingEffectPoint(Vector3 heroHandPosition)
        {
            var a = Vector3.Distance(heroHandPosition, effectPointFromLeft.position);
            var b = Vector3.Distance(heroHandPosition, effectPointFromRight.position);

            return a > b ? effectPointFromRight : effectPointFromLeft;
        }

        private BezierDirection GetBezierDirectionShooting(Vector3 heroHandPosition)
        {
            return FindNearestStartingEffectPoint(heroHandPosition) == effectPointFromLeft
                ? BezierDirection.LeftToRight
                : BezierDirection.RightToLeft;
        }


        private void RegisterEventSetActive(List<string> setActiveTrueEventNames, List<string> setActiveFalseEventNames)
        {
            foreach (string eventName in setActiveTrueEventNames)
            {
                EventManager.StartListening(eventName, callBack: () => SetActive(true));
            }

            foreach (string eventName in setActiveFalseEventNames)
            {
                EventManager.StartListening(eventName, callBack: () => SetActive(false));
            }
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}