using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace UI.ScreenController.Window.Battle
{
    public class MainBattleHeroSetUpHandler : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.StartGame, OnStartGame);
        }
    
        private void OnStartGame()
        {
            var mainHeroOldAble = NetworkService.Instance.services.getHeroList.HeroResponses.GetMainHero();
            EventManager.EmitEventData(EventName.Client.MainHeroOldable, mainHeroOldAble);
        }
    }
}
