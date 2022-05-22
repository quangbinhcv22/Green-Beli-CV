using GEvent;
using GRBESystem.Definitions;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.EndGame.Pvp
{
    public class PvpRewardCaseContent : MonoBehaviour
    {
        [SerializeField] private Owner owner;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string defaultString;
        [SerializeField] private string outOfLimitFreeReward;
        [SerializeField] private string disconnect;

        private bool _isFirstLoaded;
        
        
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.EndGame, UpdateView);
        }

        private void OnEnable()
        {
            if(_isFirstLoaded is false)
                UpdateView();
        }

        private void UpdateView()
        {
            var roomData = EventManager.GetData(EventName.Client.Battle.PvpRoom);
            if (EndGameServerService.Response.IsError || roomData is null) return;

            var roomFee = (int) roomData;

            if (EndGameServerService.Data.IsOpinionQuitPvp() && owner is Owner.Opponent)
                text.SetText(disconnect);
            else switch (roomFee)
            {
                case (int) default when NetworkService.Instance.services.login.GetNumberReceivedFreeMaterial() <= (int) default &&
                                        owner is Owner.Self:
                case (int) default when EndGameServerService.Data.HaveDropGFruit(owner) is false && 
                                        EndGameServerService.Data.HaveDropItemFragment(owner) is false &&
                                        owner is Owner.Opponent:
                    text.SetText(outOfLimitFreeReward);
                    break;
                default:
                    text.SetText(defaultString);
                    break;
            }

            _isFirstLoaded = true;
        }
    }
}
