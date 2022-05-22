using Network.Messages.GetHeroList;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualActiveSetter : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerHero;
        [SerializeField] private bool isActiveDefault;
        [SerializeField] private bool isActiveOnHaveData = true;

        private void Awake()
        {
            ownerHero.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetActive(isActiveDefault);
        }

        public void UpdateView(HeroResponse hero)
        {
            SetActive(isActiveOnHaveData);
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}