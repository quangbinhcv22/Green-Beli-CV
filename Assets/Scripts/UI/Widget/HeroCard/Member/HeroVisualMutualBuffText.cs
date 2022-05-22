using Calculate;
using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualMutualBuffText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private TMP_Text mutualBuffText;
        [SerializeField] private string textFormat;
        [SerializeField] private string textDefault;

        void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            mutualBuffText.SetText(textDefault);
        }

        public void UpdateView(HeroResponse hero)
        {
            var mutualBuff = MutualCalculator.CalculatePercent(hero.GetID());
            mutualBuffText.SetText(string.Format(textFormat, mutualBuff));
        }
    }
}