using GEvent;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Panels.SelectCard.Widgets.SelectButton
{
    [RequireComponent(typeof(Button))]
    public class SendMessageSelectCardButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SendMessage);

            EventManager.StartListening(EventName.Select.BattleCard, OnSelectCard);
        }

        private void OnEnable()
        {
            OnSelectCard();
        }

        private void OnSelectCard()
        {
            _button.interactable = HaveSelectCard();
        }

        private void SendMessage()
        {
            var nullableIndex = EventManager.GetData(EventName.Select.BattleCard);
            if (nullableIndex is int index) SelectCardServerService.SendRequest(index);
        }

        private bool HaveSelectCard()
        {
            return EventManager.GetData(EventName.Select.BattleCard) is int;
        }
    }
}