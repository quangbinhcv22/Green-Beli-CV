using Config.ArtSet;
using Config.Format;
using Config.Localize;
using GRBESystem.Definitions.BodyPart.Index;
using GRBESystem.Entity.Rarity;
using Network.Messages.GetHeroList;
using TMPro;
using UI.Widget.HeroCard.Member;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.FusionResult.Widgets
{
    public class StatCellView : MonoBehaviour
    {
        [SerializeField] private BodyPartImage image;
        
        [SerializeField] [Space] private TMP_Text nameText;
        [SerializeField]  private BodyPartLocalizeSet nameSet;

        [SerializeField] [Space] private TMP_Text valueText;
        [SerializeField] private BodyPartFormatSet valueFormatSet;

        [SerializeField] [Space]  private Image bodyPartBackground;
        [SerializeField] private RarityArtConfig bodyPartBackgroundArtSet;

        [SerializeField][Space] private TMP_Text levelText;
        [SerializeField] private Image levelBackground;
        [SerializeField] private RarityArtConfig levelBackgroundArtSet;

        public void UpdateView(BodyPartIndex bodyPartIndex, HeroResponse hero)
        {
            if (image) image.UpdateView(bodyPartIndex);
            if (nameText) nameText.SetText(nameSet.GetText(bodyPartIndex));
            if (valueText) valueText.SetText(string.Format(valueFormatSet.GetFormat(bodyPartIndex), hero.GetStatValue(bodyPartIndex)));

            var bodyPart = hero.bodyParts.Find(part => part.id.Equals((int)bodyPartIndex));
            if (levelText) levelText.SetText(bodyPart.level.ToString());
            if (bodyPartBackground) bodyPartBackground.sprite = bodyPartBackgroundArtSet.GetRarityArtPair(bodyPart.rarity).mainBackground;
            if (levelBackground) levelBackground.sprite = levelBackgroundArtSet.GetRarityArtPair(bodyPart.rarity).levelBackground;
        }
    }
}