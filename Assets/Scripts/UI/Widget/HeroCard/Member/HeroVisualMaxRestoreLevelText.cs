using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;


namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualMaxRestoreLevelText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private HeroVisual ownHero;
        [SerializeField] private string defaultString;
        [SerializeField] private string textFormat = "{0}";


        private void Awake()
        {
            ownHero.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.SetText(defaultString);
        }

        public void UpdateView(HeroResponse hero)
        {
            text.SetText(string.Format(textFormat, hero.maxLevel));
        }
    }
}