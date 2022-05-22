using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory
{
    public class PvpTicketCoreViewActiveSetter : MonoBehaviour, IMemberView<PvpTicket>
    {
        [SerializeField] private PvpTicketCoreView ownItemCoreView;
        [SerializeField] private bool activeDefault;
        [SerializeField] private bool activeWhenUpdateView = true;


        private void Awake() => ownItemCoreView.AddCallBackUpdateView(this);
        public void UpdateDefault() => SetActive(activeDefault);
        public void UpdateView(PvpTicket data) => SetActive(activeWhenUpdateView);
        private void SetActive(bool enable) => gameObject.SetActive(enable);
    }
}
