using System;
using System.Linq;
using GEvent;
using GRBESystem.UI.Screens.Windows.TeamHero;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using Utils;

namespace GRBESystem.UI.Screens.Popups.LuckyMen
{
    [DefaultExecutionOrder(1000)]
    public class LotteryHandler : MonoBehaviour
    {
        [SerializeField] private SelectButton selectButton;
        private GetLotteryResultByDateResponse _lotteryResult;


        private void Awake()
        {
            selectButton.Set(true);
            _lotteryResult = new GetLotteryResultByDateResponse();
        }

        private void OnEnable()
        {
            OnLoadLottery();
            
            EventManager.StartListening(EventName.PlayerEvent.EndCountdownLottery, OnLoadLottery);
            EventManager.StartListening(EventName.Server.BuyLotteryTicket, OnLoadLottery);
            EventManager.StartListening(EventName.Server.WinLottery, OnLoadLottery);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.PlayerEvent.EndCountdownLottery, OnLoadLottery);
            EventManager.StopListening(EventName.Server.BuyLotteryTicket, OnLoadLottery);
            EventManager.StopListening(EventName.Server.WinLottery, OnLoadLottery);
        }

        private void OnLoadLottery()
        {
            GetLotteryDetailServerService.SendRequest();
            SendResult();
        }

        private void SendResult()
        {
            EventManager.StartListening(EventName.Server.GetLotteryResult, SendRequestTop);
            GetLotteryResultByDateServerService.SendRequest(new GetLotteryResultByDateRequest()
                {time = GetPreviousLotterySessionDateKey(), type = nameof(LotteryType.JACKPOT)});
        }
        
        private void SendRequestTop()
        {
            EventManager.StopListening(EventName.Server.GetLotteryResult, SendRequestTop);
            
            var response = GetLotteryResultByDateServerService.Response;
            if (response.IsError ||
                GetPreviousLotterySessionDateKey().ToDateTime(DateTimeUtils.FullFranceFormatDate) !=
                response.data.GetDate) return;
            
            _lotteryResult = response.data;
            if(_lotteryResult.winTickets.Any())
                _lotteryResult.winTickets.First().isJackpot = true;
            
            EventManager.StartListening(EventName.Server.GetLotteryResult, OnGetLotteryResultResponse);
            GetLotteryResultByDateServerService.SendRequest(new GetLotteryResultByDateRequest()
                {time = GetPreviousLotterySessionDateKey(), type = nameof(LotteryType.DAILY)});
        }
        
        private void OnGetLotteryResultResponse()
        {
            EventManager.StopListening(EventName.Server.GetLotteryResult, OnGetLotteryResultResponse);
            
            var response = GetLotteryResultByDateServerService.Response;
            if (response.IsError)
                return;

            GetLotteryResultByDateServerService.Response.data.winTickets.ForEach(
                item => _lotteryResult.winTickets.Add(item));

            EventManager.EmitEventData(EventName.UI.Select<GetLotteryResultByDateResponse>(), _lotteryResult);
        }
        
        private string GetPreviousLotterySessionDateKey()
        {
            var lotteryScheduleMilliseconds = NetworkService.Instance.services.loadGameConfig.Response.data.lottery.reward_interval_time;

            var previousLotterySessionDate = DateTime.UtcNow.AddMilliseconds(-lotteryScheduleMilliseconds);
            return previousLotterySessionDate.ToString(DateTimeUtils.FullFranceFormatDate);
        }
    }
}
