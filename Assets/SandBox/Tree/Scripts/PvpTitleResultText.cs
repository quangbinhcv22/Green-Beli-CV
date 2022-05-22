using GEvent;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace SandBox.Tree.Scripts
{
    public class PvpTitleResultText : MonoBehaviour
    {
        [SerializeField] private Image title;
        [SerializeField] private Sprite win;
        [SerializeField] private Sprite lose;

        private bool _isFirstUpdated;
    
    
        private void Awake()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.EndGame, UpdateView);
        }
    
        private void OnEnable()
        {
            if(_isFirstUpdated is false)
                UpdateView();
        }

        private void UpdateView()
        {
            if (EndGameServerService.Data.IsOpinionQuitPvp() || EndGameServerService.Data.IsLargestScoreInBattle())
                title.sprite = win;
            else title.sprite = lose;
        
            title.SetNativeSize();
            _isFirstUpdated = true;
        }
    }
}