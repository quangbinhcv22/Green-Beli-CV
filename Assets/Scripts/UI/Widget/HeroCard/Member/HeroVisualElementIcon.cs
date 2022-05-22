using GRBESystem.Entity.Element;
using Network.Messages.GetHeroList;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualElementIcon : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private Image elementIcon;
        [SerializeField] private ElementArtSet artSet;
        [SerializeField] private Sprite spriteDefault;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            elementIcon.sprite = spriteDefault;
        }

        void IHeroVisualMember.UpdateView(HeroResponse hero)
        {
            elementIcon.sprite = artSet.GetSprite(hero.GetElement());
        }
    }
}