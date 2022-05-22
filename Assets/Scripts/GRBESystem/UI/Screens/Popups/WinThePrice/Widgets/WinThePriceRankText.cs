using Network.Service;
using Network.Service.Implement;
using QB.Collection;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.WinThePrice.Widgets
{
    public class WinThePriceRankText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private DefaultableDictionary<int, string> rankTextFormats;


        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            var response = WinLotteryServerService.Response;
            if (response.IsError) return;

            text.SetText(rankTextFormats[response.data.winOrder]);
        }
    }
}