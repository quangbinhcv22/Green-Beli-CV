using GEvent;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.ServerRequest
{
    public class HideWhenArenaResponse : MonoBehaviour
    {
        private void Awake()
            => EventManager.StartListening(EventName.Server.PlayPvp, OnPlayPvP);
        
        private void Hide() => gameObject.SetActive(false);

        private void OnPlayPvP()
        {
            if (PlayPvpServerService.Response.IsError) return;
            Hide();
        }
    }
}