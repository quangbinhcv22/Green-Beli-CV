using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GEvent;
using GRBESystem.Definitions;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Widgets.SubHeroAvatar
{
    [DefaultExecutionOrder(500)]
    public class BattleSubHeroAvatars : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private Owner owner;

        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private HeroAvatarCellView cellViewTemplate;

        private List<HeroResponse> _subHeroIds = new List<HeroResponse>();


        private void Awake()
        {
            scroller.Delegate = this;
        }

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.StartGame, UpdateView);
        }

        private void UpdateView()
        {
            if (StartGameServerService.Response.IsError) return;

            _subHeroIds = StartGameServerService.Data.GetPlayerInfo(owner).selectedHeros.GetSubHeroes();
            scroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _subHeroIds.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 140f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (HeroAvatarCellView)scroller.GetCellView(cellViewTemplate);
            cellView.UpdateView(_subHeroIds[dataIndex].GetID());

            return cellView;
        }
    }
}