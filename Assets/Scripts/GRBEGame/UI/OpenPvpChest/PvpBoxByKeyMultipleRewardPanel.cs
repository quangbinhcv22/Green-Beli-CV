using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using GRBEGame.Define;
using GRBESystem.UI.Screens.Windows.Leaderboard;
using Manager.Inventory;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.OpenPvpChest
{
    public class PvpBoxByKeyMultipleRewardPanel : MonoBehaviour
    {
        private const int SimpleValue = 1;

        [SerializeField] private PvpTotalRewardFrame pvpTotalRewardFrame;
        [SerializeField] [Space] private GameObject rewardPanel;
        [SerializeField] private PvpChestRewardItem chestItemPrefab;

        private List<PvpChestRewardItem> _chestItems;
        private int _numberClaimedItem;


        private void Awake()
        {
            _chestItems = new List<PvpChestRewardItem>();
        }

        private void OnEnable()
        {
            if(NetworkService.Instance.IsNotLogged()) return;
            
            UpdateView();
            EventManager.StartListening(EventName.Server.OpenPvpBoxRewardEarnKey, UpdateView);
        }

        private void OnDisable()
        {
            HideItems(default);
            EventManager.StopListening(EventName.Server.OpenPvpBoxRewardEarnKey, UpdateView);
        }
        
        private void OnClaimReward(int index)
        {
            var response = NetworkService.Instance.services.openPvpBox.Response;
            if (index >= response.data.Count) return;
            
            _chestItems[index].SetActiveState(PvpRewardActiveState.Claimed);
            switch (response.data[index].type)
            {
                case 0:
                    _chestItems[index].UpdateView(PvpRewardType.GFruit, response.data[index].amount);
                    NetworkService.playerInfo.inventory.AddMoney(MoneyType.GFruit, response.data[index].amount);
                    break;
                default:
                    _chestItems[index].UpdateView(PvpRewardType.Fragment, response.data[index].amount,
                        response.data[index].type);
                    break;
            }

            _numberClaimedItem++;
            if (_numberClaimedItem < response.data.Count) return;

            NetworkService.Instance.services.getRewardHistoryAll.SendRequest();
            EventManager.EmitEvent(EventName.Client.EventPvpKeyUpdate);
            StartCoroutine(ShowPvpTotalRewardFrame());
        }

        private IEnumerator ShowPvpTotalRewardFrame()
        {
            yield return new WaitForSeconds(SimpleValue);
            
            var response = NetworkService.Instance.services.openPvpBox.Response;
            HideItems(default);
            pvpTotalRewardFrame.gameObject.SetActive(true);
            // pvpTotalRewardFrame.UpdateView(GetTotalGFruit(response.data), FragmentType.Land,
            //     GetTotalLandFragment(response.data));
        }

        private int GetTotalGFruit(List<OpenPvpChestResponse> items)
        {
            var total = (int) default;
            items.ForEach(item => { total += item.type == default ? item.amount : default; });
            return total;
        }
        
        private int GetTotalLandFragment(List<OpenPvpChestResponse> items)
        {
            var total = (int) default;
            items.ForEach(item => { total += item.type == (int) FragmentType.Land ? item.amount : default; });
            return total;
        }

        private void UpdateView()
        {
            pvpTotalRewardFrame.gameObject.SetActive(false);
            StartCoroutine(DelayShowReward());
        }

        private IEnumerator DelayShowReward()
        {
            var response = NetworkService.Instance.services.openPvpBox.Response;
            if (response.IsError) yield return default;

            _numberClaimedItem = default;
            yield return new WaitForSeconds(SimpleValue);
            
            UpdateItemList(response.data);
        }

        private void UpdateItemList(List<OpenPvpChestResponse> chestItems)
        {
            if(chestItems.Count <= SimpleValue) return;
            
            var index = (int) default;
            HideItems(index);
            chestItems.ForEach(_ =>
            {
                if (_chestItems.Count <= index)
                {
                    var indexClaimed = index;
                    
                    _chestItems.Add(Instantiate(chestItemPrefab, rewardPanel.transform));
                    _chestItems.Last()
                        .AddOnClickListenerClaimButton(() => OnClaimReward(indexClaimed));
                }
                _chestItems[index].gameObject.SetActive(true);
                _chestItems[index].SetActiveState(PvpRewardActiveState.Claim);
                ++index;
            });
        }

        private void HideItems(int index)
        {
            if(_chestItems.Count == default) return;
            for (var i = index; i < _chestItems.Count; i++)
                _chestItems[i].gameObject.SetActive(false);
        }
    }
}
