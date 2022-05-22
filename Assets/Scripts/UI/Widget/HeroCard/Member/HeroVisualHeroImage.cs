using GRBEGame.Resources;
using GRBESystem.Model.HeroIcon;
using Network.Messages.GetHeroList;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualHeroImage : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private Image heroImage;
        [SerializeField] private Sprite spriteDefault;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            heroImage.sprite = spriteDefault;
        }

        void IHeroVisualMember.UpdateView(HeroResponse hero)
        {
            heroImage.sprite = GrbeGameResources.Instance.HeroIcon.GetIcon(hero.GetID().ToString());
        }
    }
}