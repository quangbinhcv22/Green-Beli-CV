using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;


namespace GRBESystem.UI.Screens.Panels.SkipBattleConfirm
{
    public class SkipAllBattleConfirmPanel : MonoBehaviour
    {
        [SerializeField] private Button okButton;
        
        private void Awake()
        {
            okButton.onClick.AddListener(() => EmitSkipAllBattleEvent(true));
        }
        
        private void OnEnable()
        {
            EventManager.StopListening(EventName.Client.Battle.OpenSkipAllBattleConfirmPanel,
                () => gameObject.SetActive(true));
        }

        private void OnDisable()
        {
            EventManager.StartListening(EventName.Client.Battle.OpenSkipAllBattleConfirmPanel,
                () => gameObject.SetActive(true));
        }

        private void EmitSkipAllBattleEvent(bool enable)
        {
            NetworkService.Instance.services.getHeroList.SendRequest();
            EventManager.StartListening(EventName.Server.GetListHero, SkipAllGame);
        }

        private void SkipAllGame()
        {
            EventManager.StopListening(EventName.Server.GetListHero, SkipAllGame);
            SkipAllGameServerService.SendRequest();
        }
    }
}
