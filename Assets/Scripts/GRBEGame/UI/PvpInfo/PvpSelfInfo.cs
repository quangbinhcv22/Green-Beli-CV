using GEvent;
using GRBEGame.UI.DataView.Pvp;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.PvpInfo
{
    public class PvpSelfInfo : MonoBehaviour
    {
        [SerializeField] private PvpPlayerCoreView coreView;

        private void OnEnable()
        {
            EventManager.StartListening(EventName.Server.GetPvpContestDetail, UpdateView);
            UpdateView();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetPvpContestDetail, UpdateView);
        }

        private void UpdateView()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn) return;

            var pvpContestResponse = NetworkService.Instance.services.getPvpContestDetail.Response;
            if (pvpContestResponse.IsError) return;

            var player = pvpContestResponse.data.MyRewardCurrentSeason();
            player.numberReceivedFreeMaterial = NetworkService.Instance.services.login.GetNumberReceivedFreeMaterial();
            coreView.UpdateView(player);
        }
    }
}