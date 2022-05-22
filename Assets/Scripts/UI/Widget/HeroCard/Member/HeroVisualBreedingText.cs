using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualBreedingText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private TMP_Text breedingText;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            breedingText.SetText(FormattedBreeding(breeding: 0, breedingLimit: 0));
        }

        public void UpdateView(HeroResponse heroResponse)
        {
            breedingText.SetText(FormattedBreeding(heroResponse.breeding, heroResponse.breedingLimit));
        }

        private static string FormattedBreeding(int breeding, int breedingLimit)
        {
            return $"{breedingLimit - breeding}/{breedingLimit}";
        }
    }

    public interface IHeroVisualMember
    {
        void UpdateDefault();
        void UpdateView(HeroResponse hero);
    }
}