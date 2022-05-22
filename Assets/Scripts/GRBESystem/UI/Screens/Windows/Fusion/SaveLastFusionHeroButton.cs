using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.Fusion
{
    [RequireComponent(typeof(Button))]
    public class SaveLastFusionHeroButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(SaveLastFusionHero);
        }

        private void SaveLastFusionHero()
        {
            var fusionHeroId = EventManager.GetData<List<long>>(EventName.PlayerEvent.FusionHeroes).First();
            var fusionHero = NetworkService.Instance.services.getHeroList.HeroResponses.GetHeroInfo(fusionHeroId);
            
            EventManager.EmitEventData(EventName.PlayerEvent.LastFusionHero, fusionHero);
        }
    }
}