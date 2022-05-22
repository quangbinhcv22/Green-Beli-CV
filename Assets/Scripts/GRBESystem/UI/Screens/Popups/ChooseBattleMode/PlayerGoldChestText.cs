using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.ChooseBattleMode
{
    [RequireComponent(typeof(TMP_Text))]
    public class PlayerGoldChestText : MonoBehaviour
    {
        [SerializeField] private string formatValue = "{0}";
        [SerializeField] private string defaultValue = "-----";
        
        private TMP_Text _text;
    
    
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _text.SetText(defaultValue);
        }
    
        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Client.EventPvpKeyUpdate, UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.Client.EventPvpKeyUpdate, UpdateView);
        }

        private void UpdateView()
        {
            if (NetworkService.Instance.IsNotLogged())
            {
                _text.SetText(defaultValue);
                return;
            }

            _text.SetText(string.Format(formatValue,
                NetworkService.Instance.services.login.LoginResponse.numberPVPContestGoldChest.ToString()));
        }
    }
}
