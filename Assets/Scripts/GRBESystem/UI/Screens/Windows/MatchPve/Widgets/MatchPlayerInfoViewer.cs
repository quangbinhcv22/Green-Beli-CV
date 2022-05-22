using System.Collections.Generic;
using System.Linq;
using Config.Format;
using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UI.Widget.HeroCard;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.MatchPve.Widgets
{
    public class MatchPlayerInfoViewer : MonoBehaviour
    {
        [SerializeField, Space] private PlayerAddressFormatConfig playerAddressFormatConfig;

        [SerializeField, Space] private HeroVisual heroAvatar;
        [SerializeField] private TMP_Text playerAddressText;
        [SerializeField] private TMP_Text powerHeroTeamText;

        public void ResetViewDefault()
        {
            heroAvatar.UpdateDefault();
            playerAddressText.SetText(string.Empty);

            powerHeroTeamText.SetText(string.Empty);
            powerHeroTeamText.gameObject.SetActive(false);
        }


        public void UpdateView(List<long> selectedHeroIds, string playerAddress)
        {
            heroAvatar.UpdateView(selectedHeroIds.First());
            playerAddressText.SetText(playerAddressFormatConfig.FormattedAddress(playerAddress));

            EventManager.StartListening(EventName.Server.CalculateHeroTeamPower, OnCalculateTeamPowerResponse);
            NetworkService.Instance.services.calculateHeroTeamPower.SendRequest(selectedHeroIds);
        }

        private void OnCalculateTeamPowerResponse()
        {
            powerHeroTeamText.SetText($"{NetworkService.Instance.services.calculateHeroTeamPower.Result:N0}");
            EventManager.StopListening(EventName.Server.CalculateHeroTeamPower, OnCalculateTeamPowerResponse);

            powerHeroTeamText.gameObject.SetActive(true);
        }


        public void UpdateView(List<long> selectedHeroIds, string playerAddress, int heroTeamPower)
        {
            heroAvatar.UpdateView(selectedHeroIds.First());
            playerAddressText.SetText(playerAddressFormatConfig.FormattedAddress(playerAddress));
            powerHeroTeamText.SetText($"{heroTeamPower:N0}");
            powerHeroTeamText.gameObject.SetActive(true);
        }

        public void SetFormattedAddressText(string formattedAddress)
        {
            playerAddressText.SetText(formattedAddress);
        }
    }
}