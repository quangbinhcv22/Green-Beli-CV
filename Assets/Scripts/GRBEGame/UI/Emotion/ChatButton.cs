using System;
using System.Collections;
using GEvent;
using GRBESystem.Definitions;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Emotion
{
    [RequireComponent(typeof(Button))]
    public class ChatButton : MonoBehaviour
    {
        [SerializeField] private float delaySeconds = 2f;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            EventManager.StartListening(EventName.Server.ChatInGame, OnChatResponse);
        }

        private void OnEnable()
        {
            _button.interactable = true;
        }

        private void OnChatResponse()
        {
            var chatResponse = NetworkService.Instance.services.chatInGame.Response;
            if (chatResponse.IsError) return;

            var isSelfChat = chatResponse.data.Player() is Owner.Self;
            if (isSelfChat) StartCoroutine(DeInteractableOnSeconds());
        }

        private IEnumerator DeInteractableOnSeconds()
        {
            _button.interactable = false;

            yield return new WaitForSeconds(delaySeconds);

            _button.interactable = true;
        }
    }
}