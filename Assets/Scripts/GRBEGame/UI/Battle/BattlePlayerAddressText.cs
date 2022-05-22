using Config.Format;
using GRBESystem.Definitions;
using Network.Service;
using Network.Service.Implement;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.Battle
{
    public class BattlePlayerAddressText : MonoBehaviour
    {
        [SerializeField] private Owner owner;
        [SerializeField] private TMP_Text text;
        [SerializeField] private PlayerAddressFormatConfig formatConfig;

        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            var startGameResponse = StartGameServerService.Response;
            if (startGameResponse.IsError) return;

            var address = startGameResponse.data.GetPlayerInfo(owner).id;
            text.SetText(formatConfig.FormattedAddress(address));
        }
    }
}