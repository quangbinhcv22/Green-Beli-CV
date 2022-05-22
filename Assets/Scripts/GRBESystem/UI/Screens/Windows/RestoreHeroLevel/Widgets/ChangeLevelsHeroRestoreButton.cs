using Network.Messages.GetHeroList;
using UI.Widget.HeroCard;
using UI.Widget.HeroCard.Member;
using UnityEngine;
using UnityEngine.UI;


namespace GRBESystem.UI.Screens.Windows.RestoreHeroLevel
{
    public class ChangeLevelsHeroRestoreButton : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private Button button;
        [SerializeField] private Slider slider;
        [SerializeField] private HeroVisual ownHero;
        [SerializeField] private float ratio;

        private HeroResponse _heroResponse;


        private void Awake()
        {
            ownHero.AddCallBackUpdateView(this);
            button.onClick.AddListener(UpdateSliderView);
            slider.onValueChanged.AddListener(SliderOnValueChanged);
        }

        private void OnEnable()
        {
            SliderOnValueChanged(slider.value);
        }

        private void UpdateSliderView()
        {
            var spaceLevel = _heroResponse.maxLevel - _heroResponse.level;
            slider.value += ratio / spaceLevel;
        }
        
        private void SliderOnValueChanged(float value)
        {
            button.interactable = ratio < (float)default ? value > slider.minValue : value < slider.maxValue;
        }

        public void UpdateDefault()
        {
            button.interactable = default;
        }

        public void UpdateView(HeroResponse hero)
        {
            _heroResponse = hero;
        }
    }
}