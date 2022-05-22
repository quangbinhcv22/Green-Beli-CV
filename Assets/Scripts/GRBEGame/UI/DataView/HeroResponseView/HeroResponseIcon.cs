using GRBEGame.Resources;
using Network.Messages.GetHeroList;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;


namespace GRBEGame.UI.DataView.HeroResponseView
{
    [DefaultExecutionOrder(100)]
    public class HeroResponseIcon : MonoBehaviour, IMemberView<HeroResponse>
    {
        [SerializeField] [Space] protected GameObject coreView;

        [SerializeField] private Image icon;
        [SerializeField] private Sprite avatarDefault;
        [SerializeField] private Quaternion maskRotation;


        private void Awake()
        {
            coreView.GetComponent<ICoreView<HeroResponse>>().AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            gameObject.transform.rotation = new Quaternion();
            icon.sprite = avatarDefault;
        }

        public void UpdateView(HeroResponse heroResponse)
        {
            UpdateView(heroResponse.GetID());
        }

        private void UpdateView(long heroId)
        {
            if (heroId is (long)default)
            {
                UpdateDefault();
                return;
            }

            gameObject.transform.rotation = maskRotation;
            icon.sprite = GrbeGameResources.Instance.HeroIcon.GetIcon(heroId.ToString());
        }

        private void OnValidate()
        {
            Assert.IsNotNull(coreView);
            Assert.IsNotNull(coreView.GetComponent<ICoreView<HeroResponse>>());
        }
    }
}