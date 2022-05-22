using System.Collections.Generic;
using GEvent;
using Manager.Game;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.TeamHero.SelectHero
{
    public class BattleHeroSelectEraser : MonoBehaviour
    {
        [SerializeField] private Button eraseButton;
        [SerializeField] private int slotIndex;

        private void Awake()
        {
            eraseButton.onClick.AddListener(EraseSlot);
        }

        private void EraseSlot()
        {
            var heroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.BattleHeroes);
            heroIds[slotIndex] = GameManager.Instance.selectHeroConfig.nonHeroId;

            EventManager.EmitEventData(EventName.PlayerEvent.BattleHeroes, data: heroIds);
        }
    }
}
