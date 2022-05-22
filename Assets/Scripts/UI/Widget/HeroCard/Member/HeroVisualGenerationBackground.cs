using GRBESystem.Entity.Generation;
using Network.Messages.GetHeroList;
using UnityEngine;
using UnityEngine.UI;


namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualGenerationBackground : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private Image generationBackground;
        [SerializeField] private GenerationArtSet generationArtConfig;
        [SerializeField] private int defaultHeroRarity = 1;

        
        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }
        
        public void UpdateDefault()
        {
            UpdateGenerationBackground(defaultHeroRarity);
        }

        public void UpdateView(HeroResponse hero)
        {
            UpdateGenerationBackground(hero.rarity);
        }
        
        private void UpdateGenerationBackground(int heroRarity)
        {
            generationBackground.sprite = generationArtConfig.GetGenerationSprite(heroRarity);
        }
    }
}
