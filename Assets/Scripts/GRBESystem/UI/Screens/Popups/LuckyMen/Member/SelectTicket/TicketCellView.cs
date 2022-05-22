using EnhancedUI.EnhancedScroller;
using UnityEngine.Events;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    public class TicketCellView : EnhancedScrollerCellView
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<long> _onUpdateView;

        public void UpdateView(long heroId)
        {
            _onUpdateView?.Invoke(heroId);
        }

        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void AddCallBackUpdateView(ITicketCellViewMember member)
        {
            _onUpdateDefault += member.UpdateDefault;
            _onUpdateView += member.UpdateView;
        }
    }

    public interface ITicketCellViewMember
    {
        void UpdateDefault();
        void UpdateView(long heroId);
    }
}