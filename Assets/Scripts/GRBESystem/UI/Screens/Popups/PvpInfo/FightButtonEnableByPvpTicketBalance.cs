using GEvent;
using Manager.Inventory;
using Network.Service;
using Spine.Unity;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    [RequireComponent(typeof(Button))]
    public class FightButtonEnableByPvpTicketBalance : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private SkeletonGraphic skeletonGraphic;
        [SerializeField] private Color enableColor;
        [SerializeField] private Color unEnableColor;


        private void Awake() => button ??= GetComponent<Button>();

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Inventory.Change, UpdateViewByTimeLimit);
            EventManager.StartListening(EventName.UI.Select<EnablePvpFight>(), UpdateViewByTimeLimit);
            EventManager.StartListening(EventName.Client.Battle.PvpRoom, UpdateViewByTimeLimit);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Inventory.Change, UpdateViewByTimeLimit);
            EventManager.StopListening(EventName.UI.Select<EnablePvpFight>(), UpdateViewByTimeLimit);
            EventManager.StopListening(EventName.Client.Battle.PvpRoom, UpdateViewByTimeLimit);
        }

        private void EnableButton(bool enable)
        {
            button.interactable = enable;
            skeletonGraphic.color = enable ? enableColor : unEnableColor;
            skeletonGraphic.freeze = enable is false;
        }

        private void UpdateViewByTimeLimit()
        {
            var enablePvpFight = EventManager.GetData<EnablePvpFight>(EventName.UI.Select<EnablePvpFight>());
            EnableButton(enablePvpFight.enable);

            if (enablePvpFight.enable) UpdateView();
        }

        private void UpdateView()
        {
            var pvpRoom = EventManager.GetData(EventName.Client.Battle.PvpRoom);
            if (pvpRoom is null || NetworkService.Instance.IsNotLogged())
            {
                EnableButton(default);
                return;
            }

            var isEnable = NetworkService.playerInfo.inventory.GetMoney(MoneyType.PvpTicket) >= (int) pvpRoom;
            EnableButton(isEnable);
        }
    }
}