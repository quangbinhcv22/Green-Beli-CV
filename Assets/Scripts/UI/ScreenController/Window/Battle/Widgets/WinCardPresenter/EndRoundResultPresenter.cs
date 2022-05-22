using System.Collections.Generic;
using GEvent;
using Network.Messages.AttackBoss;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UI.ScreenController.Panel.SelectCard.Selector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ScreenController.Window.Battle.Widgets.WinCardPresenter
{
    public class EndRoundResultPresenter : MonoBehaviour
    {
        [SerializeField] private CardIndexConfig cardConfig;
        [SerializeField] private TMP_Text statusText;
        [SerializeField, Space] private List<Image> cardImages;
        [SerializeField, Space] private List<Transform> cardContents;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.AttackBoss, () =>
                Invoke(nameof(Present), AttackBossServerService.delayResponseCallbackConfig.showCardWin));

            Hide();
        }

        private void Present()
        {
            var attackBossResponse = AttackBossServerService.Response.data;
            var isRoundDraw = attackBossResponse.IsRoundDraw();

            var maxCardCount = 2;
            var minCardCount = 1;
            
            var cardToShow = isRoundDraw ? maxCardCount : minCardCount;

            statusText.SetText(attackBossResponse.IsRoundDraw() ? $"Draw Card" : $"Win Card");

            var winCard = attackBossResponse.GetCardWin();

            for (int i = 0; i < maxCardCount; i++)
            {
                cardContents[i].gameObject.SetActive( i < cardToShow);
                cardImages[i].sprite = cardConfig.GetCardSprite(winCard);
            }
            

            Show();
            Invoke(nameof(Hide), AttackBossServerService.delayResponseCallbackConfig.hideCardWin);
        }


        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}