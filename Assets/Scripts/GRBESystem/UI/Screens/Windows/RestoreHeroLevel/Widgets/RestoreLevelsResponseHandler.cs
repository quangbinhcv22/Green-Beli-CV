using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UI.ScreenController.Window.Battle.Mode;
using UI.Widget.Toast;
using UnityEngine;


namespace GRBESystem.UI.Screens.Windows.RestoreHeroLevel
{
    public class RestoreLevelsResponseHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text restoreLevelText;
        [SerializeField] private RestoreLevelsPrice restoreLevelsPrice;
        [SerializeField] private GreenBeliToastDataSet toastDataSet;

        private long _idHeroRestoreLevelsRequested;
        private int _restoreLevel;
        private int _fee;
        
        
        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.ConfirmRestoreLevels, OnConfirmRestoreLevelEvent);
            EventManager.StartListening(EventName.Server.RestoreHeroLevel, OnRestoreLevelsHeroEventReceive);
        }

        private void OnEnable()
        {
            var battleModeData = EventManager.GetData(EventName.Client.Battle.BattleMode);
            if(NetworkService.Instance.IsNotLogged()) return;
            
            var mode = battleModeData is null ? BattleMode.PvE :
                (BattleMode) battleModeData is BattleMode.PvP ? BattleMode.PvP : BattleMode.PvE;
            if(mode is BattleMode.PvE) return;
            
            EventManager.EmitEventData(EventName.Client.Battle.BattleMode, BattleMode.PvE);
            NetworkService.Instance.services.getHeroList.SendRequest();    
        }

        private void OnConfirmRestoreLevelEvent()
        {
            if (EventManager.GetData<bool>(EventName.PlayerEvent.ConfirmRestoreLevels) is false) return;

            _idHeroRestoreLevelsRequested =
                EventManager.GetData<HeroResponse>(EventName.PlayerEvent.SelectHero).GetID();
            _restoreLevel = int.Parse(restoreLevelText.text);
            _fee = (int) restoreLevelsPrice.RestorePrice();
            NetworkService.Instance.services.restoreHeroLevel.SendRequest(new RestoreHeroLevelRequest()
            {
                heroId = _idHeroRestoreLevelsRequested,
                restoredLevel = _restoreLevel
            });
        }

        private void OnRestoreLevelsHeroEventReceive()
        {
            var isResponseError = NetworkService.Instance.services.restoreHeroLevel.Response.IsError;
            if (isResponseError is false) NetworkService.playerInfo.inventory.SubMoney(0, _fee);
            
            EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel,
                isResponseError ? toastDataSet.restoreLevelsHeroFailed : toastDataSet.successfullyRestoreLevelsHero);
        }

        public long GetRestoreLevelsHeroID()
        {
            return _idHeroRestoreLevelsRequested;
        }
    }
}