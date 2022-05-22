using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualIdText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private TMP_Text idText;
        [SerializeField] private string defaultValue;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            idText.SetText(defaultValue);
        }

        public void UpdateView(HeroResponse hero)
        {
            idText.SetText(FormattedId(hero.GetID()));
        }

        private string FormattedId(long id)
        {
            return $"#{id}";
        }
    }
}