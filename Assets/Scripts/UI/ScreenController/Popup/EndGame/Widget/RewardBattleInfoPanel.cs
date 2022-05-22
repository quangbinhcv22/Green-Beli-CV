using System.Collections.Generic;
using GEvent;
using GRBEGame.UI.Screen.Inventory;
using GRBEGame.UI.Screen.Inventory.Fragment;
using Network.Service;
using Network.Service.Implement;
using QuangBinh.UIFramework.Screen;
using TigerForge;
using TMPro;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ScreenController.Popup.EndGame.Widget
{
    [DefaultExecutionOrder(1000)]
    public class RewardBattleInfoPanel : MonoBehaviour
    {
        [SerializeField, Space] private TMP_Text totalRewardText;
        [SerializeField] private TMP_Text damageRewardText;
        [SerializeField] private TMP_Text lastHitRewardText;

        [SerializeField] private FragmentItemCoreView itemFragment;
        [SerializeField] private PvpTicketCoreView pvpTicket;

        [SerializeField, Space] private List<GameObject> dropItemFragmentObjects;
        [SerializeField] private List<GameObject> dropPvPTicketObjects;
        [SerializeField] private List<GameObject> dropTokenObjects;

        [SerializeField] [Space] private Button backButton;


        private void Awake()
        {
            backButton.onClick.AddListener(BackToPvpScreen);
        }

        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            var endGameResponse = EndGameServerService.Response;
            if (endGameResponse.IsError) return;

            var selfInfo = endGameResponse.data.GetSelfInfo();

            var haveDropItemFragment = endGameResponse.data.HaveDropItemFragment();
            var haveDropPvPTicket = endGameResponse.data.HaveDropPvPTicket();

            dropItemFragmentObjects.ForEach(@object => @object.SetActive(haveDropItemFragment));
            dropPvPTicketObjects.ForEach(@object =>
                @object.SetActive(haveDropItemFragment is false && haveDropPvPTicket));
            dropTokenObjects.ForEach(@object =>
                @object.SetActive(haveDropItemFragment is false && haveDropPvPTicket is false));

            SetAddText(lastHitRewardText, selfInfo.rewardGFRTokenOnLastHit);

            switch (haveDropItemFragment)
            {
                case true:
                    itemFragment.UpdateView(new FragmentItemInfo(selfInfo.rewardFragment));
                    break;
                case false when haveDropPvPTicket:
                    pvpTicket.UpdateView(new PvpTicket()
                    {
                        quantity = endGameResponse.data.GetSelfInfo().rewardNumberPVPTicket,
                    });
                    NetworkService.Instance.services.login.AddPvpTicket(endGameResponse.data.GetSelfInfo()
                        .rewardNumberPVPTicket);
                    break;
                default:
                    SetAddText(damageRewardText, selfInfo.rewardGFRTokenOnDamage);
                    SetAddText(totalRewardText, selfInfo.TotalToken);
                    break;
            }
        }
        
        private void BackToPvpScreen()
        {
            var data = EventManager.GetData(EventName.Client.Battle.BattleMode);
            if(data is null || (BattleMode)data != BattleMode.PvP) return;
            
            EventManager.EmitEventData(EventName.UI.RequestScreen(), new ScreenRequest()
            {
                action = ScreenAction.Open,
                screenID = ScreenID.PvPInfoPopup,
            });
        }

        private void SetAddText(TMP_Text textMesh, object numberAdd)
        {
            textMesh.text = GetFormattedAddNumber(numberAdd);
        }

        private string GetFormattedAddNumber(object numberAdd)
        {
            return $"+ {numberAdd:N0}";
        }
    }
}