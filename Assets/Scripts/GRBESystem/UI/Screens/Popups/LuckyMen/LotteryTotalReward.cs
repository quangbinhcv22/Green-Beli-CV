using GEvent;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen
{
    public class LotteryTotalReward : MonoBehaviour
    {
        [SerializeField] private Type type;
        [SerializeField] private DateType dateType;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textDefault;
        [SerializeField] private string textFormat = "{0:N0}";


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.GetLotteryDetail, LoadDetail);
        }

        private void OnEnable()
        {
            LoadDetail();
        }

        private void LoadDetail()
        {
            if (GetLotteryDetailServerService.Response.IsError)
            {
                text.SetText(textDefault);
                return;
            }

            text.SetText(string.Format(textFormat, GetInfoByType()));
        }

        private long GetInfoByType()
        {
            return type switch
            {
                Type.Jackpot => GetLotteryDetailServerService.Response.data.poolRewardGfrJackpot,
                Type.TicketSold when dateType is DateType.Today => GetLotteryDetailServerService.Response.data.totalSoldTicketOfDay,
                Type.TicketSold when dateType is DateType.Week => GetLotteryDetailServerService.Response.data.totalSoldTicketOfWeek,
                Type.GFruit when dateType is DateType.Today => GetLotteryDetailServerService.Response.data.poolRewardGfrLotteryOfDay,
                Type.GFruit when dateType is DateType.Week => GetLotteryDetailServerService.Response.data.poolRewardGfrLotteryOfWeek,
                _ => default,
            };
        }
    }

    [System.Serializable]
    public enum DateType
    {
        None = 0,
        Today = 1,
        Week = 2,
    }

    [System.Serializable]
    public enum Type
    {
        Jackpot = 0,
        TicketSold = 1,
        GFruit = 2,
    }
}
