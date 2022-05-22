using Network.Messages.GetHeroList;
using Network.Service;
using TMPro;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualNextMaxLevelText : MonoBehaviour, IHeroVisualMember
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
            var nextStart = hero.star + 1;
            maxLevelText.SetText(GetMaxLevelAtStar(nextStart).ToString());
        }

        private int GetMaxLevelAtStar(int star)
        {
            var gameConfig = NetworkService.Instance.services.loadGameConfig.ResponseData;
            return gameConfig.levelCapacityStar.GetMaxLevelAtStar(star);
        }
    }
}
