using System.Linq;
using GEvent;
using QuangBinh.Reflect;
using TigerForge;
using UnityEngine;

namespace Calculate.Widgets.MutualSubHeroes
{
    public class MutualSubHeroesActivator : MonoBehaviour
    {
        [SerializeField] private ActiveReflector activeReflector;

        private void Awake()
        {
            activeReflector.SetCondition(HaveSubHero);
            EventManager.StartListening(EventName.PlayerEvent.BattleHeroes, OnSelectBattleHero);
        }

        private bool HaveSubHero()
        {
            return MutualCalculator.GetSudHeroIds() is null == false && MutualCalculator.GetSudHeroIds().Any();
        }

        private void OnSelectBattleHero()
        {
            activeReflector.Reflect();
        }
    }
}