using System.Collections.Generic;
using GEvent;
using Manager.Game;
using Network.Service;
using Network.Web3;
using TigerForge;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.Fusion.Input
{
    public class FusionButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(SendMessageFusion);
        }

        private void SendMessageFusion()
        {
            var selectedHeroIds = EventManager.GetData<List<long>>(EventName.PlayerEvent.FusionHeroes);
            var selectHeroConfig = GameManager.Instance.selectHeroConfig;

            Assert.IsTrue(selectHeroConfig.IsFullSelection(selectedHeroIds, selectHeroConfig.StandardFusionHeroCount));
            
            Web3Controller.Instance.GreenHero.FusionHero(selectedHeroIds[0], selectedHeroIds[1]);
        }
    }
}