using GEvent;
using GRBESystem.Definitions;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.EndGame.Pvp
{
    public class ActiveByPvPCaseSetter : MonoBehaviour
    {
        [SerializeField] private GameObject gameObjectSetter;
        [SerializeField] private Owner owner;
        [SerializeField] private bool isEnable = true;


        private void Awake()
            => EventManager.StartListening(EventName.Server.EndGame, UpdateView);

        private void OnEnable() => UpdateView();
        
        private void UpdateView()
        {
            if (NetworkService.Instance.IsNotLogged() || EndGameServerService.Response.IsError) return;

            var isActive = EndGameServerService.Data.HaveDropGFruit(owner) ||
                           EndGameServerService.Data.HaveDropItemFragment(owner);

            gameObjectSetter.SetActive(isActive ? isEnable : isEnable is false);
        }
    }
}
