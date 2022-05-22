using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using GRBESystem.UI.Screens.Windows.Loading;
using GRBESystem.UI.Screens.Windows.MatchPve.Widgets;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using QuangBinh.UIFramework.Screen;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.MatchPvp
{
    public class StartPvpGameHandler : MonoBehaviour
    {
        [SerializeField] private MatchPlayerInfoViewer selfInfoViewer;
        [SerializeField] private MatchPlayerInfoViewer opponentInfoViewer;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.PlayPvp, () =>
            {
                opponentInfoViewer.ResetViewDefault();
                UpdateTeamPvpPower();
            });
        }

        private void OnEnable()
        {
            if(NetworkService.Instance.IsNotLogged()) return;
            
            opponentInfoViewer.ResetViewDefault();
            EventManager.StartListening(EventName.Server.StartGame, OnStartGame);
            UpdateTeamPvpPower();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.StartGame, OnStartGame);
            EventManager.StopListening(EventName.Server.CalculateHeroTeamPower, UpdateSelfInfoViewer);
        }

        private void UpdateTeamPvpPower()
        {
            var selectedHero = NetworkService.Instance.services.getHeroList.HeroResponses.GetSelectedHeroes();
            
            EventManager.StartListening(EventName.Server.CalculateHeroTeamPower, UpdateSelfInfoViewer);
            NetworkService.Instance.services.calculateHeroTeamPower.SendRequest(
                selectedHero.Select(hero => hero.GetID()).ToList(), GameMode.PVP);
        }
        
        private void UpdateSelfInfoViewer()
        {
            var selfInfo = NetworkService.Instance.services.login.MessageResponse.data;
            var selectedHeroes = EventManager.GetData<List<long>>(EventName.PlayerEvent.BattleHeroes);
            var heroTeamPower = NetworkService.Instance.services.calculateHeroTeamPower.Result;

            selfInfoViewer.UpdateView(selectedHeroes, selfInfo.id, heroTeamPower);
        }

        private void OnStartGame()
        {
            var data = EventManager.GetData(EventName.Client.Battle.BattleMode);
            if(data is null) return;
            var battleMode = (BattleMode) data;
            if(battleMode is BattleMode.PvE) return;

            var opponentInfo = StartGameServerService.Response.data.GetOpponentInfo();
            var opponentSelectedHeroIds = opponentInfo.selectedHeros.Select(hero => hero.GetID()).ToList();
            opponentInfoViewer.UpdateView(opponentSelectedHeroIds, opponentInfo.id, opponentInfo.heroTeamPower);
        }
    }
}
