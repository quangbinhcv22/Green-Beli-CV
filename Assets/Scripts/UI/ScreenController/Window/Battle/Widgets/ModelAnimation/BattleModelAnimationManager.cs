using GEvent;
using GRBESystem.Model.BossModel;
using Network.Messages.AttackBoss;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using Service.Server.EndGame;
using TigerForge;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Widgets.ModelAnimation
{
    public class BattleModelAnimationManager : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.AttackBoss, OnAttackBoss);
            EventManager.StartListening(EventName.Server.EndGame, OnEndGame);
        }

        private void OnAttackBoss()
        {
            if (AttackBossServerService.Response.data.IsRoundDraw()) return;

            Invoke(nameof(PlayerWinHeroAnimationAttack), AttackBossServerService.delayResponseCallbackConfig.heroStartAttack);
            Invoke(nameof(PlayBossAnimationHurt), AttackBossServerService.delayResponseCallbackConfig.healthBarChange);
        }

        private void OnEndGame()
        {
            EventManager.EmitEventData(EventName.ScreenEvent.Battle.BOSS_ANIMATION, data: BossAnimationState.Die);
        }
        
        private void PlayerWinHeroAnimationAttack()
        {
            var playerTakeWinAddress = AttackBossServerService.Response.data.GetPlayerTakeWin().playerId;
            var mainHeroAttack = StartGameServerService.Response.data.GetMainHero(playerTakeWinAddress).GetID();

            PlayHeroAnimationAttack(mainHeroAttack);
        }
        
        private void PlayHeroAnimationAttack(long heroId)
        {
            EventManager.EmitEventData(EventName.ScreenEvent.Battle.HERO_ATTACK, data: $"{heroId}");
            EventManager.EmitEventData(EventName.ScreenEvent.Battle.PLAY_EFFECT_ATTACK, $"{heroId}");
        }

        private void PlayBossAnimationHurt()
        {
            EventManager.EmitEventData(EventName.ScreenEvent.Battle.BOSS_ANIMATION, data: BossAnimationState.GetHurt);
        }
    }
}