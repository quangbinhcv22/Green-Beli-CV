using GEvent;
using GRBEGame.Resources;
using GRBESystem.Definitions;
using GRBESystem.Entity.Element;
using Network.Messages.GetHeroList;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ScreenController.Popup.EndGame.Widget
{
    public class PlayerResultBattleInfoPanel : MonoBehaviour
    {
        [SerializeField, Space] private Owner owner;

        [SerializeField] [Space] private Image avatarBackGround;
        [SerializeField] private Image heroAvatar;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private ElementArtSet artSet;

        private void Awake()
        {
            EventManager.StartListening(EventName.Server.EndGame, UpdateView);
        }

        private void OnEnable() => UpdateView();

        private void UpdateView()
        {
            var endGamePlayerInfo = EndGameServerService.Data.GetPlayerInfo(owner);
            var element = HeroResponseUtils.GetElement(endGamePlayerInfo.MainHero().GetID());

            scoreText.text = $"{endGamePlayerInfo.totalAtkDamage:N0}";
            avatarBackGround.sprite = artSet.GetSprite(element);
            heroAvatar.sprite =
                GrbeGameResources.Instance.HeroIcon.GetIcon(endGamePlayerInfo.MainHero().GetID().ToString());
        }
    }
}