using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank.Bridge.Widgets.TransactionHistoryView.CellView
{
    [CreateAssetMenu(fileName = "TransactionHistoryCellViewArtSet", menuName = "ScriptableObjects/ArtSet/TransactionHistoryCellView")]
    public class TransactionHistoryCellViewArtSet : ScriptableObject
    {
        [SerializeField] private Sprite odd;
        [SerializeField] private Sprite even;

        public Sprite GetSprite(int index)
        {
            return IsEvent(index) ? even : odd;
        }

        private static bool IsEvent(int number)
        {
            return number % 2 == 0;
        }
    }
}