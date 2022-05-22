using System.Linq;
using GEvent;
using GRBESystem.Definitions;
using Network.Messages.AttackBoss;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace Extensions.Battle
{
    public class ShootingBoss : MonoBehaviour
    {
        [SerializeField] private ShootingEffect shootingEffect;
        
        
        private void Awake()
        {
            EventManager.StartListening(EventName.ScreenEvent.Battle.HERO_ATTACK, StartShooting);
        }

        private void StartShooting()
        {
            var response = AttackBossServerService.Response;
            if(NetworkService.Instance.IsNotLogged() || response.IsError) return;
            if(response.data.IsRoundDraw())
                return;
            if(response.data.IsRoundLose())
            {
                var startGamePlayerInfo = StartGameServerService.Response.data.GetPlayerInfo(Owner.Opponent);
                var hero = startGamePlayerInfo.selectedHeros.First();
                shootingEffect.StartShooting(hero.GetElement(), BezierDirection.RightToLeft);
            }
            else
            {
                var startGamePlayerInfo = StartGameServerService.Response.data.GetPlayerInfo(Owner.Self);
                var hero = startGamePlayerInfo.selectedHeros.First();
                shootingEffect.StartShooting(hero.GetElement(), BezierDirection.LeftToRight);
            }
        }
    }
}
