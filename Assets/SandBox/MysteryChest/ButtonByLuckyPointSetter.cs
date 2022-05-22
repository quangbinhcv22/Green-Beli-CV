using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace SandBox.MysteryChest
{
    [RequireComponent(typeof(Button))]
    public class ButtonByLuckyPointSetter : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TypeSetter stage;


        private void Awake() => button ??= GetComponent<Button>();

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
            switch (stage)
            {
                case TypeSetter.Active:
                    button.gameObject.SetActive(IsCanClaim());
                    break;
                case TypeSetter.Interactable:
                    button.interactable = IsCanClaim();
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
        }

        private bool IsCanClaim()
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError ||
                GetMysteryChestInfoServerService.Response.IsError)
                return false;
            return GetMysteryChestInfoServerService.Data.numberRemainLandFragment > (int) default &&
                   GetMysteryChestInfoServerService.Data.myLuckyPoint >=
                   loadGameResponse.data.mysteryChest.limit_lucky_point_to_claim;
        }
    }
}
