using System.Collections.Generic;
using GEvent;
using Network.Messages;
using Network.Messages.GetPvpContestDetail;
using Network.Service;
using TigerForge;
using UIFlow;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    [RequireComponent(typeof(Button))]
    public class PvpSeasonRewardChest : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Sprite closeChestSprite;
        [SerializeField] private Sprite openChestSprite;
        [SerializeField] [Space] private List<GameObject> claimObjects;
        [SerializeField] [Space] private UIRequest screenRequest;
        [SerializeField] private UIRequest lastSeasonRewardScreenRequest;

        private string _id;


        private void Awake()
        {
            button ??= GetComponent<Button>();
        }

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.GetPvpContestDetail, UpdateView);
            EventManager.StartListening(EventName.Server.OpenPvpBoxRewardLeaderboard, OpenSeasonRewardScreen);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetPvpContestDetail, UpdateView);
            EventManager.StopListening(EventName.Server.OpenPvpBoxRewardLeaderboard, OpenSeasonRewardScreen);
        }

        private void UpdateView()
        {
            var response = NetworkService.Instance.services.getPvpContestDetail.Response;
            if(NetworkService.Instance.IsNotLogged() || response.IsError) return;

            _id = response.data.MyReward().id;
            var isReward = string.IsNullOrEmpty(response.data.MyReward().id) is false &&
                           response.data.MyReward().isClaimed is false;

            claimObjects.ForEach(item => item.gameObject.SetActive(isReward));
            button.image.sprite = isReward ? openChestSprite : closeChestSprite;
            if (isReward)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(GetSeasonReward);
            }
            else
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(OpenSeasonRewardInfoScreen);
            }
        }
        
        private void GetSeasonReward()
        {
            Message.Instance().SetId(EventName.Server.OpenPvpBoxRewardLeaderboard).SetRequest(new RewardId
            {
                rewardId = _id
            }).Send();
        }
        
        private void OpenSeasonRewardScreen()
        {
            var response =
                EventManager.GetData<MessageResponse<PvpPlayerInfo>>(EventName.Server.OpenPvpBoxRewardLeaderboard);
            if (response.IsError)
                return;
            
            NetworkService.Instance.services.getPvpContestDetail.SendRequest();
            lastSeasonRewardScreenRequest.SendRequest();
        }

        private void OpenSeasonRewardInfoScreen()
        {
            screenRequest.SendRequest();
        }
    }

    [System.Serializable]
    public struct RewardId
    {
        public string rewardId;
    }
}
