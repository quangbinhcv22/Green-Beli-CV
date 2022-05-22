using System.Collections.Generic;
using GEvent;
using Manager.Game;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.Breeding.Widgets.BreedingHeroesSelect
{
    public class BreedingHeroEraser : MonoBehaviour
    {
        [SerializeField] private Button eraseButton;
        [SerializeField] private int slotIndex;

        void Awake()
        {
            eraseButton.onClick.AddListener(EraseSlot);
        }

        private void EraseSlot()
        {
            var heroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.BreedingHeroes);
            heroIds[slotIndex] = GameManager.Instance.breedingConfig.noneHeroId;

            EventManager.EmitEventData(EventName.PlayerEvent.BreedingHeroes, data: heroIds);
        }
    }
}