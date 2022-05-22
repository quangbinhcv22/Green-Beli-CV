using EnhancedUI.EnhancedScroller;
using Network.Service;
using Network.Service.Implement;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Emotion
{
    public class EmotionCellView : EnhancedScrollerCellView
    {
        private static readonly Vector3 FlipEuler = new Vector3(default, 180);
        private static readonly Vector3 NoneFlipEuler = Vector3.zero;

        [SerializeField] [Space] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private Image background;
        [SerializeField] private EmotionArtSet artSet;

        private EmotionData _emotionData;
        private string _content;


        public void FlipBackground(bool isFlipped)
        {
            SetBackgroundRotation(isFlipped ? FlipEuler : NoneFlipEuler);

            void SetBackgroundRotation(Vector3 euler) =>
                background.gameObject.transform.rotation = Quaternion.Euler(euler);
        }

        public void FlipIcon(bool isFlipped)
        {
            SetIconRotation(_emotionData.CanFlipped && isFlipped ? FlipEuler : NoneFlipEuler);
            void SetIconRotation(Vector3 euler) => image.gameObject.transform.rotation = Quaternion.Euler(euler);
        }


        private void Awake()
        {
            if (button) button.onClick.AddListener(SelectEmotion);
        }

        private void SelectEmotion()
        {
            var selfAddress = NetworkService.Instance.services.login.MessageResponse.data.id;
            var request = new ChatInGameRequest { text = _content, player = selfAddress };

            NetworkService.Instance.services.chatInGame.SendRequest(request);
        }


        public void UpdateView(string newContent)
        {
            _content = newContent;
            _emotionData = artSet.emotions[_content];

            if (image) image.sprite = _emotionData.art;
            if (background) background.sprite = artSet.emotionBackgrounds[_emotionData.emotionType];
        }
    }
}