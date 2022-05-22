using System;
using Config.Format;
using Network.Service;
using TMPro;
using UnityEngine;

namespace UI.PlayerInfo
{
    public class WalletAddressText : MonoBehaviour
    {
        [SerializeField] private TMP_Text addressText;
        [SerializeField] private PlayerAddressFormatConfig formatConfig;

        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn)
            {
                addressText.SetText(string.Empty);
                return;
            }

            var address = NetworkService.Instance.services.login.MessageResponse.data.id;
            addressText.SetText(formatConfig.FormattedAddress(address));
        }
    }
}