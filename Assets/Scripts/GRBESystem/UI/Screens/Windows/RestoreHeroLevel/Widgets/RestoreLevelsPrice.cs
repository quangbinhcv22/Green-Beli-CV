using Network.Messages.GetHeroList;
using Network.Service;
using TMPro;
using UI.Widget.HeroCard;
using UI.Widget.HeroCard.Member;
using UnityEngine;
using UnityEngine.UI;


namespace GRBESystem.UI.Screens.Windows.RestoreHeroLevel
{
    public class RestoreLevelsPrice : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private TMP_Text gFruitText;
        [SerializeField] private Slider slider;
        [SerializeField] private HeroVisual ownHero;
        [SerializeField] private string defaultString;

        private HeroResponse _heroResponse;
        private long _restorePrice;

        public long RestorePrice()
        {
            return _restorePrice;
        }

        private void Awake()
        {
            ownHero.AddCallBackUpdateView(this);
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            var spaceLevel = Mathf.Round((_heroResponse.maxLevel - _heroResponse.level) * value);
            var totalLevel = (int) spaceLevel + _heroResponse.level;

            _restorePrice = GetPrice(totalLevel);
            gFruitText.SetText(_restorePrice.ToString("N0"));
        }

        private long GetPrice(int restoreLevel)
        {
            if (restoreLevel == _heroResponse.level) return 0;

            var restoreLevelPrices = NetworkService.Instance.services.loadGameConfig
                .ResponseData.restoreLevel.restore_level_price;

            var minLevel = _heroResponse.level;
            var price = (long) default;
            restoreLevelPrices.ForEach(restoreLevelPrice =>
            {
                if (restoreLevelPrice.max_level <= minLevel || minLevel >= restoreLevel) return;
                var math = restoreLevelPrice.max_level + 1 > restoreLevel
                    ? restoreLevel
                    : restoreLevelPrice.max_level + 1;
                var spaceLevel = math - minLevel;

                price += spaceLevel * restoreLevelPrice.gfruits_per_level;
                minLevel = math;
            });

            var restoreLevelPrice = restoreLevelPrices.Find(restoreLevelPrice =>
                restoreLevelPrice.min_level <= restoreLevel && restoreLevelPrice.max_level >= restoreLevel);
            if (restoreLevelPrice != null)
            {
                price += restoreLevelPrice.gfruits_per_level;
            }

            return price;
        }

        public void UpdateDefault()
        {
            gFruitText.SetText(defaultString);
        }

        public void UpdateView(HeroResponse hero)
        {
            _heroResponse = hero;
            slider.value = slider.maxValue;

            OnSliderValueChanged(slider.value);
        }
    }
}