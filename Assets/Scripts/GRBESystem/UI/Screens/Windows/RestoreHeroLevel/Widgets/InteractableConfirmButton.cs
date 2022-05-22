using Manager.UseFeaturesPermission;
using Network.Messages.GetHeroList;
using Network.Service;
using UI.Widget.HeroCard;
using UI.Widget.HeroCard.Member;
using UnityEngine;
using UnityEngine.UI;


namespace GRBESystem.UI.Screens.Windows.RestoreHeroLevel
{
    public class InteractableConfirmButton : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private Button button;
        [SerializeField] private Slider slider;
        [SerializeField] private HeroVisual ownHero;
        [SerializeField] private RestoreLevelsPrice restoreLevelsPrice;


        private void Awake()
        {
            ownHero.AddCallBackUpdateView(this);
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnEnable()
        {
            OnSliderValueChanged(slider.value);
        }

        private void OnSliderValueChanged(float value)
        {
            var gFruit = NetworkService.playerInfo.inventory.GetMoney(0);
            var fee = restoreLevelsPrice.RestorePrice();
            
            button.interactable = PermissionUseFeature.CanUse(FeatureId.RestoreLevel) &&  value > slider.minValue && fee > (int) default && fee <= gFruit;
        }

        public void UpdateDefault()
        {
            button.interactable = default;
        }

        public void UpdateView(HeroResponse hero)
        {
            OnSliderValueChanged(slider.value);
        }
    }
}