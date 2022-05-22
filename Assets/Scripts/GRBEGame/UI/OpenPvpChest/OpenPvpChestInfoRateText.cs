using System.Globalization;
using GEvent;
using Network.Messages.LoadGame;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.OpenPvpChest
{
    [RequireComponent(typeof(TMP_Text))]
    public class OpenPvpChestInfoRateText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private PvpRewardType pvpRewardType;
        [SerializeField] private string textFormat;
        [SerializeField] private string textDefault;


        private void Awake()
        {
            text ??= GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.LoadGameConfig, UpdateView);
        }
        
        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.LoadGameConfig, UpdateView);
        }

        private void UpdateView()
        {
            var response = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || response.IsError)
            {
                text.SetText(textDefault);
                return;
            }
            
            text.SetText(string.Format(textFormat, GetTotalRate(response.data.pvp)));
        }

        private string GetTotalRate(LoadGameConfigResponse.Pvp pvp)
        {
            const int percent = 100;
            return ((pvpRewardType is PvpRewardType.GFruit
                ? pvp.GetTotalRateOpenPvpChestGFruit()
                : pvp.GetTotalRateOpenPvpChestFragment()) * percent).ToString("F");
        }
    }
}
