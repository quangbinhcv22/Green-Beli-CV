using System;
using Network.Service;
using Network.Service.Implement;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.WinThePrice.Widgets
{
    public class WinThePriceDateText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        private bool _isTextNull;
        
        private void OnEnable()
        {
            UpdateTextView();
        }

        private void UpdateTextView()
        {
            var response = WinLotteryServerService.Response;
            if(response.IsError) return;

            text.SetText(NetworkService.Instance.services.loadGameConfig.Response.data.lottery.GetPreviousLotterySessionDate().ToString("dd/MM/yyyy"));
        }
        
        private void OnValidate()
        {
            _isTextNull = text == null;
            if (_isTextNull) throw new NullReferenceException();
        }
    }
}