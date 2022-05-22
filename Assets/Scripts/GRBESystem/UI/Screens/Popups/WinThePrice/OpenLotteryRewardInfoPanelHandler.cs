using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.WinThePrice
{
    public class OpenLotteryRewardInfoPanelHandler : MonoBehaviour
    {
        [SerializeField] private LotteryRewardInfoPanel lotteryRewardInfoPanelTemplate;
        private readonly List<LotteryRewardInfoPanel> _pooledLotteryRewardInfoPanels = new List<LotteryRewardInfoPanel>();
        private bool _isFirstUpdated;
        
        
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.WinLottery, OnWinLotteryResponse);
        }

        private void OnEnable()
        {
            if (WinLotteryServerService.Response.IsError is false && _isFirstUpdated is false)
                OnWinLotteryResponse();
        }

        private void OnWinLotteryResponse()
        {
            _isFirstUpdated = true;
            
            var response = WinLotteryServerService.Response;
            if(response.IsError) return;

            GetPooledLotteryRewardInfoPanel().UpdateView(response.data);
        }

        private LotteryRewardInfoPanel GetPooledLotteryRewardInfoPanel()
        {
            var pooledPanelIndex = _pooledLotteryRewardInfoPanels.FindIndex(panel => panel.gameObject.activeInHierarchy is false);

            LotteryRewardInfoPanel panelResult;

            if (pooledPanelIndex >= (int)default)
            {
                panelResult = _pooledLotteryRewardInfoPanels[pooledPanelIndex];
                panelResult.gameObject.SetActive(true);
            }
            else
            {
                panelResult = Instantiate(lotteryRewardInfoPanelTemplate, parent: transform);
                panelResult.onDisable += () =>  Invoke( nameof(OnAnyPooledLotteryRewardInfoPanelDisable), 0.15f);
                    
                _pooledLotteryRewardInfoPanels.Add(panelResult);
            }
            
            return panelResult;
        }

        private void OnAnyPooledLotteryRewardInfoPanelDisable()
        {
            var haveAnyPanelActive = _pooledLotteryRewardInfoPanels.Any(panel => panel.gameObject.activeInHierarchy);
            gameObject.SetActive(haveAnyPanelActive);
        }
    }
}