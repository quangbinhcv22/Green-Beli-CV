using Config.Format;
using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualGenerationText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private TMP_Text generationText;
        [SerializeField] private GenerationFormatConfig formatConfig;
        [SerializeField] private string defaultTextValue;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            generationText.SetText(defaultTextValue);
        }

        public void UpdateView(HeroResponse hero)
        {
            generationText.SetText(FormattedGeneration(hero.generation));
        }

        private string FormattedGeneration(int generation)
        {
            return string.Format(formatConfig.GetFormat(generation), generation);
        }
    }
}