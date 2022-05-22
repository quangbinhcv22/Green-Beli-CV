using Network.Messages.GetHeroList;
using Network.Service;
using TMPro;
using UI.Widget.HeroCard;
using UI.Widget.HeroCard.Member;
using UnityEngine;


namespace GRBESystem.UI.Screens.Windows.Fusion.Info
{
    public class HeroFusionInfo : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerHero;
        
        [SerializeField, Space] private TMP_Text currentStarText;
        [SerializeField] private TMP_Text nextStarText;
        [SerializeField, Space] private TMP_Text currentLevelText;
        [SerializeField] private TMP_Text nextLevelText;
        [SerializeField, Space] private TMP_Text numberBodyPartsText;

        [SerializeField] private string stringDefault;


        private void Awake()
        {
            ownerHero.AddCallBackUpdateView(this);
        }
        
        public void UpdateDefault()
        {
            currentStarText.SetText(stringDefault);
            nextStarText.SetText(stringDefault);
            currentLevelText.SetText(stringDefault);
            nextLevelText.SetText(stringDefault);
            numberBodyPartsText.SetText(stringDefault);
        }

        public void UpdateView(HeroResponse hero)
        {
            var price = NetworkService.Instance.services.loadGameConfig.ResponseData.fusion
                .GetNextPriceMemberAtStar(hero.star);
            if(price.Equals(null)) return;
            
            currentStarText.SetText(hero.star.ToString());
            nextStarText.SetText(price.star);
            currentLevelText.SetText(hero.level.ToString());
            nextLevelText.SetText(price.max_level);
            numberBodyPartsText.SetText(price.quantity_level_up_body_part);
        }
    }
}
