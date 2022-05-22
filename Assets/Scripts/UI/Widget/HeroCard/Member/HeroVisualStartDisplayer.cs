using Extensions.VisualQuantityDisplayer;
using Network.Messages.GetHeroList;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualStartDisplayer : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private VisualQuantityDisplayer startDisplayer;
        [SerializeField] private int valueDefault;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            startDisplayer.VisualDisplay(valueDefault);
        }

        void IHeroVisualMember.UpdateView(HeroResponse hero)
        {
            startDisplayer.VisualDisplay(hero.star);
        }
    }
}