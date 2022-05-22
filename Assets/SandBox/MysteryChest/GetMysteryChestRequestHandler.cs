using GEvent;
using GNetwork;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace SandBox.MysteryChest
{
    public class GetMysteryChestRequestHandler : MonoBehaviour
    {
        [SerializeField] private string claimFragmentByLuckyPoint;
        private bool _isCalculateMysteryChestRateUpdate;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.ClaimMysteryChestLuckyPoint, OnClaimByLuckyPoint);
            EventManager.StartListening(EventName.Server.OpenMysteryChest, OnOpenMysteryChest);
        }

        private void OnEnable()
        {
            GetMysteryChestInfoRequest();
            CalculateMysteryChestRateRequest();
            
            TimeManager.Instance.AddEvent(UpdateRealTime);
        }

        private void OnDisable()
        {
            if(TimeManager.Instance != null)
                TimeManager.Instance.RemoveEvent(UpdateRealTime);
        }

        private const int SecondUpdate = 3;
        private void UpdateRealTime(int seconds)
        {
            if (gameObject.activeSelf is false || seconds % SecondUpdate != default) return;
            GetMysteryChestInfoRequest();
        }

        private void OnOpenMysteryChest() => GetMysteryChestInfoRequest();

        private void OnClaimByLuckyPoint()
        {
            NetworkApiManager.OnResponse(claimFragmentByLuckyPoint);
            GetMysteryChestInfoRequest();
        }

        private void CalculateMysteryChestRateRequest()
        {
            if (CalculateMysteryChestRateServerService.Response.IsError is false &&
                _isCalculateMysteryChestRateUpdate) return;
            
            _isCalculateMysteryChestRateUpdate = true;
            CalculateMysteryChestRateServerService.SendRequest();
        }

        private void GetMysteryChestInfoRequest()
        {
            GetMysteryChestInfoServerService.SendRequest();
        }
    }
}
