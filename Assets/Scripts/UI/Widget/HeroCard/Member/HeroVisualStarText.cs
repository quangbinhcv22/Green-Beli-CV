using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualStarText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private TMP_Text starText;
        [SerializeField] private string textDefault;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            starText.SetText(textDefault);
        }

        public void UpdateView(HeroResponse hero)
        {
            starText.SetText(hero.star.ToString());
        }
    }
}