using UI.ScreenController.Panel.SelectCard.Selector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ScreenController.Popup.EndGame.Widget.SelectedCardsHistory.CardView
{
    public class HistoryCardView : MonoBehaviour
    {
        [SerializeField, Space] private CardIndexConfig cardIndexConfig;
        [SerializeField] private HistoryCardViewConfig cardViewConfig;

        [SerializeField] private Image card;

        public void UpdateView(SelectCardHistory selectCardHistory)
        {
            card.sprite = cardIndexConfig.GetCardSprite(selectCardHistory.index);
            card.color = selectCardHistory.isSelected ? cardViewConfig.selectedColor : cardViewConfig.unSelectedColor;
        }
    }
}