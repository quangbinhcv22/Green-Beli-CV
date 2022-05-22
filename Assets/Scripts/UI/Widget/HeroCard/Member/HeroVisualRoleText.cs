using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widget.HeroCard.Member
{
    public enum HeroRole
    {
        None = 0,
        Main = 1,
        Support1 = 2,
        Support2 = 3,
    }

    public class HeroVisualRoleText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private TMP_Text roleText;
        [SerializeField] private string textDefault;
        [SerializeField] private Image background;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            roleText.SetText(textDefault);
        }

        public void UpdateView(HeroResponse hero)
        {
            var heroRole = (HeroRole)hero.selectedIndex;
            
            background.gameObject.SetActive(heroRole != HeroRole.None);
            roleText.SetText(GerFormattedRoleHeroString(heroRole));
        }

        private string GerFormattedRoleHeroString(HeroRole role)
        {
            return role switch
            {
                HeroRole.Main => "Main",
                HeroRole.Support1 => "Support 1",
                HeroRole.Support2 => "Support 2",
                _ => string.Empty
            };
        }
    }
}