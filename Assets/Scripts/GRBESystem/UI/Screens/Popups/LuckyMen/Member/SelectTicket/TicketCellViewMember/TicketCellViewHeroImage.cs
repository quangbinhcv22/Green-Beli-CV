using UI.Widget.HeroCard;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket.TicketCellViewMember
{
    public class TicketCellViewHeroImage : MonoBehaviour, ITicketCellViewMember
    {
        [SerializeField] private TicketCellView owner;
        [SerializeField] private HeroVisual heroCoreView;

        private void Awake()
        {
            owner.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            heroCoreView.UpdateDefault();
        }

        public void UpdateView(long heroId)
        {
            heroCoreView.UpdateView(heroId);
        }
    }
}