using GEvent;
using Manager.Inventory;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.MainHall.Widgets.PlayerInfoPanel
{
    [RequireComponent(typeof(TMP_Text))]
    public class BeLiText : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake() => _text = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            EventManager.StartListening(EventName.Inventory.Change, SetText);
            SetText();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Inventory.Change, SetText);
        }

        private void SetText()
        {
            _text.text = NetworkService.playerInfo.inventory.GetMoney(MoneyType.BeLi).ToString("N0");
        }
    }
}
