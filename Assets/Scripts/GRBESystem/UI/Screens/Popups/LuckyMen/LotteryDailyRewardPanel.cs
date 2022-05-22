using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen
{
    public class LotteryDailyRewardPanel : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller scroller;
        [SerializeField] private LotteryDailyRewardCellView prefab;

        private bool isFirstLoaded;
        private List<DailyData> _dataList = new List<DailyData>();


        private void Awake()
        {
            scroller.Delegate = this;
            EventManager.StartListening(EventName.Server.GetLotteryDetail, OnLoadDetail);
        }

        private void OnEnable()
        {
            if(isFirstLoaded) return;
                OnLoadDetail();
        }

        private void OnLoadDetail()
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if(GetLotteryDetailServerService.Response.IsError || loadGameResponse.IsError) return;

            _dataList.Clear();
            for (int i = 1; i <= loadGameResponse.data.lottery.GetMaxTopCount(); i++)
            {
                _dataList.Add(new DailyData()
                {
                    Top = i,
                    Token = (int) loadGameResponse.data.lottery.GetGFruitReward(
                        (int) GetLotteryDetailServerService.Response.data.poolRewardGfrLotteryOfDay, i),
                });
            }
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _dataList.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 120f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = (LotteryDailyRewardCellView)scroller.GetCellView(prefab);
            cellView.UpdateView(_dataList[cellIndex].Top, _dataList[cellIndex].Token);

            return cellView;
        }
    }

    public class DailyData
    {
        public int Top;
        public int Token;
    }
}
