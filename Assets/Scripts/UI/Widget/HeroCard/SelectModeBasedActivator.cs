using System.Collections.Generic;
using GEvent;
using GRBESystem.UI.Screens.Panels.SelectHero.CellView.Widgets.InteractableSetter;
using Network.Messages.GetHeroList;
using TigerForge;
using UI.Widget.HeroCard.Member;
using UnityEngine;

namespace UI.Widget.HeroCard
{
    public class SelectModeBasedActivator : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual heroOwner;
        [SerializeField] private List<SelectHeroMode> activeSelectHeroModes;

        private void Awake()
        {
            heroOwner.AddCallBackUpdateView(this);
            EventManager.StartListening(EventName.PlayerEvent.SelectHeroMode, OnSetSelectHeroMode);
        }

        private void OnSetSelectHeroMode()
        {
            var selectHeroMode = EventManager.GetData(EventName.PlayerEvent.SelectHeroMode);
            if (selectHeroMode is null) return;

            gameObject.SetActive(activeSelectHeroModes.Contains((SelectHeroMode)selectHeroMode));
        }

        public void UpdateDefault()
        {
            OnSetSelectHeroMode();
        }

        public void UpdateView(HeroResponse hero)
        {
            OnSetSelectHeroMode();
        }
    }
}