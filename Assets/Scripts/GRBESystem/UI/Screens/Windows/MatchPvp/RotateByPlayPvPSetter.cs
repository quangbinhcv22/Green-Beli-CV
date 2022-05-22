using System;
using GEvent;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.MatchPvp
{
    public class RotateByPlayPvPSetter : MonoBehaviour
    {
        [SerializeField] private Rotate rotate;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.PlayPvp, OnPlayPvp);
            EventManager.StartListening(EventName.Server.CancelPlayPvp, OnCancelPlayPvP);
        }

        private void OnEnable() => OnPlayPvp();
   
        private void OnPlayPvp()
        {
            if(PlayPvpServerService.Response.IsError) return;
            rotate.StartRotate();
        }

        private void OnCancelPlayPvP() => rotate.StopRotate();
    }
}
