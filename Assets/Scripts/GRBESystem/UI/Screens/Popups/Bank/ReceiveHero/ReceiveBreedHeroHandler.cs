using GEvent;
using Network.Service.Implement;
using TigerForge;
using UI.Widget.HeroCard;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank.ReceiveHero
{
    public class ReceiveBreedHeroHandler : MonoBehaviour
    {
        [SerializeField] private HeroVisual heroVisual;
        private bool _isFirstUpdated;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.BreedingSuccess, OnBreedingSuccess);
        }

        private void OnEnable()
        {
            if (_isFirstUpdated) 
                return;
            
            OnBreedingSuccess();
        }

        private void OnBreedingSuccess()
        {
            if (BreedingSuccessServerService.Response.IsError) return;
            
            heroVisual.UpdateView(BreedingSuccessServerService.Response.data.hero);
            _isFirstUpdated = true;
        }
    }
}