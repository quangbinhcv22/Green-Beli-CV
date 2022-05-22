using GEvent;
using Network.Service.Implement;
using TigerForge;
using UIFlow;
using UnityEngine;
using UnityEngine.UI;

namespace SandBox.Battle
{
    public class BattleResultStatus : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image statusImage;
        [SerializeField] private Sprite loseSprite;
        [SerializeField] private Sprite winSprite;
        
        [Header("ScreenRequest")]
        [SerializeField] private UIRequest resultRequest;
        [SerializeField] private UIRequest rewardRequest;

        private bool _isFirstUpdated;
        

        private void Awake()
        {
            button.onClick.AddListener(ClickAnyWhereToSkip);
            EventManager.StartListening(EventName.Server.EndGame, UpdateView);
        }

        private void OnEnable()
        {
            if(_isFirstUpdated is false)
                UpdateView();
        }

        private void UpdateView()
        {
            if(EndGameServerService.Response.IsError) return;
            _isFirstUpdated = true;
            
            var isWin = EndGameServerService.Data.IsOpinionQuitPvp() ||
                        EndGameServerService.Data.IsLargestScoreInBattle();

            statusImage.sprite = isWin ? winSprite : loseSprite;
            statusImage.SetNativeSize();
        }

        private void ClickAnyWhereToSkip()
        {
            if(EndGameServerService.Data.HaveDropGFruit() || EndGameServerService.Data.HaveDropItemFragment())
                rewardRequest.SendRequest();
            else
                resultRequest.SendRequest();
        }
    }
}
