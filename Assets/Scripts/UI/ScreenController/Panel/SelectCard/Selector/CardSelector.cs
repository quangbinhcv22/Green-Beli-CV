using System.Collections.Generic;
using GEvent;
using Network.Messages.StartRound;
using TigerForge;
using UnityEngine;

namespace UI.ScreenController.Panel.SelectCard.Selector
{
    public class CardSelector : MonoBehaviour
    {
        // [SerializeField] private List<Card> selectCards;
        // [SerializeField] private CardIndexConfig cardIndexConfig;
        //
        // protected void Awake()
        // {
        //     EventManager.StartListening(EventName.Server.START_ROUND, ShowSelectionSession);
        // }
        //
        // private void OnEnable()
        // {
        //     foreach (var selectCard in this.selectCards)
        //     {
        //         selectCard.gameObject.SetActive(false);
        //     }
        // }
        //
        // private void ShowSelectionSession()
        // {
        //     ResetIndexSelected();
        //
        //     var startRoundResponse = EventManager.GetData<StartRoundResponse>(EventName.Server.START_ROUND);
        //     int[] cardsToSelect = startRoundResponse.cardNumbers.ToArray();
        //
        //     foreach (var selectCard in this.selectCards)
        //     {
        //         selectCard.gameObject.SetActive(false);
        //     }
        //
        //     for (var i = 0; i < cardsToSelect.Length; i++)
        //     {
        //         var cardNumber = cardsToSelect[i];
        //
        //         selectCards[i].SetSelectIndex(cardNumber);
        //         selectCards[i].gameObject.SetActive(true);
        //         selectCards[i].FlipOpenTo(cardIndexConfig.GetCardSprite(cardNumber));
        //     }
        // }
        //
        // private void ResetIndexSelected()
        // {
        //     const int noneIndexSelect = 0;
        //     EventManager.EmitEventData(EventName.ScreenEvent.Battle.SELECTING_CARD, data: noneIndexSelect);
        // }
    }
}