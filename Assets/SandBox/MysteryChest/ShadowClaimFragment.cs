using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace SandBox.MysteryChest
{
    public class ShadowClaimFragment : MonoBehaviour
    {
        [SerializeField] private Image shadow;
        [SerializeField] private float height;


        private void Awake() => shadow.rectTransform.sizeDelta = new Vector2(shadow.rectTransform.sizeDelta.x, height);

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.GetMysteryChestInfo, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetMysteryChestInfo, UpdateView);
        }

        private void UpdateView()
        {
            shadow.rectTransform.sizeDelta = new Vector2(shadow.rectTransform.sizeDelta.x, GetHeight());
        }

        private float GetHeight()
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError ||
                GetMysteryChestInfoServerService.Response.IsError)
                return height;

            var limitNumber = loadGameResponse.data.mysteryChest.limit_lucky_point_to_claim;
            var number = GetMysteryChestInfoServerService.Data.myLuckyPoint;
            
            return height * (1 - (float) number / limitNumber) >= (float) default
                ? height * (1 - (float) number / limitNumber)
                : default;
        }
    }
}
