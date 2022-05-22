using System;
using System.Linq;
using GEvent;
using GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter;
using GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter.Condition;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Panels.SelectHero.Widgets.TotalHeroes
{
    public class TotalHeroesText : MonoBehaviour
    {
        [SerializeField] private TMP_Text contentText;
        [SerializeField] private string contentDefault;

        private SelectHeroMode _selectHeroMode;


        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.SelectHeroMode, UpdateView);
            EventManager.StartListening(EventName.PlayerEvent.BreedingHeroes, UpdateView);
            EventManager.StartListening(EventName.PlayerEvent.FusionHeroes, UpdateView);
            EventManager.StartListening(EventName.PlayerEvent.RestoreLevelsHero, UpdateView);
        }

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.GetListHero, UpdateView);
            EventManager.StartListening(EventName.Server.RestoreHeroLevel, UpdateView);
            EventManager.StartListening(EventName.Server.BreedingHero, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetListHero, UpdateView);
            EventManager.StopListening(EventName.Server.RestoreHeroLevel, UpdateView);
            EventManager.StopListening(EventName.Server.BreedingHero, UpdateView);
        }

        private void UpdateView()
        {
            if (NetworkService.Instance.IsNotLogged() ||
                NetworkService.Instance.services.getHeroList.HeroResponses == null)
            {
                UpdateViewDefault();
                return;
            }

            var notFilterHeroes = NetworkService.Instance.services.getHeroList.HeroResponses;
            contentText.SetText(notFilterHeroes.Where(GetHeroSelectSlotInteractable().CanInteractable).ToList().Count.ToString());
        }

        private IHeroSelectSlotInteractableCondition GetHeroSelectSlotInteractable()
        {
            UpdateSelectHeroMode();
            return _selectHeroMode switch
            {
                SelectHeroMode.Breeding => new InteractableBasedBreedingCondition(),
                SelectHeroMode.Fusion => new InteractableBasedFusionCondition(),
                SelectHeroMode.RestoreLevels => new InteractableBasedRestoreLevelsCondition(),
                _ => new InteractableAlwaysTrueCondition()
            };
        }

        private void UpdateSelectHeroMode()
        {
            var data = EventManager.GetData(EventName.PlayerEvent.SelectHeroMode);
            if(NetworkService.Instance.IsNotLogged() || data is null) return;
            _selectHeroMode = (SelectHeroMode) data;
        }

        private void UpdateViewDefault()
        {
            contentText.SetText(contentDefault);
        }
    }
}