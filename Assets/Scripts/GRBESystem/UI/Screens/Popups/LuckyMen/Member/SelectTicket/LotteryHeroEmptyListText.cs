using System.Linq;
using GEvent;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen.Member.SelectTicket
{
    public class LotteryHeroEmptyListText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textDefault;
        [SerializeField] private string textFormat = "No Heroes found";


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.GetListHero, UpdateView);
        }

        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            if (NetworkService.Instance.IsNotLogged() is false &&
                NetworkService.Instance.services.getHeroList.HeroResponses.Any() is false && IsOpenTime())
                text.SetText(textFormat);
            else
                text.SetText(textDefault);
        }
        
        private bool IsOpenTime()
        {
            return NetworkService.Instance.services.loadGameConfig.Response.data.lottery.IsOpenBuy();
        }
    }
}
