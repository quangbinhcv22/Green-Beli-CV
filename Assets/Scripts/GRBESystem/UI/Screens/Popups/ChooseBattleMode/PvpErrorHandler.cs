using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using GRBESystem.UI.Screens.Windows.MatchPvp;
using Network.Controller;
using Network.Messages;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using QuangBinh.UIFramework.Screen;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UI.Widget.Toast;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.ChooseBattleMode
{
    public class PvpErrorHandler : MonoBehaviour
    {
        [SerializeField] private string heroHasChanged;
        [SerializeField] [Space] private string noMatchingCompetitorError;
        [SerializeField] private ToastData.ToastLevel noMatchingCompetitorErrorToastLevel;
        [SerializeField] [Space] private string competitorOutBattle;
        [SerializeField] private ToastData.ToastLevel competitorOutBattleToastLevel;
        [SerializeField] [Space] private string nextPvpBattleByTicket;
        [SerializeField] private ToastData.ToastLevel nextPvpBattleByTicketToastLevel;

        private bool _isStartGame;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.CancelGame, OnCancelGame);
            EventManager.StartListening(EventName.Server.CancelPlayPvp, OnCancelPlayPvp);
            EventManager.StartListening(EventName.Server.HeroHasChanged, OnHeroHasChanged);

            EventManager.StartListening(EventName.Server.StartGame, () => { _isStartGame = true; });

            EventManager.StartListening(EventName.Client.Battle.NextPvpBattleByTicketError,
                () => StartCoroutine(NextPvpBattleByTicketError()));
        }

        private void OnEnable()
        {
            _isStartGame = false;
        }
        
        private void OnCancelGame()
        {
            var data = EventManager.GetData(EventName.Client.Battle.BattleMode);
            var boxedBattleMode = data is null ? BattleMode.PvE : (BattleMode)data;
            
            if(boxedBattleMode is BattleMode.PvE) return;
            EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, new ToastData()
            {
                content = competitorOutBattle,
                toastLevel = competitorOutBattleToastLevel,
            });
        }

        private void OnHeroHasChanged()
        {
            EventManager.StartListening(EventName.Server.GetListHero, OnUpdateHeroList);
        }

        private void OnUpdateHeroList()
        {
            EventManager.StopListening(EventName.Server.GetListHero, OnUpdateHeroList);
            
            var selectedHeroes = EventManager.GetData<List<long>>(EventName.PlayerEvent.BattleHeroes);
            var mainHero = NetworkService.Instance.services.getHeroList.HeroResponses.GetMainHero();

            if (selectedHeroes.First() == mainHero.GetID() && mainHero.GetID() != default || _isStartGame) return;

            Message.Instance().SetId(EventName.Server.CancelPlayPvp).Send();
            WebSocketController.Instance.SendFakeReceivedMessage(heroHasChanged);
        }

        private void OnCancelPlayPvp()
        {
            if (CancelPlayPvpServerService.Response.IsError ||
                string.IsNullOrEmpty(CancelPlayPvpServerService.Response.data.info)) return;
            
            EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, new ToastData
            {
                content = noMatchingCompetitorError,
                toastLevel = noMatchingCompetitorErrorToastLevel,
            });
        }

        private const float ScreenOpenDelay = 0.5f;
        
        private IEnumerator NextPvpBattleByTicketError()
        {
            EventManager.EmitEventData(EventName.UI.RequestScreen(), new ScreenRequest
            {
                action = ScreenAction.Open,
                screenID = ScreenID.PvPInfoPopup,
            });
            EventManager.EmitEventData(EventName.Client.Battle.BattleMode, BattleMode.PvP);
            
            yield return new WaitForSeconds(ScreenOpenDelay);
            
            EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, new ToastData
            {
                content = nextPvpBattleByTicket,
                toastLevel = nextPvpBattleByTicketToastLevel,
            });
        }
    }
}
