using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    [RequireComponent(typeof(TMP_Text))]
    public class LeaderboardPlayerRankText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private int topRank = 3;
        [SerializeField] private string textFormat = "{0}";
        [SerializeField] private string textDefault = "500+";
        
        
        private void Awake()
        {
            text ??= gameObject.GetComponent<TMP_Text>();
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
            if (NetworkService.Instance.IsNotLogged() || response.IsError || response.data.topAccounts is null) return;

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
            
            var content = string.IsNullOrEmpty(playerRank.owner) ? textDefault : string.Format(textFormat, playerRank.rank);
            text.SetText(playerRank.rank > (int)default && playerRank.rank <= topRank ? string.Empty : content);
        }
    }
}
