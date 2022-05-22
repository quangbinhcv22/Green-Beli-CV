using System;
using EnhancedUI.EnhancedScroller;
using GEvent;
using Network.Messages.GetHeroList;
using Service.Client.SelectHero;
using TigerForge;
using UI.Widget.HeroCard;
using UI.Widget.HeroCard.Member;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Panels.SelectHero.New
{
    [Obsolete]
    [RequireComponent(typeof(HeroVisual))]
    public class HeroToSelectSlot : EnhancedScrollerCellView, IHeroVisualMember
    {
        public Button selectButton;
        public HeroResponse heroResponse;

        [SerializeField] private SelectHeroClientService selectHeroClientService;
        [SerializeField, Space] private Image selectedBackground;

        private HeroVisual _heroVisual;


        private void Awake()
        {
            _heroVisual = GetComponent<HeroVisual>();
            _heroVisual.AddCallBackUpdateView(this);

            selectButton.onClick.AddListener(SendMessageSelect);
            selectHeroClientService.AddListenerResponse(OnSelectOtherHero);
        }

        private void SendMessageSelect()
        {
            EventManager.EmitEventData(EventName.PlayerEvent.SelectHero, data: heroResponse);
        }

        public void SetData(HeroResponse newHeroResponse)
        {
            heroResponse = newHeroResponse;
            _heroVisual.UpdateView(newHeroResponse);
        }

        public void UpdateDefault()
        {
            OnSelectOtherHero();
        }

        public void UpdateView(HeroResponse newHeroResponse)
        {
            OnSelectOtherHero();
        }

        private void OnSelectOtherHero()
        {
            if (EventManager.GetData(EventName.PlayerEvent.SelectHero) is null)
            {
                selectedBackground.gameObject.SetActive(false);
                return;
            }

            var isSelectedMe = selectHeroClientService.GetEventEmitData().GetID() == heroResponse.GetID();
            selectedBackground.gameObject.SetActive(isSelectedMe);
        }
    }
}