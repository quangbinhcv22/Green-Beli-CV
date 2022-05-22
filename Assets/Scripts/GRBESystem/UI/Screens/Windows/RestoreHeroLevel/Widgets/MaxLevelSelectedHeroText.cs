using System.Globalization;
using Network.Messages.GetHeroList;
using TMPro;
using UI.Widget.HeroCard;
using UI.Widget.HeroCard.Member;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;


namespace GRBESystem.UI.Screens.Windows.RestoreHeroLevel
{
    public class MaxLevelSelectedHeroText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Slider restoreLevelSlider;
        [SerializeField] private HeroVisual hero;
        [SerializeField] private string defaultString;

        private HeroResponse _heroResponse;
        

        private void Awake()
        {
            restoreLevelSlider.onValueChanged.AddListener(OnSliderValueChanged);
            hero.AddCallBackUpdateView(this);
        }

        private void OnSliderValueChanged(float value)
        {
            var spaceLevel = _heroResponse.maxLevel - _heroResponse.level;
            var targetLevel = _heroResponse.level + Mathf.Round(spaceLevel * value);
            
            text.SetText( targetLevel.ToString());
        }

        public void UpdateDefault()
        {
            text.SetText(defaultString);
        }

        public void UpdateView(HeroResponse heroResponse)
        {
            _heroResponse = heroResponse;
            OnSliderValueChanged(restoreLevelSlider.value);
        }

        private void OnValidate()
        {
            Assert.IsNotNull(text);
            Assert.IsNotNull(hero);
        }
    }
}