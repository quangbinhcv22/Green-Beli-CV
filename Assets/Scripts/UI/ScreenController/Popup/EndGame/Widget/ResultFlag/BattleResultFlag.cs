using GEvent;
using Network.Service;
using Network.Service.Implement;
using Service.Server.EndGame;
using TigerForge;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ScreenController.Popup.EndGame.Widget.ResultFlag
{
    public class BattleResultFlag : MonoBehaviour
    {
        [SerializeField, Space] private Image flagImage;
        [SerializeField] private ResultFlagArtSet resultFlagArtSet;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.EndGame, UpdateView);
        }

        private void UpdateView()
        {
            SetFlagImage(EndGameServerService.GetClientData().GetResultBattle() switch
            {
                EndGameClientData.ResultBattle.Lose => resultFlagArtSet.lose,
                _ => resultFlagArtSet.victory,
            });
        }

        private void SetFlagImage(Sprite sprite)
        {
            flagImage.sprite = sprite;
            flagImage.SetNativeSize();
        }
    }
}