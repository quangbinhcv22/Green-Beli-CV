using System.Collections.Generic;
using System.Linq;
using GEvent;
using Manager.Game;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.Breeding.Widgets.OpenBreededHero
{
    public class OpenBreededHeroButton : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.BreedingSuccess, () => SetActive(true));
            EventManager.StartListening(EventName.PlayerEvent.BreedingHeroes, OnSelectBreedingHero);
        }

        private void SetActive(bool isActive)
        {
            if (isActiveAndEnabled == isActive) return;
            gameObject.SetActive(isActive);
        }

        private void OnSelectBreedingHero()
        {
            var breedingHero = EventManager.GetData<List<long>>(EventName.PlayerEvent.BreedingHeroes);
            if(breedingHero.All(heroId => heroId.Equals(GameManager.Instance.breedingConfig.noneHeroId))) return;
            
            gameObject.SetActive(false);
        }
    }
}