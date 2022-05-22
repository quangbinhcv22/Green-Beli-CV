using GRBEGame.ArtSet;
using GRBESystem.Definitions.BodyPart.Index;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widget.HeroCard.Member
{
    public class BodyPartImage : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private BodyPartRepresentArtSet artSet;

        public void UpdateView(BodyPartIndex index)
        {
            image.sprite = artSet.GetArt(index);
        }
    }
}