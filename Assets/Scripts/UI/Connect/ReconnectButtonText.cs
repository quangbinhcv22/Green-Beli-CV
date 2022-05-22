using GEvent;
using TigerForge;
using TMPro;
using UnityEngine;

namespace UI.Connect
{
    [RequireComponent(typeof(TMP_Text))]
    public class ReconnectButtonText : MonoBehaviour
    {
        [SerializeField] private string defaultText = "Reconnect";
        [SerializeField] private string failText = "Try again";

        private TMP_Text _connectText;

        private void Awake()
        {
            _connectText = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            _connectText.SetText(defaultText);
            EventManager.StartListening(EventName.Server.Stopped, OnServerStopped);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.Stopped, OnServerStopped);
        }

        private void OnServerStopped()
        {
            _connectText.SetText(failText);
        }
    }
}