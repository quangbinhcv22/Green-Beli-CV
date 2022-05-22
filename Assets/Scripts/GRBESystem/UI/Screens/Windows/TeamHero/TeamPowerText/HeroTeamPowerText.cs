using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.TeamHero.TeamPowerText
{
    [RequireComponent(typeof(TMP_Text))]
    public class HeroTeamPowerText : MonoBehaviour
    {
        private const int DefaultPower = 0;

        private static ServerServiceGroup ServerServices => NetworkService.Instance.services;

        [SerializeField] private TMP_Text powerText;

        private void Awake()
        {
            EventManager.StartListening(EventName.Server.GetListHero, OnGetListHeroResponse);
            EventManager.StartListening(EventName.Server.CalculateHeroTeamPower, OnCalculateHeroTeamPowerResponse);
        }

        private void OnEnable()
        {
            OnGetListHeroResponse();
        }

        private void OnGetListHeroResponse()
        {
            var battleModeData = EventManager.GetData(EventName.Client.Battle.BattleMode);
            var selectedHero = ServerServices.getHeroList.HeroResponses.GetSelectedHeroes();

            if (selectedHero.Any() && HaveSelectMainHero())
            {
                ServerServices.calculateHeroTeamPower.SendRequest(selectedHero.Select(hero => hero.GetID()).ToList(),
                    battleModeData is null || (BattleMode) battleModeData != BattleMode.PvP
                        ? GameMode.PVE
                        : GameMode.PVP);
                return;
            }

            SetPowerText(DefaultPower);
        }

        private void OnCalculateHeroTeamPowerResponse()
        {
            SetPowerText(ServerServices.calculateHeroTeamPower.Result);
        }

        private void SetPowerText(int power)
        {
            powerText.SetText(HaveSelectMainHero() ? $"{power:N0}" : "0");
        }

        private bool HaveSelectMainHero()
        {
            var battleHeroes = EventManager.GetData(EventName.PlayerEvent.BattleHeroes);
            if (battleHeroes is null) return false;

            var selectedHeroes = EventManager.GetData<List<long>>(EventName.PlayerEvent.BattleHeroes);
            return GameManager.Instance.selectHeroConfig.HaveMainHero(selectedHeroes);
        }

        private void OnValidate()
        {
            powerText ??= GetComponent<TMP_Text>();
        }
    }
}