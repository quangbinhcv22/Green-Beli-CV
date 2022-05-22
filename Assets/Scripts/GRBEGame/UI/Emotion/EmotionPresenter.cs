using System.Collections;
using System.Collections.Generic;
using GEvent;
using GRBESystem.Definitions;
using Network.Service.Implement;
using QB.Collection;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.Emotion
{
    public class EmotionPresenter : MonoBehaviour
    {
        [SerializeField] private DefaultableDictionary<Owner, PresentEmotionConfig> presentConfigs;
        [SerializeField] private EmotionCellView emotionTemplate;
        [SerializeField] private Transform content;
        [SerializeField] private float delayHide = 2f;

        private readonly List<EmotionCellView> _pooledEmotion = new List<EmotionCellView>();
        private readonly List<Owner> _showingPlayer = new List<Owner>();

        private EmotionCellView GetPooledEmotion()
        {
            var emotion = _pooledEmotion.Find(emotion => emotion.gameObject.activeInHierarchy is false);

            if (emotion is null)
            {
                emotion = Instantiate(emotionTemplate, parent: content);
                _pooledEmotion.Add(emotion);
            }

            emotion.gameObject.SetActive(true);
            return emotion;
        }


        private void OnEnable()
        {
            ResetPresent();
            EventManager.StartListening(EventName.Server.ChatInGame, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.ChatInGame, UpdateView);
        }

        private void ResetPresent()
        {
            _showingPlayer.Clear();
            _pooledEmotion.ForEach(emotion => emotion.gameObject.SetActive(false));
        }

        private void UpdateView()
        {
            var boxedEmotionRequest = EventManager.GetData(EventName.Server.ChatInGame);
            if (boxedEmotionRequest is null) return;

            var emotionRequest = (ChatInGameResponse)boxedEmotionRequest;
            if (_showingPlayer.Contains(emotionRequest.Player()) is false) StartCoroutine(ShowEmotion(emotionRequest));
        }

        private IEnumerator ShowEmotion(ChatInGameResponse response)
        {
            var owner = response.Player();
            _showingPlayer.Add(owner);


            var emotion = GetPooledEmotion();
            var presentConfig = presentConfigs[response.Player()];

            emotion.UpdateView(response.text);

            emotion.gameObject.transform.position = presentConfig.position.transform.position;
            emotion.FlipBackground(presentConfig.isFlipBackground);
            emotion.FlipIcon(presentConfig.isFlipIcon);


            yield return new WaitForSeconds(delayHide);
            emotion.gameObject.SetActive(false);


            _showingPlayer.Remove(owner);
        }
    }

    [System.Serializable]
    public class PresentEmotionConfig
    {
        public RectTransform position;
        public bool isFlipBackground;
        public bool isFlipIcon;
    }
}