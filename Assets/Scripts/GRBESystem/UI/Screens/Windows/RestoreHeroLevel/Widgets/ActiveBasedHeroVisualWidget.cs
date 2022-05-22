using Network.Messages.GetHeroList;
using UI.Widget.HeroCard;
using UI.Widget.HeroCard.Member;
using UnityEngine;


namespace GRBESystem.UI.Screens.Windows.RestoreHeroLevel
{
    public class ActiveBasedHeroVisualWidget : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownHero;
        private HeroResponse _heroResponse;


        private void Awake()
        {
            ownHero.AddCallBackUpdateView(this);
        }

        public void OnEnable()
        {
            if(_heroResponse is null) return;
            SetActive(_heroResponse.GetID() > (long) default);
        }

        public void UpdateDefault()
        {
            SetActive(false);
        }

        public void UpdateView(HeroResponse hero)
        {
            _heroResponse = hero;
            SetActive(_heroResponse.GetID() > (long) default);
        }

        private void SetActive(bool enable)
        {
            gameObject.SetActive(enable);
        }
    }
}