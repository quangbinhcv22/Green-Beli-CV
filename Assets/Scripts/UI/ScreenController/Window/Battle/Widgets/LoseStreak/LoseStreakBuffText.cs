using GEvent;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Widgets.LoseStreak
{
    [RequireComponent(typeof(TMP_Text))]
    public class LoseStreakBuffText : MonoBehaviour
    {
        [SerializeField] [Space] private TMP_Text text;
        [SerializeField] private string defaultString = "0%";
        [SerializeField] private string formatString = "40%";

        private bool _isFirstUpdate;
        

        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.LoseTurnBattle, OnLoseStreak);
        }
        
        
        private void OnEnable()
        {
            if (_isFirstUpdate is false)
                OnLoseStreak();
        }

        private const int BuffEffectIndex = 3;

        private void OnLoseStreak()
        {
            var data = EventManager.GetData(EventName.PlayerEvent.LoseTurnBattle);
            if (data is null)
            {
                text.SetText(defaultString);
                return;
            }

            _isFirstUpdate = true;
            var playerLoseStreakInfo = (PlayerLoseStreakInfo) data;
            text.SetText(playerLoseStreakInfo.LoseStreak >= BuffEffectIndex ? formatString : defaultString);
        }

        private void OnValidate()
        {
            text ??= GetComponent<TMP_Text>();
        }
    }
}
