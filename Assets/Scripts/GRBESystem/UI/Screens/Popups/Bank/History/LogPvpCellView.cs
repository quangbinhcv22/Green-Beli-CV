using System;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GRBEGame.UI.OpenPvpChest;
using GRBEGame.UI.Screen.Inventory.Material;
using Network.Service.Implement;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank.History
{
    public class LogPvpCellView : EnhancedScrollerCellView
    {
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private string timeTextDefault = "--:--:--";
        [SerializeField] private string timeTextFormat;
        [SerializeField] [Space] private TMP_Text roomText;
        [SerializeField] private string roomTextDefault = "Free";
        [SerializeField] private string roomTextFormat = "{0:N0}";
        [SerializeField] [Space] private TMP_Text resultText;
        [SerializeField] private string resultTextLose = "Lose";
        [SerializeField] private string resultTextWin = "Win";
        [SerializeField] [Space] private PvpTotalRewardFrame pvpTotalRewardFrame;
        
        
        public void UpdateView(LogPvpResponse response)
        {
            timeText.SetText(UnixTimeStampToDateTime(response.time).ToString("f"));
            
            roomText.SetText(response.numberTicket <= (int) default
                ? roomTextDefault
                : string.Format(roomTextFormat, response.numberTicket));

            resultText.SetText(response.isWin ? resultTextWin : resultTextLose);
            
            pvpTotalRewardFrame.UpdateView(response.gfrToken,
                response.material != null && response.material.number > (int)default
                    ? new List<MaterialInfo>()
                    {
                        new MaterialInfo(response.material.type, response.material.number)
                    }
                    : new List<MaterialInfo>());
        }
        
        private const int MinYear = 1970;
        private const int MinValueDateTime = 1;
        private const int UtcSecond = 25200000;
        
        private static DateTime UnixTimeStampToDateTime(double seconds)
        {
            var dateTime = new DateTime(MinYear, MinValueDateTime, MinValueDateTime,
                default, default, default, default, DateTimeKind.Utc);
            
            dateTime = dateTime.AddMilliseconds(seconds - UtcSecond).ToLocalTime();
            return dateTime;
        }
    }
}
