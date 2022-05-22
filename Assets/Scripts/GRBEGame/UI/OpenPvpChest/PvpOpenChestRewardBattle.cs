using System.Collections.Generic;
using GEvent;
using GRBEGame.UI.Screen.Inventory.Material;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.OpenPvpChest
{
    public class PvpOpenChestRewardBattle : MonoBehaviour
    {
        [SerializeField] private PvpTotalRewardFrame pvpTotalRewardFrame;
        private bool _isFirstUpdated;

        private void Awake()
        {
            EventManager.StartListening(EventName.Server.EndGame, UpdateView);
        }

        private void OnEnable()
        {
            if (EndGameServerService.Response.IsError is false && _isFirstUpdated is false)
                UpdateView();
        }

        private void UpdateView()
        {
            _isFirstUpdated = true;
            
            var playerInfo = EndGameServerService.Data.GetSelfInfo();
            var fragmentInfo = playerInfo.rewardFragment;
            var fragmentInfos = new List<MaterialInfo>
            {
                new MaterialInfo(new MaterialResponse()
                {
                    number = fragmentInfo.number,
                    type = fragmentInfo.type,
                })
            };
            pvpTotalRewardFrame.UpdateView(playerInfo.rewardGFruit, fragmentInfos);
        }
    }
}
