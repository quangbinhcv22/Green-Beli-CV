using UnityEngine;

namespace UI.ScreenController.Popup.EndGame.Widget.SelectedCardsHistory.CardView
{
    [CreateAssetMenu(fileName = "HistoryCardViewConfig", menuName = "ScriptableObjects/Screen/ResultBattle/HistoryCardViewConfig")]
    public class HistoryCardViewConfig : UnityEngine.ScriptableObject
    {
        public Color selectedColor;
        public Color unSelectedColor;
    }
}