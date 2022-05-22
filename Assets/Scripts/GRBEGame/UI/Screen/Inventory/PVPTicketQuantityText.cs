using TMPro;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory.Fragment
{
    [DefaultExecutionOrder(100)]
    public class PVPTicketQuantityText : MonoBehaviour, IMemberView<PvpTicket>
    {
        [SerializeField] private PvpTicketCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "{0}";
        [SerializeField] private string defaultString;

        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.text = defaultString;
        }

        public void UpdateView(PvpTicket data)
        {
            text.SetText(string.Format(textFormat, data.quantity));
        }
    }
}
