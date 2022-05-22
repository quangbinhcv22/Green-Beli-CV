using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    [RequireComponent(typeof(TMP_Text))]
    public class PvpCurrentSeasonIndexText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "Season {0}";
        [SerializeField] private string textDefault = "Season ---";


        private void Awake()
        {
            text ??= GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.GetPvpContestDetail, UpdateView);
        }
        
        private void OnDisable()
        {
            EventManager.StopListening(EventName.Server.GetPvpContestDetail, UpdateView);
        }

        private void UpdateView()
        {
            var response = NetworkService.Instance.services.getPvpContestDetail.Response;
            if (NetworkService.Instance.IsNotLogged() || response.IsError)
            {
                text.SetText(textDefault);
                return;
            }

            text.SetText(string.Format(textFormat, response.data.seasonIndex.ToString()));
        }
    }
}
