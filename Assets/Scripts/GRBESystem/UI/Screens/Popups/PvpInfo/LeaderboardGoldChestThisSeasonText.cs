using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    [RequireComponent(typeof(TMP_Text))]
    public class LeaderboardGoldChestThisSeasonText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "{0}";
        [SerializeField] private string  textDefault = "-";
        
        
        private void Awake()
        {
            text ??= gameObject.GetComponent<TMP_Text>();
        }
        
        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.GetPvpContestDetail, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StartListening(EventName.Server.GetPvpContestDetail, UpdateView);
        }

        private void UpdateView()
        {
            if (NetworkService.Instance.IsNotLogged())
            {
                text.SetText(textDefault);
                return;
            }

            text.SetText(string.Format(textFormat,
                NetworkService.Instance.services.login.LoginResponse.numberPVPContestGoldChest.ToString()));
        }
    }
}
