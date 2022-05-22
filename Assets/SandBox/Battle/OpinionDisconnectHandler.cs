using System.Collections.Generic;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace SandBox.Battle
{
    public class OpinionDisconnectHandler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> activeWhenDisconnectObjects;
        [SerializeField] private List<GameObject> inactiveWhenDisconnectObjects;


        private void Awake()
            => EventManager.StartListening(EventName.Server.EndGame, UpdateView);

        private void OnEnable() => UpdateView();

        private void UpdateView()
        {
            if(NetworkService.Instance.IsNotLogged() || EndGameServerService.Response.IsError) return;

            activeWhenDisconnectObjects.ForEach(item => item.SetActive(EndGameServerService.Data.IsOpinionQuitPvp()));
            inactiveWhenDisconnectObjects.ForEach(item =>
                item.SetActive(EndGameServerService.Data.IsOpinionQuitPvp() is false));
        }
    }
}
