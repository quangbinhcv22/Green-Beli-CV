using GEvent;
using GRBESystem.Definitions;
using GRBESystem.Entity.Element;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Widgets.SameBossElementBuff
{
    public class SameBossElementBuffPanel : MonoBehaviour
    {
        private LoginServerService LoginService => NetworkService.Instance.services.login;
        
        [SerializeField] private Owner owner;
        [SerializeField] private GameObject contentParent;
        [SerializeField] private TMP_Text buffText;
        [SerializeField] private string textFormat;

        private void Awake()
        {
            EventManager.StartListening(EventName.Server.StartGame, UpdateView);
        }

        private void OnEnable()
        {
            if(LoginService.IsNotLoggedIn) return;
            UpdateView();
        }

        private void UpdateView()
        {
            if(StartGameServerService.Response.IsError) return;
            
            var bossElement = (HeroElement) StartGameServerService.Data.boss.faction;
            var mainHeroElement = StartGameServerService.Data.GetPlayerInfo(owner).selectedHeros.GetMainHero().GetElement();

            var isSameElement = bossElement.Equals(mainHeroElement);
            
            contentParent.SetActive(isSameElement);
            if(contentParent.activeInHierarchy == false) return;

            buffText.SetText(string.Format(textFormat, GameManager.Instance.elementBuffConfig.sameBossElement));
        }
    }
}
