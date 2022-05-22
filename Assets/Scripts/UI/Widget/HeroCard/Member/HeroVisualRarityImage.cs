using Config.ArtSet;
using Network.Messages.GetHeroList;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualRarityImage : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private Image rarityImage;
        [SerializeField] private HeroRarityArtSet artSet;
        [SerializeField] private Sprite artDefault;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            rarityImage.sprite = artDefault;
        }

        void IHeroVisualMember.UpdateView(HeroResponse hero)
        {
            rarityImage.sprite = artSet.GetRaritySprite(hero.rarity);
        }
    }
}