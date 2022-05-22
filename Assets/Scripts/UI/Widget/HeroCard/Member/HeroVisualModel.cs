using GEvent;
using Extensions.Initialization.Request;
using Network.Messages.GetHeroList;
using TigerForge;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualModel : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private ShowHeroModelRequest showHeroModelRequest;
        [SerializeField] private bool recallAllHeroWhenReshow;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
        }

        public void UpdateView(HeroResponse hero)
        {
            if (isActiveAndEnabled is false) return;

            if (recallAllHeroWhenReshow) EventManager.EmitEvent(EventName.Model.HideAllModels);

            var selectedHero = EventManager.GetData(EventName.PlayerEvent.SelectHero);
            if (selectedHero is null) return;

            showHeroModelRequest.heroId = ((HeroResponse)selectedHero).GetID();
            EventManager.EmitEventData(EventName.Model.ShowHeroModel, showHeroModelRequest);
        }
    }
}