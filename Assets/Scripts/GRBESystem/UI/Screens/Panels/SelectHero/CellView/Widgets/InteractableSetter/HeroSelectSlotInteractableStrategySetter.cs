using System.Collections.Generic;
using GEvent;
using GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter.Condition;
using Network.Messages.GetHeroList;
using TigerForge;
using UI.Widget.HeroCard;
using UI.Widget.HeroCard.Member;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter
{
    public enum SelectHeroMode
    {
        None = 0,
        Normal = 1,
        HeroTeam = 2,
        Breeding = 3,
        Fusion = 4,
        RestoreLevels = 5,
    }


    public class HeroSelectSlotInteractableStrategySetter : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual heroVisual;
        [SerializeField] private Button heroSelectButton;
        [SerializeField] private HeroResponse _heroResponse;
        // [SerializeField] private HeroSelectSlotInteractableModeEmitter interactableModeEmitter;
        [SerializeField] private SelectHeroMode interactableMode;


        private static Dictionary<SelectHeroMode, IHeroSelectSlotInteractableCondition> _interactableConditionDefines;
        private IHeroSelectSlotInteractableCondition _interactableCondition;

        private IHeroSelectSlotInteractableCondition InteractableCondition
        {
            set
            {
                _interactableCondition = value;
                ReUpdateView();
            }
        }


        static HeroSelectSlotInteractableStrategySetter()
        {
            _interactableConditionDefines = new Dictionary<SelectHeroMode, IHeroSelectSlotInteractableCondition>()
            {
                { SelectHeroMode.None, new InteractableAlwaysTrueCondition() },
                { SelectHeroMode.Normal, new InteractableAlwaysTrueCondition() },
                { SelectHeroMode.HeroTeam, new InteractableBasedHeroTeamCondition() },
                { SelectHeroMode.Breeding, new InteractableBasedBreedingCondition() },
                { SelectHeroMode.Fusion, new InteractableBasedFusionCondition() },
                { SelectHeroMode.RestoreLevels, new InteractableBasedRestoreLevelsCondition() },
            };
        }


        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.SelectHeroMode, OnSetInteractableMode);
            OnSetInteractableMode();

            heroVisual.AddCallBackUpdateView(this);

            EventManager.StartListening(EventName.PlayerEvent.BreedingHeroes, OnSetInteractableMode);
            EventManager.StartListening(EventName.PlayerEvent.BattleHeroes, OnSetInteractableMode);
            EventManager.StartListening(EventName.PlayerEvent.FusionHeroes, OnSetInteractableMode);
            EventManager.StartListening(EventName.PlayerEvent.RestoreLevelsHero, OnSetInteractableMode);

            EventManager.StartListening(EventName.Server.GetListHero, OnSetInteractableMode);
        }

        private void OnSetInteractableMode()
        {
            var selectHeroMode = EventManager.GetData(EventName.PlayerEvent.SelectHeroMode);

            if (selectHeroMode is null)
            {
                // interactableModeEmitter.EmitEvent();
                // return;
            }

            SetInteractableMode((SelectHeroMode)selectHeroMode);
        }

        private void SetInteractableMode(SelectHeroMode newInteractableMode)
        {
            InteractableCondition = _interactableConditionDefines[newInteractableMode];
        }


        public void UpdateDefault()
        {
            OnSetInteractableMode();
        }

        public void UpdateView(HeroResponse hero)
        {
            _heroResponse = hero;
            ChangeMechanism();
        }

        private void ReUpdateView()
        {
            UpdateView(_heroResponse);
        }

        private void ChangeMechanism()
        {
            heroSelectButton.interactable = _interactableCondition.CanInteractable(_heroResponse);
        }
    }
}