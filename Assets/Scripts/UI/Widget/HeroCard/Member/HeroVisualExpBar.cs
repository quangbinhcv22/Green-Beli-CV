using Network.Messages.GetHeroList;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualExpBar : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private ProcessBar expBar;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            expBar.ResetView();
        }

        public void UpdateView(HeroResponse hero)
        {
            expBar.UpdateView(hero.GetRealHeroExp(), hero.expToUpLevel);
        }
    }
}