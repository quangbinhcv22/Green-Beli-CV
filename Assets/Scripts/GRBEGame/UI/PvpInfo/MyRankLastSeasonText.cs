using GEvent;
using Network.Messages;
using Network.Messages.GetPvpContestDetail;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.PvpInfo
{
    [RequireComponent(typeof(TMP_Text))]
    public class MyRankLastSeasonText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat;
        [SerializeField] private string textDefault = "-----";
        
        
        private void Awake()
        {
            text ??= GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.OpenPvpBoxRewardLeaderboard, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.OpenPvpBoxRewardLeaderboard, UpdateView);
        }
        
        private void UpdateView()
        {
            var response = EventManager.GetData(EventName.Server.OpenPvpBoxRewardLeaderboard);
            if(NetworkService.Instance.IsNotLogged() || response is null) 
            {
                text.SetText(textDefault);
                return;
            }

            var data = (MessageResponse<PvpPlayerInfo>) response;
            if (data.IsError)
            {
                text.SetText(textDefault);
                return;
            }

            text.SetText(string.Format(textFormat, data.data.rank));
        }
    }
}
