using System.Collections.Generic;
using GEvent;
using Manager.Game;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Web3;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;


namespace GRBESystem.UI.Screens.Popups.BreedingReconfirm
{
    public class BreedingConfirmPopupController : AGrbeScreenController
    {
        [SerializeField, Space] private Button okButton;

        protected override void OtherActionOnAwake()
        {
            okButton.onClick.AddListener(EmitConfirmEvent);
        }

        protected override void OtherActionOnEnable()
        {
        }

        protected override void OtherActionOnDisable()
        {
        }

        protected override void HandleDataOpenScreenRequest(object data)
        {
        }

        private void EmitConfirmEvent()
        {
            var breedingHeroes = EventManager.GetData<List<long>>(EventName.PlayerEvent.BreedingHeroes);

            if (breedingHeroes is null) return;

            var breedingConfig = GameManager.Instance.breedingConfig;
            if (breedingHeroes.Count < breedingConfig.heroesNumberRequire ||
                breedingHeroes.Contains(breedingConfig.noneHeroId)) return;
            
            Web3Controller.Instance.GreenHero.BreedHero(breedingHeroes[0], breedingHeroes[1]);
        }
    }
}