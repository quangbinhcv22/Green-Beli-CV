using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket.TicketCellViewMember
{
    public class TicketCellViewIdText : MonoBehaviour, ITicketCellViewMember
    {
        [SerializeField] private TicketCellView owner;

        [SerializeField] private TMP_Text idText;
        [SerializeField] private string idFormat = "{0}";
        [SerializeField] private string stringDefault;

        private void Awake()
        {
            Assert.IsNotNull(idText);

            owner.AddCallBackUpdateView(this);
        }


        public void UpdateDefault()
        {
            idText.SetText(stringDefault);
        }

        public void UpdateView(long heroId)
        {
            idText.SetText(string.Format(idFormat, heroId));
        }
    }
}