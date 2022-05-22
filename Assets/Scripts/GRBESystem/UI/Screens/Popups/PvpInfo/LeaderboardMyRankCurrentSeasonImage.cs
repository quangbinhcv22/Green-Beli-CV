using GEvent;
using Network.Service;
using TigerForge;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    [RequireComponent(typeof(Image))]
    public class LeaderboardMyRankCurrentSeasonImage : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private LuckyGreenbieRankArtSet artSet;
        [SerializeField] private Sprite spriteDefault;
        
        
        private void Awake()
        {
            image ??= gameObject.GetComponent<Image>();
        }
        
        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.GetPvpContestDetail, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetPvpContestDetail, UpdateView);
        }
        
        private void UpdateView()
        {
            const int countBonus = 1;
            
            var response = NetworkService.Instance.services.getPvpContestDetail.Response;
            if (NetworkService.Instance.IsNotLogged() || response.IsError || response.data.topAccounts is null)
            {
                image.gameObject.SetActive(false);
                return;
            }

            var address = NetworkService.Instance.services.login.MessageResponse.data.id;
            var playerRank = new LeaderboardPvpData();
            for (var i = (int)default; i < response.data.topAccounts.Count; i++)
            {
                if (response.data.topAccounts[i].owner != address) continue;
                playerRank =  new LeaderboardPvpData{
                    owner = response.data.topAccounts[i].owner,
                    rank = i + countBonus,
                };
                break;
            }

            if (playerRank.rank <= (int) default || playerRank.rank > artSet.GetMaxCount())
            {
                image.gameObject.SetActive(false);
                return;
            }

            image.gameObject.SetActive(true);
            image.sprite = artSet.GetRankIcon(playerRank.rank);
        }
    }
}
