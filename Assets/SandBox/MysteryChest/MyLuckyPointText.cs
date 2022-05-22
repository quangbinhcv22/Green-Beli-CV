using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

namespace SandBox.MysteryChest
{
    [RequireComponent(typeof(TMP_Text))]
    public class MyLuckyPointText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string stringDefault = "-/-";
        [SerializeField] private string stringFormat = "{0:N0}/{1:N0}";
        [SerializeField] [Space] private Color normalColor;
        [SerializeField] private Color highlightColor;
        
        private void Awake() => text ??= GetComponent<TMP_Text>();

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
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError ||
                GetMysteryChestInfoServerService.Response.IsError)
            {
                text.SetText(stringDefault);
                text.color = normalColor;
                return;
            }

            text.SetText(string.Format(stringFormat, GetMysteryChestInfoServerService.Data.myLuckyPoint,
                loadGameResponse.data.mysteryChest.limit_lucky_point_to_claim));
            
            text.color =
                GetMysteryChestInfoServerService.Data.myLuckyPoint >=
                loadGameResponse.data.mysteryChest.limit_lucky_point_to_claim
                    ? highlightColor
                    : normalColor;
        }
    }
}
