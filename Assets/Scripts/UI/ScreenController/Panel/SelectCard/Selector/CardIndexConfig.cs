using System.Collections.Generic;
using UnityEngine;

namespace UI.ScreenController.Panel.SelectCard.Selector
{
    [CreateAssetMenu(fileName = "CardIndexConfig", menuName = "ScriptableObjects/CardIndexConfig")]
    public class CardIndexConfig : UnityEngine.ScriptableObject
    {
        [SerializeField] private List<Sprite> cardNumberSprites;

        public Sprite GetCardSprite(int cardIndex)
        {
            // card index start from 1, while list index start form 0
            return this.cardNumberSprites[cardIndex];
        }
    }
}