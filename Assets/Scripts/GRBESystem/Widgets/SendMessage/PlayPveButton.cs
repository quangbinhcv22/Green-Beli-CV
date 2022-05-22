using Network.Service;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.Widgets.SendMessage
{
    public class PlayPveButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Awake()
        {
            button.onClick.AddListener(SendRequestPlayerPve);
        }

        private void SendRequestPlayerPve()
        {
            NetworkService.Instance.services.playPve.SendRequest();
        }
    }
}