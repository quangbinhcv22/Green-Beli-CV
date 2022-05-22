using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GEvent;
using GRBESystem.Definitions.BodyPart.Index;
using GRBESystem.UI.Screens.Popups.FusionResult.Widgets;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.FusionResult
{
    public class FusionResultHandler : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private HeroVisual newHeroVisual;
        [SerializeField] private HeroVisual oldHeroVisual;

        [SerializeField] private EnhancedScroller changedStatsScroller;
        [SerializeField] private ChangedBodyPartCellView changedBodyPartCellViewPrefab;

        private List<int> _changedStats = new List<int>();
        private HeroResponse _oldHero;
        private HeroResponse _newHero;

        private bool _isFirstUpdate;


        private void Awake()
        {
            changedStatsScroller.Delegate = this;
            EventManager.StartListening(EventName.Server.FusionSuccess, FusionOnLoad);
        }

        protected void OnEnable()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn ||
                FusionSuccessServerService.Response.IsError || _isFirstUpdate) return;
            
            FusionOnLoad();
        }

        private void FusionOnLoad()
        {
            _isFirstUpdate = true;
            
            _newHero = FusionSuccessServerService.Response.data.mainHero;
            newHeroVisual.UpdateView(_newHero);

            _oldHero = EventManager.GetData<HeroResponse>(EventName.PlayerEvent.LastFusionHero);
            oldHeroVisual.UpdateView(_oldHero);

            _changedStats = FusionSuccessServerService.Response.data.bodyPartLevelUp;
            changedStatsScroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _changedStats.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return changedBodyPartCellViewPrefab.GetComponent<RectTransform>().sizeDelta.y;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (ChangedBodyPartCellView)scroller.GetCellView(changedBodyPartCellViewPrefab);

            cellView.UpdateView((BodyPartIndex)_changedStats[cellIndex], new List<HeroResponse>() { _oldHero, _newHero });

            return cellView;
        }
    }
}