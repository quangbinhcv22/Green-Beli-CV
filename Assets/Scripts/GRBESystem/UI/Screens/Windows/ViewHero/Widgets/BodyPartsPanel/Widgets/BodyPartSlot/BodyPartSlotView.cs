using Network.Messages.GetHeroList;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.ViewHero.Widgets.BodyPartsPanel.Widgets.BodyPartSlot
{
    public class BodyPartSlotView : MonoBehaviour
    {
        private const string DefaultContent = "-";

        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Image backgroundLevel;
        
        [SerializeField] private Image backgroundView;


        public void UpdateView(HeroResponse.BodyPart slotViewData)
        {
            levelText.text = GetFormattedLevelText(slotViewData.level);
        }

        public void UpdateRarityBackground(Sprite rarityLevel, Sprite rarityView)
        {
            this.backgroundLevel.sprite = rarityLevel;
            this.backgroundView.sprite = rarityView;
        }

        private string GetFormattedLevelText(int level)
        {
            return level == 0 ? DefaultContent : $"{level}";
        }
    }
}