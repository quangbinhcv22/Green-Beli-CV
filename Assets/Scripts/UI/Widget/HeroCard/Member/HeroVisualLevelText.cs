using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualLevelText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private string textDefault;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            levelText.SetText(textDefault);
        }

        void IHeroVisualMember.UpdateView(HeroResponse hero)
        {
            levelText.SetText(hero.level.ToString());
        }
    }
}