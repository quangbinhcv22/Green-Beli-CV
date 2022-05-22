using System;
using Cysharp.Threading.Tasks;
using GEvent;
using Log;
using Network.Controller;
using Network.Messages.ErrorCase;
using Network.Service;
using Network.Web3;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.Login
{
    [Obsolete]
    public class LoginWindowController : AGrbeScreenController
    {
        [SerializeField, Space] private Button bntConnectWallet;
        [SerializeField] private Toggle agreeTermsToggle;

        // [SerializeField, Space] private Button bntLoginTesting;

        private Web3Login _webLogin;
        private bool _connectedToServer;


        private void Start()
        {
            this.agreeTermsToggle.onValueChanged.AddListener(EmitTermAgreedEvent);
            agreeTermsToggle.isOn = true;

            _connectedToServer = false;
            EventManager.StartListening(EventName.Server.Connect, OnConnect);
        }

        private void EmitTermAgreedEvent(bool isOn)
        {
            EventManager.EmitEventData(EventName.Client.Login.TermAgreed, isOn);
        }

        private async UniTaskVoid ConnectWallet()
        {
            if (agreeTermsToggle.isOn == false)
            {
                EventManager.EmitEvent(EventName.Server.GetErrorCaseEventName(ErrorCase.YetNotAgreeTerm));
                return;
            }


            _webLogin ??= new Web3Login();

            var address = await _webLogin.GetAccount();

            NetworkService.playerInfo.address = address;

            if (_connectedToServer == false)
            {
                WebSocketController.Instance.Connect();
            }

            GLogger.LogLog("Connect Wallet");
        }

        private void OnConnect()
        {
            _connectedToServer = true;
        }


        protected override void OtherActionOnEnable()
        {
        }

        protected override void OtherActionOnDisable()
        {
        }

        protected override void HandleDataOpenScreenRequest(object data)
        {
        }
    }
}