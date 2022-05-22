using UnityEngine;
using UnityEngine.Events;


namespace GRBEGame.UI.Screen.Inventory
{
    public class PvpTicketCoreView : MonoBehaviour, ICoreView<PvpTicket>
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<PvpTicket> _onUpdateView;


        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(PvpTicket data)
        {
            _onUpdateView?.Invoke(data);
        }

        public void AddCallBackUpdateView(IMemberView<PvpTicket> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }

    [System.Serializable]
    public class PvpTicket
    {
        public int quantity;
    }
}