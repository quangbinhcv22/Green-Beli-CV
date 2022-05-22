using System;
using System.Linq;
using Config.ArtSet;
using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using Service.Server.EndGame;
using TigerForge;
using TMPro;
using UI.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ScreenController.Popup.EndGame.Widget
{
    public class ExpHeroProgressPanel : MonoBehaviour
    {
        [SerializeField] private HeroRarityArtSet heroRarityArtSet;
        [SerializeField, Space] private ProcessBar expBar;
        [SerializeField] private Image backgroundLevel;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text addScoreText;

        private void Awake()
        {
            EventManager.StartListening(EventName.Server.EndGame, UpdateView);
        }

        void OnEndGame()
        {
            EventManager.StartListening(EventName.Server.GetListHero, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetListHero, UpdateView);
        }

        private void UpdateView()
        {
            var mainHero = EventManager.GetData<HeroResponse>(EventName.Client.MainHeroOldable)
                .AddFakeExp(EndGameServerService.GetClientData().GetSelfInfo().rewardExp);

            backgroundLevel.sprite = heroRarityArtSet.GetRaritySprite(mainHero.rarity);

            levelText.text = GetFormattedLevelString(mainHero.level);
            expBar.UpdateView(mainHero.GetRealHeroExp(), mainHero.expToUpLevel);

            addScoreText.text = GetFormattedExpString(EndGameServerService.GetClientData().GetSelfInfo().rewardExp);
        }


        private string GetFormattedLevelString(int number)
        {
            return $"{number:N0}";
        }

        private string GetFormattedExpString(int number)
        {
            return $"+ {number:N0}  EXP";
        }
    }
}