using Network.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    [RequireComponent(typeof(Button))]
    public class LeaderboardRefreshButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string refreshString;
        [SerializeField] private int limitSeconds;
        [SerializeField] [Space] private Sprite normalSprite;
        [SerializeField] private Sprite countdownSprite;

        private int _startTimer;
        private bool _isUpdateTimer;
        
        
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(RefreshLeaderboard);
        }

        private void RefreshLeaderboard()
        {
            if (NetworkService.Instance.IsNotLogged()) return;
            NetworkService.Instance.services.getPvpContestDetail.SendRequest();

            _isUpdateTimer = true;
            TimeManager.Instance.AddEvent(UpdateTimeCountdown);
        }

        private void UpdateTimeCountdown(int currentSeconds)
        {
            if (_isUpdateTimer)
            {
                _startTimer = currentSeconds;
                _isUpdateTimer = default;
                
                button.image.sprite = countdownSprite; 
                button.enabled = default; 
            }

            if (currentSeconds - _startTimer > limitSeconds)
            {
                button.image.sprite = normalSprite;
                button.enabled = true;
                text.SetText(refreshString);
                if (TimeManager.Instance)
                    TimeManager.Instance.RemoveEvent(UpdateTimeCountdown);
                
                return;
            }

            var seconds = limitSeconds - currentSeconds + _startTimer;
            text.SetText($"00:{seconds:00}");
        }
    }
}
