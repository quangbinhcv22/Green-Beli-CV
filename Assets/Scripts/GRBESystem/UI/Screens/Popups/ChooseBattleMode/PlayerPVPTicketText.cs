using System;
using GEvent;
using Manager.Inventory;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.ChooseBattleMode
{
    [RequireComponent(typeof(TMP_Text))]
    public class PlayerPVPTicketText : MonoBehaviour
    {
        [SerializeField] private string defaultValue = "-----";
        private TMP_Text _text;


        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _text.SetText(defaultValue);
        }

        private void OnEnable()
        {
            EventManager.StartListening(EventName.Inventory.Change, UpdateView);
            UpdateView();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Inventory.Change, UpdateView);
        }

        private void UpdateView()
        {
            if (NetworkService.Instance.IsNotLogged())
            {
                _text.SetText(defaultValue);
                return;
            }

            _text.SetText($"{NetworkService.playerInfo.inventory.GetMoney(MoneyType.PvpTicket):N0}");
        }
    }
}