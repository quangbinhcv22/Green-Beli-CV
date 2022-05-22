using System.Collections.Generic;
using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;


namespace GRBESystem.UI.Screens.Panels.LuckyMenConfirm
{
    public class LuckyMenConfirmPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text contentText;

        [SerializeField] [TextArea]
        private string contentFormat = "Are you sure you want to buy {0} ticket{2} with price {1} GRFUITS?";


        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.SelectedTickets, OnContent);
        }

        private void OnEnable()
        {
            OnContent();
        }

        private void OnContent()
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn) return;

            var selectToBuyTickets = EventManager.GetData<List<long>>(EventName.PlayerEvent.SelectedTickets);
            var price = NetworkService.Instance.services.loadGameConfig.Response.data.lottery.price;

            const int singularNoun = 1;
            const string pluralSuffix = "s";

            contentText.SetText(string.Format(contentFormat, selectToBuyTickets.Count, selectToBuyTickets.Count * price,
                selectToBuyTickets.Count is singularNoun ? string.Empty : pluralSuffix));
        }
    }
}