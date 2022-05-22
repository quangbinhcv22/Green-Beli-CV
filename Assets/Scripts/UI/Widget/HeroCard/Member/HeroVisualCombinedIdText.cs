using System.Text;
using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualCombinedIdText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private TMP_Text contentText;

        [SerializeField, Space] private string stringFormat;
        [SerializeField] private int numberCharacterInText;
        [SerializeField] private string defaultTextValue;


        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }
        
        public void UpdateDefault()
        {
            contentText.SetText(defaultTextValue);
        }

        public void UpdateView(HeroResponse hero)
        {
            contentText.SetText(FormattedID(hero.GetID()));
        }

        private string FormattedID(long id)
        {
            const int firstIndex = 0;
            var stringBuilder = new StringBuilder(id.ToString());
            stringBuilder.Remove(numberCharacterInText, stringBuilder.Length - numberCharacterInText);
            stringBuilder.Insert(firstIndex, stringFormat);
            return stringBuilder.ToString();
        }
    }
}
