using Network.Messages.GetHeroList;
using Network.Service;
using TMPro;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualMaxLevelText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private TMP_Text maxLevelText;
        [SerializeField] private string textDefault;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            maxLevelText.SetText(textDefault);
        }

        public void UpdateView(HeroResponse hero)
        {
            maxLevelText.SetText(GetMaxLevelAtStar(hero.star).ToString());
        }

        private int GetMaxLevelAtStar(int star)
        {
            var gameConfig = NetworkService.Instance.services.loadGameConfig.ResponseData;
            return gameConfig.levelCapacityStar.GetMaxLevelAtStar(star);
        }
    }
}