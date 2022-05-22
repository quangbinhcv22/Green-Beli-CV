using Network.Messages.GetHeroList;
using Network.Service;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.PvpInfo
{
    [RequireComponent(typeof(TMP_Text))]
    public class EarnKeyRewardByPvpBattleText : MonoBehaviour
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
        }

        private void UpdateView()
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError)
            {
                text.SetText(textDefault);
                return;
            }

            var earnKeyReward = loadGameResponse.data.pvp.pvpticket_require_pvpkey_reward[default].key_reward;
            text.SetText(string.Format(textFormat, earnKeyReward));
        }
    }
}
