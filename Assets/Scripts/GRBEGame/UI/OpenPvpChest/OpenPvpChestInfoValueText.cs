using GEvent;
using Network.Messages.LoadGame;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.OpenPvpChest
{
    [RequireComponent(typeof(TMP_Text))]
    public class OpenPvpChestInfoValueText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private PvpRewardType pvpRewardType;
        [SerializeField] private string textFormat = "{0} - {1}";
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
            text.SetText(GetTotalValue(response.data.pvp));
        }

        private string GetTotalValue(LoadGameConfigResponse.Pvp pvp)
        {
            const int gFruitType = 0;
            const int fragmentType = 9;

            return pvpRewardType is PvpRewardType.GFruit
                ? string.Format(textFormat, pvp.GetMinValue(gFruitType).ToString("N0"),
                    pvp.GetMaxValue(gFruitType).ToString("N0"))
                : string.Format(textFormat, pvp.GetMinValue(fragmentType).ToString("N0"),
                    pvp.GetMaxValue(fragmentType).ToString("N0"));
        }
    }
}
