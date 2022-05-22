using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.MatchPvp
{
    public class ActiveBasedPlayPvpEvent : MonoBehaviour
    {
        [SerializeField] private bool isActiveOnSuccess;
        [SerializeField] private bool isActiveOnCancel;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.PlayPvp, OnPlayPvp);
            EventManager.StartListening(EventName.Server.CancelPlayPvp, OnCancelPlayPvp);
            
            OnPlayPvp();
        }

        private void OnPlayPvp()
        {
            var somethingWentWrong = PlayPvpServerService.Response.IsError;
            SetActive(somethingWentWrong ? default : isActiveOnSuccess);
        }

        private void OnCancelPlayPvp()
        {
            SetActive(isActiveOnCancel);
        }


        private void SetActive(bool isActive) => gameObject.SetActive(isActive);
    }
}