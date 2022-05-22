using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    [CreateAssetMenu(fileName = nameof(SelectTicketConfig), menuName = "ScriptableObject/Config/Mechanism/SelectTicket")]
    public class SelectTicketConfig : ScriptableObject
    {
        public int maxTicket = 5;
    }
}