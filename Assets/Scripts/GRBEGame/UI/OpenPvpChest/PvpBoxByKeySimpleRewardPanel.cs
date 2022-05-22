using GEvent;
using Manager.Inventory;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.OpenPvpChest
{
    public class PvpBoxByKeySimpleRewardPanel : MonoBehaviour
    {
        [SerializeField] private Button oKButton;
        [SerializeField] private PvpChestRewardItem chestItem;
        [SerializeField] private TMP_Text receiveText;
        [SerializeField] private string receiveString;

        private bool _isReward;


        private void Awake()
        {
            chestItem.AddOnClickListenerClaimButton(OnClaimReward);
        }

        private void OnEnable()
        {
            if(NetworkService.Instance.IsNotLogged()) return;
            
            UpdateView();
            EventManager.StartListening(EventName.Server.OpenPvpBoxRewardEarnKey, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.OpenPvpBoxRewardEarnKey, UpdateView);
        }
        
        private void OnClaimReward()
        {
            if(_isReward is false) return;
            
            var response = NetworkService.Instance.services.openPvpBox.Response;
            SetActiveWidgetByReward(false);
            switch (response.data[default].type)
            {
                case 0 :
                    chestItem.UpdateView(PvpRewardType.GFruit, response.data[default].amount);
                    NetworkService.playerInfo.inventory.AddMoney(MoneyType.GFruit, response.data[default].amount);
                    break;
                default:
                    chestItem.UpdateView(PvpRewardType.Fragment, response.data[default].amount,
                        response.data[default].type);
                    break;
            }

            NetworkService.Instance.services.getRewardHistoryAll.SendRequest();
            EventManager.EmitEvent(EventName.Client.EventPvpKeyUpdate);
        }

        private void UpdateView()
        {
            var response = NetworkService.Instance.services.openPvpBox.Response;
            if (response.IsError) return;

            SetActiveWidgetByReward(true);
        }

        private void SetActiveWidgetByReward(bool isReward)
        {
            _isReward = isReward;
            oKButton.gameObject.SetActive(isReward is false);
            chestItem.SetActiveState(isReward ? PvpRewardActiveState.Claim : PvpRewardActiveState.Claimed);
            receiveText.SetText(isReward is false ? receiveString : string.Empty);
        }
    }
}
