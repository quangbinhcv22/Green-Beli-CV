using Cysharp.Threading.Tasks;
using GEvent;
using Log;
using Network.Messages;
using Network.Service;
using Network.Web3;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Widget.Connect
{
    [RequireComponent(typeof(Button))]
    public class ConnectWalletButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private bool _agreedTerm;
        private Web3Login _webLogin;


        private void Awake()
        {
            button.onClick.AddListener(() => ConnectWallet().Forget());
            EventManager.StartListening(EventName.Client.Login.TermAgreed, AgreedTerm);
        }

        private void OnEnable()
        {
            if (TryToCheckAgreeTerm()) AgreedTerm();
        }

        private async UniTaskVoid ConnectWallet()
        {
            // if (_agreedTerm == false)
            // {
            //     EventManager.EmitEvent(EventName.Server.GetErrorCaseEventName(ErrorCase.YetNotAgreeTerm));
            //     return;
            // }


            _webLogin ??= new Web3Login();

            var address = await _webLogin.GetAccount();

            NetworkService.playerInfo.address = address;

            NetworkService.Instance.services.genNoneCode.SendRequest(new GenNonCodeRequest(){ address = address});

            GLogger.LogLog("Connect Wallet");
        }

        private bool TryToCheckAgreeTerm()
        {
            return EventManager.GetData(EventName.Client.Login.TermAgreed) != null;
        }

        private void AgreedTerm()
        {
            _agreedTerm = EventManager.GetData<bool>(EventName.Client.Login.TermAgreed);
        }
    }
}