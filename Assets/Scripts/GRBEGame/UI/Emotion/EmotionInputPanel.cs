using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using GEvent;
using GRBESystem.Definitions;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.Emotion
{
    public class EmotionInputPanel : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private EmotionCellView cellViewTemplate;
        [SerializeField] private EmotionArtSet artSet;

        private List<string> _allEmotionIds;
        private List<EmotionCellView> _emotionCellViews = new List<EmotionCellView>();


        private void Awake()
        {
            _allEmotionIds = artSet.emotions.customPairs.Select(pair => pair.key).ToList();

            EventManager.StartListening(EventName.Server.ChatInGame, OnChatResponse);
        }

        private void OnEnable()
        {
            LoadInput();
        }

        private void LoadInput()
        {
            if (_allEmotionIds.Count == _emotionCellViews.Count) return;

            foreach (var emotionId in _allEmotionIds)
            {
                var newEmotion = Instantiate(cellViewTemplate, content);
                newEmotion.UpdateView(emotionId);

                _emotionCellViews.Add(newEmotion);
            }
        }

        private void OnChatResponse()
        {
            var chatResponse = NetworkService.Instance.services.chatInGame.Response;
            if (chatResponse.IsError) return;

            var isSelfChat = chatResponse.data.Player() is Owner.Self;
            if (isSelfChat is false) return;

            gameObject.SetActive(false);
        }
    }
}