using System.Collections.Generic;
using System.Linq;
using GEvent;
using Manager.Game;
using Network.Service;
using TigerForge;
using UI.Widget.Reflector;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.TeamHero.Member.ClearButton
{
    [RequireComponent(typeof(Button))]
    public class ClearSelectedHeroTeamButton : MonoBehaviour
    {
        private static ServerServiceGroup ServerServices => NetworkService.Instance.services;

        [SerializeField] private Button clearButton;
        [SerializeField] private ButtonInteractReflector buttonInteractReflector;


        private void Awake()
        {
            clearButton.onClick.AddListener(ClearSelectedHeroes);
            buttonInteractReflector.SetInteractCondition(HaveHeroSelected);
        }

        private static void ClearSelectedHeroes()
        {
            var selectConfig = GameManager.Instance.selectHeroConfig;
            EventManager.EmitEventData(EventName.PlayerEvent.BattleHeroes, selectConfig.CreateNonHeroList(selectConfig.StandardBattleHeroCount));
            
            // NetworkService.Instance.services.selectHero.SendRequestEmpty();
        }

        private bool HaveHeroSelected()
        {
            var nullableSelectedHero = EventManager.GetData<List<long>>(EventName.PlayerEvent.BattleHeroes);
            var selectedHero = nullableSelectedHero.Where(IsHeroIdValid).ToList();
            
            return selectedHero.Any();
        }

        private bool IsHeroIdValid(long heroId)
        {
            return GameManager.Instance.selectHeroConfig.IsNotHero(heroId) == false;
        }
        
        
        private void OnEnable()
        {
            if (ServerServices.login.IsLoggedIn == false) return;

            EventManager.StartListening(EventName.Server.GetListHero, OnSelectBattleHeroes);
            EventManager.StartListening(EventName.PlayerEvent.BattleHeroes, OnSelectBattleHeroes);
            
            OnSelectBattleHeroes();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetListHero, OnSelectBattleHeroes);
            EventManager.StopListening(EventName.PlayerEvent.BattleHeroes, OnSelectBattleHeroes);
        }

        private void OnSelectBattleHeroes()
        {
            buttonInteractReflector.ReflectInteract();
        }
    }
}