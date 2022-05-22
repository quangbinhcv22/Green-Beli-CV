namespace UI.ScreenController.Popup.EndGame.Widget.SelectedCardsHistory
{
    [System.Serializable]
    public struct SelectCardHistory
    {
        public const int MinCardIndex = 1;
        public const int MaxCardIndex = 10;
        
        public int index;
        public bool isSelected;
    }
}