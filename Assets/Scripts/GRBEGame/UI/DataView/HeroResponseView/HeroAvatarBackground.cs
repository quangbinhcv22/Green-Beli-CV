using GRBESystem.Entity.Element;
using Network.Messages.GetHeroList;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GRBEGame.UI.DataView.HeroResponseView
{
    [DefaultExecutionOrder(100)]
    public class HeroAvatarBackground : MonoBehaviour, IMemberView<HeroResponse>
    {
        [SerializeField] [Space] protected GameObject coreView;

        [SerializeField] private Image background;
        [SerializeField] private Sprite avatarDefault;
        [SerializeField] private ElementArtSet artSet;

        private void Awake()
        {
            coreView.GetComponent<ICoreView<Network.Messages.GetHeroList.HeroResponse>>().AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            background.sprite = avatarDefault;
        }

        public void UpdateView(HeroResponse heroResponse)
        {
            UpdateView(heroResponse.GetID());
        }

        public void UpdateView(long heroId)
        {
            if (heroId is (long)default)
            {
                UpdateDefault();
                return;
            }

            var element = HeroResponseUtils.GetElement(heroId);
            background.sprite = artSet.GetSprite(element);
        }

        private void OnValidate()
        {
            Assert.IsNotNull(coreView);
            Assert.IsNotNull(coreView.GetComponent<ICoreView<HeroResponse>>());
        }
    }
}