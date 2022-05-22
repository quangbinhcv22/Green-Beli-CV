using Network.Messages.GetHeroList;
using UnityEngine;
using UnityEngine.UI;


namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualRoleImage : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private Image background;
        [SerializeField] private Sprite backgroundDefault;
        [SerializeField] private Sprite backgroundMain;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            background.sprite = backgroundDefault;
        }

        public void UpdateView(HeroResponse hero)
        {
            var heroRole = (HeroRole)hero.selectedIndex;
            if(heroRole == HeroRole.None) return;

            background.sprite = GerFormattedRoleHeroString(heroRole);
        }

        private Sprite GerFormattedRoleHeroString(HeroRole role)
        {
            return role switch
            {
                HeroRole.Main => backgroundMain,
                _ => backgroundDefault
            };
        }
    }
}