using Network.Messages.GetHeroList;
using Network.Service;
using TMPro;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public class HeroVisualNextLevelUpBodyPartCountText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private TMP_Text contentText;
        [SerializeField] private string textDefault;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            contentText.SetText(textDefault);
        }

        public void UpdateView(HeroResponse hero)
        {
            contentText.SetText(GetNextLevelUpBodyPartCount(hero.star).ToString());
        }

        private int GetNextLevelUpBodyPartCount(int star)
        {
            var gameConfig = NetworkService.Instance.services.loadGameConfig.ResponseData;
            return gameConfig.fusion.GetNextLevelUpBodyPartCount(star);
        }
    }
}
