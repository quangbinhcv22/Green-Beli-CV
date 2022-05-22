using System.Collections.Generic;
using GEvent;
using GRBESystem.UI.Screens.Panels.SelectCard.Widgets.CardToSelect;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Panels.SelectCard
{
    public class SelectCardPanel : MonoBehaviour
    {
        [SerializeField] private List<BattleCard> cards;

        private void OnEnable()
        {
            OnStartRound();
        }

        private void OnStartRound()
        {
            var response = StartRoundServerService.Response;
            if (response.IsError) return;

            ResetCardSelection();
            ReloadCards(response.data.cardNumbers);
        }

        private void ResetCardSelection()
        {
            EventManager.EmitEventData(EventName.Select.BattleCard, null);
        }

        private void ReloadCards(List<int> cardsToSelect)
        {
            cards.ForEach(card => card.gameObject.SetActive(false));

            for (int i = 0; i < cardsToSelect.Count; i++)
            {
                cards[i].gameObject.SetActive(true);
                cards[i].SetIndex(cardsToSelect[i]);
            }
        }
    }
}