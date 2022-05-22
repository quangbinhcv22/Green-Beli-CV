using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using GEvent;
using GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter;
using GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter.Condition;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Panels.SelectHero.New
{
    public class NewSelectHeroPanel : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private HeroToSelectSlot heroToSelectSlotPrefab;

        [SerializeField, Space] private HeroPageActiveReflector.HeroPageActiveReflector previousPageSprite;
        [SerializeField] private HeroPageActiveReflector.HeroPageActiveReflector nextPageSprite;

        private List<HeroResponse> _heroList;


        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.SelectHeroMode, UpdateHeroList);
            EventManager.StartListening(EventName.PlayerEvent.BreedingHeroes, UpdateHeroList);
            EventManager.StartListening(EventName.PlayerEvent.FusionHeroes, UpdateHeroList);
            EventManager.StartListening(EventName.PlayerEvent.RestoreLevelsHero, UpdateHeroList);
        }

        private void OnEnable()
        {
            if (NetworkService.Instance.IsNotLogged()) return;

            UpdateHeroList();
            EventManager.StartListening(EventName.Server.GetListHero, UpdateHeroList);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetListHero, UpdateHeroList);
        }

        private void UpdateHeroList()
        {
            if (NetworkService.Instance is null) return;
            var notFilterHeroes = NetworkService.Instance.services.getHeroList.HeroResponses;

            IHeroSelectSlotInteractableCondition interactableCondition = SelectMode() switch
            {
                SelectHeroMode.Breeding => new InteractableBasedBreedingCondition(),
                SelectHeroMode.Fusion => new InteractableBasedFusionCondition(),
                SelectHeroMode.RestoreLevels => new InteractableBasedRestoreLevelsCondition(),
                _ => new InteractableAlwaysTrueCondition()
            };

            _heroList = notFilterHeroes.Where(interactableCondition.CanInteractable).ToList();

            ReloadData();
        }

        private void UpdateViewPageSprite()
        {
            var scrollPosition = (int) scroller.ScrollPosition;
            previousPageSprite.UpdateView(scrollPosition > 0);
            nextPageSprite.UpdateView(scrollPosition < scroller.ScrollSize);
        }

        private SelectHeroMode SelectMode()
        {
            return EventManager.GetData(EventName.PlayerEvent.SelectHeroMode) is SelectHeroMode selectMode
                ? selectMode
                : SelectHeroMode.Normal;
        }


        private void ReloadData()
        {
            if (scroller.Delegate is null)
            {
                scroller.Delegate = this;
            }
            else
            {
                var scrollPositionFactor = scroller.ScrollPosition / scroller.ScrollSize;
                scroller.ReloadData(((int) scroller.ScrollSize).Equals(default) ? default : scrollPositionFactor);
            }
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            UpdateViewPageSprite();
            return _heroList.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 210f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (HeroToSelectSlot) scroller.GetCellView(heroToSelectSlotPrefab);

            cellView.name = $"Cell {dataIndex}";
            cellView.SetData(_heroList[cellIndex]);

            return cellView;
        }
    }
}