using System;
using GEvent;
using Network.Controller;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Connect
{
    [RequireComponent(typeof(Button))]
    public class ReconnectButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Reconnect);
        }

        private static void Reconnect()
        {
            WebSocketController.Instance.Connect();
        }
    }
}