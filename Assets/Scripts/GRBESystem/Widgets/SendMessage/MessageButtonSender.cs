using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.Widgets.SendMessage
{
    [RequireComponent(typeof(Button))]
    public class MessageButtonSender : MessageSender
    {
        [SerializeField] private Button button;

        private void Awake()
        {
            button.onClick.AddListener(SendMessage);
        }
    }
}
