using System;
using GEvent;
using GRBESystem.UI.Screens.Windows.MainHall.Widgets.PlayerInfoPanel.CountdownRecoverEnergy;
using Manager.UseFeaturesPermission;
using Network.Service;
using Network.Service.Implement;
using Service.Client.ViewRewardDateDetail;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace GRBESystem.UI.Screens.Popups.Bank.PiggyBank.Widgets.RewardHistoryDateView
{
    public class RewardHistoryDateViewer : MonoBehaviour
    {
        [HideInInspector] public string dateVietnameseTimeKey;

        [SerializeField, Space] private ViewRewardDateDetailClientService viewRewardDateDetailClientService;

        [SerializeField, Space] private TMP_Text dateText;
        [SerializeField] private TMP_Text totalCoinText;

        [SerializeField] private TMP_Text claimDayCountdownText;
        [SerializeField] private TextCountdown claim24HCountdownText;

        [SerializeField] private Image notClaimObject;

        [SerializeField, Space] private Button claimButton;

        [SerializeField, Space] private Button viewDetailButton;
        [SerializeField, Space] private Sprite extendButton;
        [SerializeField] private Sprite collapseButton;


        private void Awake()
        {
            viewDetailButton.onClick.AddListener(ViewDetailSources);
            claimButton.onClick.AddListener(SendClainRequest);

            viewRewardDateDetailClientService.AddListenerResponse(OnViewRewardDateDetailOther);
        }

        private void OnEnable()
        {
            claimButton.interactable = PermissionUseFeature.CanUse(FeatureId.LockReward);
        }

        private void OnDisable()
        {
            CollapseViewDetailSources();
        }

        private void SendClainRequest()
        {
            NetworkService.Instance.services.claimRewardByDate.SendRequest(dateVietnameseTimeKey);
        }


        public void UpdateView(RewardHistoryByDateResponse rewardHistoryByDate)
        {
            dateVietnameseTimeKey = rewardHistoryByDate.date;

            UpdateGeneralView(rewardHistoryByDate);
            UpdateMechanismClaim(rewardHistoryByDate);
        }

        private void UpdateGeneralView(RewardHistoryByDateResponse rewardHistoryByDate)
        {
            dateText.SetText(GetFormattedDate());
            totalCoinText.SetText(FormattedReward((int)rewardHistoryByDate.amount));

            string FormattedReward(int reward)
            {
                // const int oneMillion = 1000000;
                // const int oneThousand = 1000;
                // var isMillionNumber = reward >= oneMillion;
                //
                // return isMillionNumber ? $"{(float)reward / oneThousand:N0} K" : $"{reward:N0}";
                
                return $"{reward:N0}";
            }
        }

        private string GetFormattedDate()
        {
            return dateVietnameseTimeKey.ToDateTime(DateTimeUtils.FranceFormatDate)
                .ToString(DateTimeUtils.GreenBeliOnlyDayFormat);
        }

        private void UpdateMechanismClaim(RewardHistoryByDateResponse rewardHistoryByDate)
        {
            var serverTime = TimeManager.Instance.ServerTime;

            var allowedClaimDate = rewardHistoryByDate.allowedClaimDate.ToDateTime(DateTimeUtils.FranceFormatDate);
            var distanceAllowedClaimDayFromToday = DateTimeUtils.DistanceDay(serverTime, allowedClaimDate);

            var isAllowedClaim = IsCanClaim(distanceAllowedClaimDayFromToday);

            claimButton.gameObject.SetActive(isAllowedClaim);


            var isDayCountDownType = allowedClaimDate - serverTime > TimeSpan.FromDays(1);

            claimDayCountdownText.gameObject.SetActive(isDayCountDownType);
            claim24HCountdownText.gameObject.SetActive(isDayCountDownType == false);

            claimDayCountdownText.SetText(FormattedDaysLeft(distanceAllowedClaimDayFromToday));
            claim24HCountdownText.StartCountdown(() => serverTime, () => allowedClaimDate);

            notClaimObject.gameObject.SetActive(isAllowedClaim == false);
        }

        private bool IsCanClaim(int distanceAllowedClaimDayFromToday)
        {
            const int distanceYesterdayBackBefore = 0;
            return distanceAllowedClaimDayFromToday <= distanceYesterdayBackBefore;
        }

        private string FormattedDaysLeft(int day)
        {
            const string pluralNounSuffix = "s";
            return $"{day} day{(day == 1 ? string.Empty : pluralNounSuffix)} left";
        }


        private void ViewDetailSources()
        {
            viewRewardDateDetailClientService.EmitData(new ViewRewardDateDetailRequest()
                { dateKey = dateVietnameseTimeKey, siblingIndex = transform.GetSiblingIndex() });
        }

        private void CollapseViewDetailSources()
        {
            EventManager.EmitEvent(EventName.PlayerEvent.CollapseRewardDateDetail);
            ChangeMechanismViewDetailButton(false);
        }

        private void OnViewRewardDateDetailOther()
        {
            var isViewMe = viewRewardDateDetailClientService.GetEventEmitData().dateKey == dateVietnameseTimeKey;
            ChangeMechanismViewDetailButton(isViewMe);
        }

        private void ChangeMechanismViewDetailButton(bool isViewMe)
        {
            viewDetailButton.GetComponent<Image>().sprite = isViewMe ? collapseButton : extendButton;
            viewDetailButton.onClick.RemoveAllListeners();

            if (isViewMe)
            {
                viewDetailButton.onClick.AddListener(CollapseViewDetailSources);
            }
            else
            {
                viewDetailButton.onClick.AddListener(ViewDetailSources);
            }
        }
    }


    [RequireComponent(typeof(Button))]
    public class AddTextInputButton : MonoBehaviour
    {
        [SerializeField] private TMP_InputField input;
        [SerializeField] private string addString;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(AddInputText);
        }

        private void AddInputText()
        {
            input.text += addString;
        }

        public void SetAddString(string newAddString)
        {
            addString = newAddString;
        }
    }
}