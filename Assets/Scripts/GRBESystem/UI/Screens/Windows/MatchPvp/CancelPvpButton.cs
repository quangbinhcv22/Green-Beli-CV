using GEvent;
using Network.Messages;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.MatchPvp
{
    [RequireComponent(typeof(Button))]
    public class CancelPvpButton : MonoBehaviour
    {
        [SerializeField] private Button button;


        private void Awake()
        {
            button ??= GetComponent<Button>();
            button.onClick.AddListener(CancelPvpRequest);
        }

        private void OnEnable()
        {
            button.interactable = true;
            EventManager.StartListening(EventName.Server.StartGame,()=>
            {
                button.interactable = false;
            });
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.StartGame,()=>
            {
                button.interactable = false;
            });
        }

        private void CancelPvpRequest()
        {
            if(NetworkService.Instance.IsNotLogged()) return;
            
            Message.Instance().SetId(EventName.Server.CancelPlayPvp).Send();
        }
    }
}
