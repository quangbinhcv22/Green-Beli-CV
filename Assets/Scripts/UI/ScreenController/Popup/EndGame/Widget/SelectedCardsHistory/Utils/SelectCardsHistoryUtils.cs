using System.Collections.Generic;

namespace UI.ScreenController.Popup.EndGame.Widget.SelectedCardsHistory.Utils
{
    public static class SelectCardsHistoryUtils
    {
        private static List<int> GetStandardSetCards()
        {
            var result = new List<int>();

            for (int i = SelectCardHistory.MinCardIndex; i <= SelectCardHistory.MaxCardIndex; i++)
            {
                result.Add(i);
            }

            return result;
        }

        public static List<SelectCardHistory> ConvertToSet10Cards(List<int> selectedCard)
        {
            var standardSetCards = GetStandardSetCards();

            var result = new List<SelectCardHistory>();

            for (int i = 0; i < selectedCard.Count; i++)
            {
                result.Add(new SelectCardHistory() { index = selectedCard[i], isSelected = true });
                standardSetCards.Remove(selectedCard[i]);
            }

            for (int i = 0; i < standardSetCards.Count; i++)
            {
                result.Add(new SelectCardHistory() { index = standardSetCards[i], isSelected = false });
                standardSetCards.Remove(selectedCard[i]);
            }

            return result;
        }
    }
}