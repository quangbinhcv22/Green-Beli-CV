using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentTitleActiveByPVPTicket : MonoBehaviour, IMemberView<PvpTicket>
    {
        [SerializeField] private PvpTicketCoreView coreView;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetActive(default);
        }

        public void UpdateView(PvpTicket data)
        {
            SetActive(data.quantity <= (int) default);
        }

        private void SetActive(bool enable)
        {
            gameObject.SetActive(enable);
        }
    }
}
