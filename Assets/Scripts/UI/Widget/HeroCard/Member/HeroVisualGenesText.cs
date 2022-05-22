using System.Globalization;
using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualGenesText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private TMP_Text breedingText;
        [SerializeField] private string defaultValue;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            breedingText.SetText(defaultValue);
        }

        public void UpdateView(HeroResponse hero)
        {
            breedingText.SetText(hero.genes.ToString(CultureInfo.InvariantCulture));
        }
    }
}