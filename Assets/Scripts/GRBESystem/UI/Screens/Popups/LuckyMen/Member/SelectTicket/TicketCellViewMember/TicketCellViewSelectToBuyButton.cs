using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket.TicketCellViewMember
{
    [RequireComponent(typeof(Button))]
    public class TicketCellViewSelectToBuyButton : MonoBehaviour, ITicketCellViewMember
    {
        [SerializeField] private TicketCellView owner;
        [SerializeField] private long ticketId;

        private void Awake()
        {
            owner.AddCallBackUpdateView(this);
            GetComponent<Button>().onClick.AddListener(SelectToBuy);
        }

        public void UpdateDefault()
        {
        }

        public void UpdateView(long heroId)
        {
            ticketId = heroId;
        }

        private void SelectToBuy()
        {
            EventManager.EmitEventData(EventName.PlayerEvent.SelectTicket, data: ticketId);
        }
    }
}