using Network.Service;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    [RequireComponent(typeof(TMP_Text))]
    public class MyCurrentSeasonPvpTicketExpensedText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textDefault = "0";


        private void Awake()
        {
            text ??= gameObject.GetComponent<TMP_Text>();
            text.SetText(textDefault);
        }

        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            if (NetworkService.Instance.IsNotLogged()) return;
            text.SetText(
                NetworkService.Instance.services.login.LoginResponse.numberPVPContestSpendTicket.ToString("N0"));
        }
    }
}
