using System.Collections.Generic;
using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.PvEReward.Widgets
{
    public class PvERewardInfoPanel : MonoBehaviour
    {
        private const string LoadGameConfigServerEvent = EventName.Server.LoadGameConfig;

        [SerializeField] private List<TMP_Text> starOfHeroTexts;
        [SerializeField] private List<TMP_Text> tokenRewardTexts;
        
        private bool _isHadContent;
        
        
        private void OnEnable()
        {
            if(_isHadContent) return;
            
            UpdateView();
            EventManager.StartListening(LoadGameConfigServerEvent, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(LoadGameConfigServerEvent, UpdateView);
        }

        private void UpdateView()
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError) return;

            var dataRewardTokens = loadGameResponse.data.pve.GetRewardGFruit();
            if(dataRewardTokens == null) return;

            tokenRewardTexts[default].text = $"{int.Parse(dataRewardTokens.star1):N0}";
            tokenRewardTexts[1].text = $"{int.Parse(dataRewardTokens.star2):N0}";
            tokenRewardTexts[2].text = $"{int.Parse(dataRewardTokens.star3):N0}";
            tokenRewardTexts[3].text = $"{int.Parse(dataRewardTokens.star4):N0}";
            tokenRewardTexts[4].text = $"{int.Parse(dataRewardTokens.star5):N0}";
            tokenRewardTexts[5].text = $"{int.Parse(dataRewardTokens.star6):n0}";

            for (var i = 0; i < 6; i++)
                starOfHeroTexts[i].text = $"{i + 1}";

            _isHadContent = true;
        }
    }
}
