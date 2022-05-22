using GEvent;
using Network.Service;
using TigerForge;
using UIFlow;
using UnityEngine;

namespace GRBEGame.UI.Nation
{
    public class SetNationSuccessHandler : MonoBehaviour
    {
        [SerializeField] private UIObject ui;
        [SerializeField] private bool haveAnimation = true;
        [SerializeField] [Space] private UIRequest screenSuccessRequest;
        
        
        private void OnEnable()
        {
            EventManager.StartListening(EventName.Server.SetNation, OnSetNation);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.SetNation, OnSetNation);
        }

        private void OnSetNation()
        {
            if(NetworkService.Instance.IsNotLogged() || NetworkService.Instance.services.setNation.Response.IsError)
                return;
            
            ui.Close(haveAnimation);
            screenSuccessRequest.SendRequest();
        }
    }
}
