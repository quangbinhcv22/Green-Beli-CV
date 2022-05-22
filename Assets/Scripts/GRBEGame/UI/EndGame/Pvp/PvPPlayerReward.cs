using GEvent;
using GRBEGame.Resources;
using GRBESystem.Definitions;
using GRBESystem.Entity.Element;
using Network.Messages.Battle;
using Network.Messages.GetHeroList;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.EndGame.Pvp
{
    public class PvPPlayerReward : MonoBehaviour
    {
        [SerializeField] private Owner owner;
        
        [Header("Score")]
        [SerializeField] private TMP_Text damageText;
        [SerializeField] private TMP_Text lastHitText;
        [SerializeField] private TMP_Text totalText;
        [SerializeField] private string scoreFormat = "{0:N0}";

        [Header("PlayerInfo")]
        [SerializeField] private Image avatarBackGround;
        [SerializeField] private Image avatar;
        [SerializeField] private TMP_Text teamPower;
        [SerializeField] private ElementArtSet artSet;
        [SerializeField] private string powerFormat = "{0:N0}";

        [Header("RewardInfo")] 
        [SerializeField] private TMP_Text countText;
        [SerializeField] private Image chestImage;
        [SerializeField] private Sprite goldChest;
        [SerializeField] private Sprite silverChest;


        private void Awake() => EventManager.StartListening(EventName.Server.EndGame, UpdateView);

        private void Start() => UpdateView();
        
        
        private const int Simple = 1;
        
        private void UpdateView()
        {
            damageText.SetText(string.Format(scoreFormat, GetPlayerInfo().totalAtkDamageScore));
            lastHitText.SetText(string.Format(scoreFormat, GetPlayerInfo().lastHitScore));
            totalText.SetText(string.Format(scoreFormat, GetPlayerInfo().TotalScore));

            var isReward = EndGameServerService.Data.HaveDropGFruit(owner) ||
                           EndGameServerService.Data.HaveDropItemFragment(owner);
            
            countText.gameObject.SetActive(isReward);
            chestImage.gameObject.SetActive(isReward);
           
            if(isReward)
            {
                countText.SetText(Simple.ToString());

                var isSelfGoldChest = EndGameServerService.IsGoldChest() || EndGameServerService.Data.IsOpinionQuitPvp();
                if (owner is Owner.Self)
                    chestImage.sprite = isSelfGoldChest ? goldChest : silverChest;
                else
                    chestImage.sprite = isSelfGoldChest ? silverChest : goldChest;
            }
            
            var element = HeroResponseUtils.GetElement(GetPlayerInfo().MainHero().GetID());
            
            avatarBackGround.sprite = artSet.GetSprite(element);
            avatar.sprite = 
                GrbeGameResources.Instance.HeroIcon.GetIcon(GetPlayerInfo().MainHero().GetID().ToString());
            teamPower.SetText(
                string.Format(powerFormat, StartGameServerService.Data.GetPlayerInfo(owner).heroTeamPower));
        }
        
        private EndGameResponse.PlayerInfo GetPlayerInfo()
        {
            var endGameResponse = EndGameServerService.Response;
            return endGameResponse.IsError
                ? new EndGameResponse.PlayerInfo()
                : endGameResponse.data.GetPlayerInfo(owner);
        }
    }
}
