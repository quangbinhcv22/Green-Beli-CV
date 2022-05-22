using System.Collections.Generic;
using GEvent;
using Manager.Game;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.Fusion
{
    public class FusionHeroSelectEraser : MonoBehaviour
    {
        [SerializeField] private Button eraseButton;
        [SerializeField] private int slotIndex;

        private void Awake()
        {
            eraseButton.onClick.AddListener(EraseSlot);
        }

        private void EraseSlot()
        {
            var heroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.FusionHeroes);
            heroIds[slotIndex] = GameManager.Instance.selectHeroConfig.nonHeroId;

            EventManager.EmitEventData(EventName.PlayerEvent.FusionHeroes, data: heroIds);
        }
    }
}
