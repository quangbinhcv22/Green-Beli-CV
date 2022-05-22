using System;
using System.Linq;
using GEvent;
using GRBEGame.UI.DataView;
using GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UI.Widget.HeroCard.Member;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Widget.HeroCard
{
    public class HeroVisual : MonoBehaviour, ICoreView<HeroResponse>
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<HeroResponse> _onUpdateView;

        [SerializeField] private UnityEvent onUpdateDefault;
        [SerializeField] private UnityEvent onUpdateView;
        
        private SelectHeroMode _selectHeroMode;

        public HeroResponse Hero { get; private set; }
        
        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.SelectHeroMode, UpdateViewBySelectMode);
        }
        
        public void UpdateView(HeroResponse heroResponse)
        {
            Hero = heroResponse;
            
            _onUpdateView?.Invoke(GetHeroResponseInfoByMode(heroResponse));
            onUpdateView?.Invoke();
        }

        public void UpdateView(long heroId)
        {
            var heroes = NetworkService.Instance.services.getHeroList.HeroResponses;
            var hero = heroes.GetHeroInfo(heroId);

            if (hero.GetID() is (long)default) UpdateDefault();
            else UpdateView(hero);
        }

        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
            onUpdateDefault?.Invoke();
        }
        
        private void UpdateViewBySelectMode()
        {
            var data = EventManager.GetData(EventName.PlayerEvent.SelectHeroMode);
            _selectHeroMode = data is null ? SelectHeroMode.Normal : (SelectHeroMode) data;
        }
        
        private HeroResponse GetHeroResponseInfoByMode(HeroResponse heroResponse)
        {
            return IsPvpMode() is false ? heroResponse : UpdateViewByGameMode(heroResponse);
        }
        
        private HeroResponse UpdateViewByGameMode(HeroResponse heroResponse)
        {
            var getHeroListService = NetworkService.Instance.services.getHeroList;
            
            if (_selectHeroMode is SelectHeroMode.HeroTeam &&
                IsPvpMode() && IsHasPvpTeam() && (HeroRole) heroResponse.selectedIndex != HeroRole.Main)
            {
                var hero = NetworkService.Instance.services.getHeroList.HeroResponses.Find(hero =>
                    hero.GetID() == heroResponse.GetID());
                
                return hero ?? heroResponse;
            }
            return getHeroListService.GetHeroPvpInfo(heroResponse);
        }
        
        private bool IsHasPvpTeam()
        {
            return NetworkService.Instance.services.getHeroList.HeroResponses.GetSelectedHeroes().Any();
        }
        
        private bool IsPvpMode()
        {
            var data = EventManager.GetData(EventName.Client.Battle.BattleMode);
            return (BattleMode?) data is BattleMode.PvP;
        }

        [Obsolete]
        public void AddCallBackUpdateView(IHeroVisualMember visualMember)
        {
            _onUpdateDefault += visualMember.UpdateDefault;
            _onUpdateView += visualMember.UpdateView;
        }


        public void AddCallBackUpdateView(IMemberView<HeroResponse> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }
}