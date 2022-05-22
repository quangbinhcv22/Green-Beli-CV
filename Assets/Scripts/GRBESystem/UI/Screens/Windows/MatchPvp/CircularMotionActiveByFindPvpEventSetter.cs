using GEvent;
using GRBESystem.UI.Screens.Windows.Match.Widgets.Motion;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.Serialization;

namespace GRBESystem.UI.Screens.Windows.MatchPvp
{
    [RequireComponent(typeof(CircularMotion))]
    public class CircularMotionActiveByFindPvpEventSetter : MonoBehaviour
    {
        [SerializeField] private CircularMotion circularMotion;
        [SerializeField] private bool isActiveMotionWhenCancelFindPvpEventOnLoad;
        [SerializeField] private bool isActiveMotionWhenPlayPvpEventOnLoad;


        private void Awake()
        {
            circularMotion ??= GetComponent<CircularMotion>();
        }

        private void OnEnable()
        {
            if(NetworkService.Instance.IsNotLogged()) return;
            EventManager.StartListening(EventName.Server.CancelPlayPvp, UpdateState);
            EventManager.StartListening(EventName.Server.PlayPvp, OnPlayPvpEventLoaded);
        }


        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.CancelPlayPvp, UpdateState);
            EventManager.StopListening(EventName.Server.PlayPvp, OnPlayPvpEventLoaded);
        }
        
        private void OnPlayPvpEventLoaded()
        {
            if(NetworkService.Instance.IsNotLogged() || PlayPvpServerService.Response.IsError) return;
            
            if(isActiveMotionWhenPlayPvpEventOnLoad) circularMotion.StartMotion();
            else circularMotion.StopMotion();
        }

        private void UpdateState()
        {
            if(isActiveMotionWhenCancelFindPvpEventOnLoad) circularMotion.StartMotion();
            else circularMotion.StopMotion();
        }
    }
}
