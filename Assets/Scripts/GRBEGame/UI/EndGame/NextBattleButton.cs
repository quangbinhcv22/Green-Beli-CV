using GEvent;
using GRBESystem.UI.Screens.Popups.PvpInfo;
using Manager.Inventory;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UIFlow;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.EndGame
{
    [RequireComponent(typeof(Button))]
    public class NextBattleButton : MonoBehaviour
    {
        [SerializeField] private UIObject resultScreen;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(NextBattle);
        }

        private void OnEnable() => UpdateView();

        private void UpdateView()
        {
            var boxingBattleMode = EventManager.GetData(EventName.Client.Battle.BattleMode);
            if (boxingBattleMode is null) return;
            
            var battleMode = (BattleMode)boxingBattleMode;
            _button.interactable = battleMode != BattleMode.PvP || IsCanPvp();
        }

        private void NextBattle()
        {
            var boxingBattleMode = EventManager.GetData(EventName.Client.Battle.BattleMode);
            if (boxingBattleMode is null) return;
            
            var battleMode = (BattleMode)boxingBattleMode;
            switch (battleMode)
            {
                case BattleMode.PvP when IsCanPvp():
                    PlayPvpServerService.SendRequest(GetRoomFee());
                    if (resultScreen != null)
                        resultScreen.Close();
                    break;
                case BattleMode.PvP when IsCanPvp() is false:
                    NextPvpBattleError();
                    break;
                case BattleMode.PvE:
                    NetworkService.Instance.services.playPve.SendRequest();
                    break;
            }
        }

        private void NextPvpBattleError()
        {
            if(IsCanPvpByTicket() is false)
            {
                EventManager.EmitEvent(EventName.Client.Battle.NextPvpBattleByTicketError);
            }
            else if(IsCanPvpInTimeLimit() is false)
            {
                EventManager.EmitEvent(EventName.Client.Battle.NextPvpBattleInTimeLimitError);
            }
        }

        private bool IsCanPvp()
        {
            var data = EventManager.GetData(EventName.Client.Battle.PvpRoom);
            if (data is null)
                return default;
            return IsCanPvpInTimeLimit() && ((int) data is (int) default || IsCanPvpByTicket());
        }

        private bool IsCanPvpByTicket()
        {
            return NetworkService.playerInfo.inventory.GetMoney(MoneyType.PvpTicket) >= GetPvpTicketRequire();
        }

        private bool IsCanPvpInTimeLimit()
        {
            return NetworkService.Instance.IsNotLogged() is false &&
                   EventManager.GetData<EnablePvpFight>(EventName.UI.Select<EnablePvpFight>()).enable;
        }

        private int GetRoomFee()
        {
            var data = EventManager.GetData(EventName.Client.Battle.PvpRoom);
            if (data is null || (int) data <= (int) default) 
                return default;
            return (int) data;
        }
        
        private int GetPvpTicketRequire()
        {
            return NetworkService.Instance.services.loadGameConfig.GetPvpTicketRequire();
        }
    }
}